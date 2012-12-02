using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TurakasLibrary;
using Turakas.Views;
using Windows.UI.Xaml;
using Windows.UI;
using Turakas.ServiceReference1;

namespace Turakas.ViewModel
{

    public class PlayerView : INotifyPropertyChanged, INotifyCollectionChanged, IServiceInterfaceCallback
    {
        private ObservableCollection<Card> _cardsOnTable;
        private ObservableCollection<string> _messages;
        private List<Player> _otherPlayers;
        private Player _currentPlayer;
        private int _gameId;
        private Card _trump;
        private int _moveIndex;//player to make next move
        private int _hitIndex;//player to make hit
        private int _moveNr; //move to be made in round
        public MainPage pageRef;
        private Windows.UI.Xaml.Visibility _endframe = Visibility.Collapsed;
        private string _looser;
        private ServiceInterfaceClient _client;
        private ObservableCollection<Game> _gameList;
        private ObservableCollection<ServiceUser> _joinersList;

        

        
        
        #region ProperyChangedEvent members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }
        #endregion
        #region constructors

        public PlayerView(string viewerName) {
            this._currentPlayer = new Player(viewerName);
            this._otherPlayers = new List<Player>();
            this._cardsOnTable = new ObservableCollection<Card>();
            //this._interfaceUsed = new Teenus();
            //this._interfaceUsed.setCallbackInterface(this);
            this._moveNr = 1;
            this._client = new ServiceInterfaceClient();
            _joinersList = new ObservableCollection<ServiceUser>();
        }

        #endregion

        #region propertid
        public ObservableCollection<ServiceUser> JoinersList
        {
            get { return _joinersList; }
            set { _joinersList = value; }
        }
        public ObservableCollection<Game> GameList
        {
            get { return _gameList; }
            set { _gameList = value; }
        }

        public ServiceInterfaceClient Client
        {
            get { //return _client == null ? new ServiceInterfaceClient() : _client;
                return _client;
            }
            set { _client = value; }
        }
        public string Looser
        {
            get { return _looser; }
            set {
                if (!value.Equals(_looser))
                {
                    _looser = value;
                    NotifyPropertyChanged("Looser");
                }
            }
        }

        public Windows.UI.Xaml.Visibility Endframe
        {
            get { return _endframe; }
            set
            {
                if (!value.Equals(_endframe)) {
                    _endframe = value;
                    NotifyPropertyChanged("Endframe");
                } 
            }
        }

        public ObservableCollection<string> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        public void setPageRef(MainPage element) {
            this.pageRef = element;
        }

        public int MoveNr
        {
            get { return _moveNr; }
            set { _moveNr = value; }
        }
        public int HitIndex
        {
            get { return _hitIndex; }
            set { _hitIndex = value; }
        }

        public int MoveIndex
        {
            get { return _moveIndex; }
            set {
                if (!value.Equals(this._moveIndex))
                {
                    this._moveIndex = value;
                    NotifyPropertyChanged("MoveIndex");
                }
            }
        }
        public Card Trump
        {
            get { return _trump; }
            set
            {
                if (value != this._trump)
                {
                    this._trump = value;
                    NotifyPropertyChanged("Trump");
                }
            }
        }

        public ObservableCollection<Card> CardsOnTable
        {
            get { return _cardsOnTable; }
            set {
                if (!value.Equals(_cardsOnTable))
                {
                    this._cardsOnTable = value;
                    NotifyPropertyChanged("CardsOnTable");
                }}
        }
        public int GameId
        {
            get { return _gameId; }
            set { _gameId = value; }
        }
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set { 
                if(value != _currentPlayer){
                _currentPlayer = value;
                NotifyPropertyChanged("CurrentPlayer");
            }}
        }
        public List<Player> OtherPlayers
        {
            get { return _otherPlayers; }
            set { _otherPlayers = value; }
        }
        #endregion

        #region game logic

        public Task< ObservableCollection<ServiceUser>> getOtherApplyers(Game g)
        {
            Task<ObservableCollection<ServiceUser>> potentialPlayers =  Client.getJoinCandidatesAsync(g.Id);
            return potentialPlayers;
        }

        public async void getJoiners()
        {
            if (_client.State.Equals( System.ServiceModel.CommunicationState.Closed))
                throw new Exception("yhendus suletud");
            if (_client.State.Equals(System.ServiceModel.CommunicationState.Faulted ))
                throw new Exception("yhendus  faulted state: disaini options page ümber, loo uus view");
            await Client.SubscribeAsync(_currentPlayer.Name);
            bool gameExists = await Client.doesGameExistAsync(_currentPlayer.Name);
            
            if (!gameExists) {
                Task<int> id = Client.createGameAsync(_currentPlayer.Name);
                _gameId = await id;
            }
            if (_gameId == 0)
                throw new Exception("Game id did not get value from service");
            //await Client.setSubscriberGameRefAsync(_gameId);
            Task<ObservableCollection<ServiceUser>> potentialPlayers = Client.getJoinersListAsync(_gameId);
            ObservableCollection<ServiceUser> tmp = await potentialPlayers;
            _joinersList = tmp;
        }
        public async void getGames()
        {
            //TODO test somehow to see if joiners are displayed
            Task<ObservableCollection<Game>> task = Client.sendGameListAsync();
            ObservableCollection<Game> res = await task;
            _gameList = res;
        }

        public async void addNewPlayer(ObservableCollection<ServiceUser> players)
        {
            this._currentPlayer.Id = 0;
            if (players == null)
                return;
            if (_otherPlayers.Count + players.Count <= 5)
            {
                ObservableCollection<ServiceUser> playersWithId = await Client.addPlayersToGameAsync(_gameId, players);

                foreach (ServiceUser p in playersWithId)
                {
                    _otherPlayers.Add(serviceUserToPlayer(p));
                }
                
            }
            else { return; }
        }

        public void applyForSelectedGame(string candidateName, Game g) {
            Client.registerPlayerCandidateAsync(g.Id, candidateName);
        }
        public void initGame() {
            Client.initGameDeckAsync(_gameId);
            Client.shuffleAsync(_gameId);
            deal();
        }

        public void deal() {
            Client.dealCardsAsync(_gameId);
            Client.notifyFirstMoveAsync(_gameId);
        }

        public async Task<int> endPressed()
        {
            ServiceUser u = new ServiceUser();
            u.Name = _currentPlayer.Name;
            u.Id = _currentPlayer.Id;
           int id = await Client.removeFromGameAsync(_gameId,u) ;
           if (id != 0)
           {
               _otherPlayers.RemoveAt(id-1);
           }
           return id;
        }

        public void moveMade(Card c) {
            Client.notifyMoveAsync(cardToServiceCard(c), _currentPlayer.Id, _gameId);
            _currentPlayer.makeMove(c);
        }

        public void hitMade(Card c)
        {
            Client.notifyMoveAsync(cardToServiceCard(c), _currentPlayer.Id, _gameId);
            _currentPlayer.makeMove(c);
        }

        public void pickUp() { 
            
        }

        #endregion

        
        
        #region helper methods
        private Player getPlayerById(int id) {
            if (id == _currentPlayer.Id)
                return _currentPlayer;
            else {
                foreach (Player p in _otherPlayers) {
                    if (p.Id == id)
                        return p;
                }
            } return null;
        }
        public void referToView(Card c)
        {
            MainPage.notifyGameAreaUpdate(c, pageRef);
        }
        public Card serviseCardToCard(ServiceCard arg)
        {
            Card card = new Card(arg.Kind, arg.Rank);
            return card;
        }
        public ServiceCard cardToServiceCard(Card c)
        {
            ServiceCard ret = new ServiceCard();
            ret.Rank = (int)c.Rank;
            ret.Kind = (int)c.Kind;
            return ret;
        }
        public Player serviceUserToPlayer(ServiceUser u)
        {
                if (u != null)
                {
                    Player p = new Player(u.Name, u.Id);
                    return p;
                }else 
                    return null;
            }


        public ServiceUser[] playerToserviceUser(List<Player> u)
        {
            ServiceUser[] ret = new ServiceUser[u.Count];
            Player s;
            for (int i = 0; i < u.Count; i++)
            {
                s = u[i];
                ServiceUser p = new ServiceUser();
                p.Name = s.Name;
                p.Id = s.Id;
                ret[i] = p;
            }
            return ret;
        }
        private void setPlayerActionColor() {
            foreach (Player p in _otherPlayers)
            {
                if (p.Id == _moveIndex)
                    p.Color = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Green);
                if (p.Id == _hitIndex)
                    p.Color = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Red);
            }
            if (_currentPlayer.Id == _moveIndex)
                _currentPlayer.Color = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Green);
            if (_currentPlayer.Id == _hitIndex)
                _currentPlayer.Color = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Red);
        }

        #endregion

        #region callback methods

        public void OnDeal(ObservableCollection<ServiceCard> cards, ServiceCard trump, int playerId, int GameId)
        {
            if (_gameId == GameId)
            {
                _trump = serviseCardToCard(trump);
                if (playerId == 0)
                {
                    foreach (ServiceCard sc in cards)
                    {
                        _currentPlayer.Hand.Add(serviseCardToCard(sc));
                    }
                }
                else
                {
                    foreach (Player p in _otherPlayers)
                    {
                        if (p.Id == playerId)
                        {
                            foreach (ServiceCard sc in cards)
                            {
                                p.Hand.Add(serviseCardToCard(sc));
                            }
                        }
                    }
                }
            }
        }

        public void OnNotifyFirstMove(int firstId, int hitId, int gameId)
        {
            if (_gameId == gameId)
            {
                _moveIndex = firstId;
                _hitIndex = hitId;
                setPlayerActionColor();
            }
        }

        public void OnHitMade(ServiceCard movedCard, int gameId, int playerId)
        {
            if (_gameId == gameId)
            {
                Card c = serviseCardToCard(movedCard);
                referToView(c);
                MoveNr += 1;
                CardsOnTable.Add(c);
                setPlayerActionColor();
            }
        }

        public void OnRoundOver(int gameId, int newMoveId, int newHitId)
        {
            if (gameId == _gameId)
            {
                _cardsOnTable = new ObservableCollection<Card>();
                _moveNr = 1;
                _moveIndex = newMoveId;
                _hitIndex = newHitId;
                setPlayerActionColor();
                Client.dealRoundAsync(_gameId);
            }
        }

        public void OnNotifyMove(ServiceCard movedCard, int gameId, int playerId, int nextHit)
        {

            if (_gameId == gameId)
            {
                Card c = serviseCardToCard(movedCard);
                referToView(c);
                MoveNr += 1;
                CardsOnTable.Add(c);
                HitIndex = nextHit;
                setPlayerActionColor();
            }
        }

        public void OnPlayerFinished(int gameId, int playerId)
        {
            if (_gameId == gameId)
            {
                if (_moveNr % 2 == 0)
                    _cardsOnTable = new ObservableCollection<Card>();
                Player p = getPlayerById(playerId);
                p.Visible = Visibility.Collapsed;
                p.Finished = Visibility.Visible;
            }
        }

        public void OnGameOver(int gameId, int looserId)
        {
            if (gameId == _gameId) {
                Player looser = getPlayerById(looserId);
                Looser = looser.Name;
                Endframe = Visibility.Visible;
                
                //SAVE LOOSERS IN DATABASE?????
            }
        }

        public void OnPickUp(int gameId, int playerId)
        {
            if (gameId == _gameId) {
                Player p = getPlayerById(playerId);
                foreach (Card c in _cardsOnTable) {
                    p.Hand.Add(c);
                }
                _cardsOnTable = new ObservableCollection<Card>();
            }
           
        }
        #endregion callback methods

    }
}
