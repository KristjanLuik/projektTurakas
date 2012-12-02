using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.ServiceModel;
using System.Diagnostics;
using TurakasServiceLibrary;
using System.Threading;
using System.Windows.Navigation;

namespace TurakasTest
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public class ViewModel : INotifyPropertyChanged, INotifyCollectionChanged, IServiceCallback
    {
        /// <summary>
        /// Cards on the table.
        /// </summary>
        private ObservableCollection<Card> _cardsOnTable;
        private ObservableCollection<string> _messages;
        private ObservableCollection<Player> _otherPlayers;
        private Player _currentPlayer;
        private int _gameId;
        private Card _trump;
        private long _moveIndex;//player to make next move
        private long _hitIndex;//player to make hit
        private int _moveNr; //move to be made in round
        public MainPage pageRef;
        private System.Windows.Visibility _endframe = Visibility.Collapsed;
        private string _looser;
        private ServiceInterfaceClient _client;
        private ObservableCollection<Game> _gameList;
        private ObservableCollection<ServiceUser> _joinersList;
        private SynchronizationContext _uiSyncContext = null;
        private int _raisePropertyChanged;
        private SelectPage _sp;

        

     
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

        public ViewModel(string viewerName) {
            this._uiSyncContext = SynchronizationContext.Current;

            this._currentPlayer = new Player(viewerName);
            this._otherPlayers = new ObservableCollection<Player>();
            this._cardsOnTable = new ObservableCollection<Card>();
            //this._interfaceUsed = new Teenus();
            //this._interfaceUsed.setCallbackInterface(this);
            this._moveNr = 1;
            
            this._client = new ServiceInterfaceClient(this);
            this._client.connect();
            long i = _client.Subscribe(viewerName);
            _currentPlayer.Id = i;
            _joinersList = new ObservableCollection<ServiceUser>();
            Trace.WriteLine("VIEW: Client created!");
        }

        #endregion

        #region propertid
        public SelectPage Sp
        {
            get { return _sp; }
            set { _sp = value; }
        }

        public int RaisePropertyChanged
        {
            get { return _raisePropertyChanged; }
            set
            {
                if (value != _raisePropertyChanged)
                {
                    _raisePropertyChanged = value;
                    NotifyPropertyChanged("RaisePropertyChanged");
                }
                
                 }
        }

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

        public System.Windows.Visibility Endframe
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
        public long HitIndex
        {
            get { return _hitIndex; }
            set { _hitIndex = value; }
        }

        public long MoveIndex
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
                if(!value.Equals(_currentPlayer)){
                _currentPlayer = value;
                NotifyPropertyChanged("CurrentPlayer");
            }}
        }
        public ObservableCollection<Player> OtherPlayers
        {
            get { return _otherPlayers; }
            set { _otherPlayers = value; }
        }
        #endregion

        #region game logic
        
        public ObservableCollection<ServiceUser> getOtherApplyers(Game g)
        {
            ServiceUser[] potentialPlayers = Client.getServiceInterface().getJoinCandidates(g.Id);
            return arrayToObservableCollection(potentialPlayers);
        }
        

        public void getJoiners()
        {
            bool gameExists = Client.getServiceInterface().doesGameExist(_currentPlayer.Name);
            
            if (!gameExists) {
                _gameId = Client.getServiceInterface().createGame(_currentPlayer.Name);
            }
            if (_gameId == 0)
                throw new Exception("Game id did not get value from service");
            //await Client.setSubscriberGameRefAsync(_gameId);
            List<ServiceUser> potentialPlayers = Client.getServiceInterface().getJoinersList(_gameId);
            _joinersList.Clear();
            foreach(ServiceUser su in potentialPlayers) {
                _joinersList.Add(su);
            }
        }

        public void getGames()
        {
             Game[] games = Client.getServiceInterface().sendGameList();
             ObservableCollection<Game> res = arrayToObservableCollection(games);
            _gameList = res;
        }

        public void addNewPlayer(ServiceUser[] players)
        {
            this._currentPlayer.Id = 0;
            if (players == null)
                return;
            if (_otherPlayers.Count + players.Length <= 5)
            {
                ServiceUser[] playersWithId = Client.getServiceInterface().addPlayersToGame(_gameId, players);

                foreach (ServiceUser p in playersWithId)
                {
                    _otherPlayers.Add(serviceUserToPlayer(p));
                }
                
            }
            else { return; }
        }

        public void applyForSelectedGame(string candidateName, Game g) {
            Client.getServiceInterface().registerPlayerCandidate(g.Id, candidateName);
        }

        public void initGame() {
            Client.getServiceInterface().initGameDeck(_gameId);
            Client.getServiceInterface().shuffle(_gameId);
            deal();
        }

        public void deal() {
            Client.getServiceInterface().dealCards(_gameId);
            Client.getServiceInterface().notifyFirstMove(_gameId);
        }

        public long endPressed()
        {
            ServiceUser u = new ServiceUser();
            u.Name = _currentPlayer.Name;
            u.Id = _currentPlayer.Id;
            long id = Client.getServiceInterface().removeFromGame(_gameId, u);
           if (id != 0)
           {
               foreach (Player p in _otherPlayers) {
                   if (p.Id == id)
                   {
                       _otherPlayers.Remove(p);
                       break;
                   }
               }
           }
           return id;
        }

        public void moveMade(Card c) {
            Client.getServiceInterface().notifyMove(cardToServiceCard(c), _currentPlayer.Id, _gameId);

            _currentPlayer.makeMove(c);
        }

        public void hitMade(Card c)
        {
            Client.getServiceInterface().notifyMove(cardToServiceCard(c), _currentPlayer.Id, _gameId);
            _currentPlayer.makeMove(c);
        }

        public void pickUp() { 
            
        }

        #endregion

        
        
        #region helper methods

        internal int findPlayerIndexById(long id)
        {
            for (int i = 0; i < _otherPlayers.Count; i++)
            {
                Player p = _otherPlayers[i];
                if (p.Id == id)
                    return i;
            }
            return -1;
        }

        private Player getPlayerById(long id) {
            if (id == _currentPlayer.Id)
                return _currentPlayer;
            else {
                foreach (Player p in _otherPlayers) {
                    if (p.Id == id)
                        return p;
                }
            } return null;
        }

        private ObservableCollection<ServiceUser> arrayToObservableCollection(ServiceUser[] array){
            ObservableCollection<ServiceUser> result = new ObservableCollection<ServiceUser>();
            foreach (ServiceUser u in array) {
                result.Add(u);
            }
            return result;
        }

        private ObservableCollection<Game> arrayToObservableCollection(Game[] array)
        {
            ObservableCollection<Game> result = new ObservableCollection<Game>();
            foreach (Game u in array)
            {
                result.Add(u);
            }
            return result;
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
                    p.Color = Colors.Green;
                if (p.Id == _hitIndex)
                    p.Color = Colors.Red;
            }
            if (_currentPlayer.Id == _moveIndex)
                _currentPlayer.Color = Colors.Green;
            if (_currentPlayer.Id == _hitIndex)
                _currentPlayer.Color = Colors.Red;
            
            RaisePropertyChanged = 1;
        }

        #endregion

        #region callback methods

        public void OnGameStart(int gameId)
        {

            SendOrPostCallback callback = new SendOrPostCallback(
                delegate(object state)
                {
                    OnGameStart_UI(gameId);
                }
            );
            _uiSyncContext.Post(callback, "");
        }
        public void OnGameStart_UI(int gameId)
        {

            MainPage page = new MainPage(this);
            _sp.NavigationService.Navigate(page);
        }

        public void OnDeal(ServiceCard[] cards, ServiceCard trump, long playerId, int GameId)
        {
            SendOrPostCallback callback = new SendOrPostCallback(
                delegate(object state) {
                    OnDeal_UI(cards, trump, playerId, GameId);
                }
            );
            _uiSyncContext.Post(callback, "");
        }

        public void OnDeal_UI(ServiceCard[] cards, ServiceCard trump, long playerId, int GameId)
        {
            Trace.WriteLine("VIEW: OnDeal called()");

            if (_gameId == GameId)
            {
                _trump = serviseCardToCard(trump);
                if (playerId == _currentPlayer.Id)
                {
                    foreach (ServiceCard sc in cards)
                    {
                        _currentPlayer.Hand.Add(serviseCardToCard(sc));
                    }
                }
                //else
                //{
                //    foreach (Player p in _otherPlayers)
                //    {
                //        if (p.Id == playerId)
                //        {
                //            foreach (ServiceCard sc in cards)
                //            {
                //                p.Hand.Add(serviseCardToCard(sc));
                //            }
                //        }
                //    }
                //}
            }
        }

        public void OnNotifyFirstMove(long firstId, long hitId, int gameId)
        {
            SendOrPostCallback callback = new SendOrPostCallback(
                delegate(object state)
                {
                    OnNotifyFirstMove_UI(firstId, hitId, gameId);
                }
            );
            _uiSyncContext.Post(callback, "");
        }

        public void OnNotifyFirstMove_UI(long firstId, long hitId, int gameId)
        {
            Trace.WriteLine("VIEW: OnNotifyFirstMove called()");
            if (_gameId == gameId)
            {
                _moveIndex = firstId;
                _hitIndex = hitId;
                setPlayerActionColor();
            }
        }
       
        public void OnHitMade(ServiceCard movedCard, int gameId, long playerId)
        {
            SendOrPostCallback callback = new SendOrPostCallback(
                delegate(object state)
                {
                    OnHitMade_UI(movedCard, gameId, playerId);
                }
            );
            _uiSyncContext.Post(callback, "");
        }

        public void OnHitMade_UI(ServiceCard movedCard, int gameId, long playerId)
        {
            Trace.WriteLine("VIEW: OnHitMade called()");
            if (_gameId == gameId)
            {
                Card c = serviseCardToCard(movedCard);
                referToView(c);
                MoveNr += 1;
                CardsOnTable.Add(c);
                setPlayerActionColor();
            }
        }

        public void OnRoundOver(int gameId, long newMoveId, long newHitId)
        {
            if (gameId == _gameId)
            {
                _cardsOnTable = new ObservableCollection<Card>();
                _moveNr = 1;
                _moveIndex = newMoveId;
                _hitIndex = newHitId;
                setPlayerActionColor();
                Client.getServiceInterface().dealRound(_gameId);
            }
        }

        public void OnNotifyMove(ServiceCard movedCard, int gameId, long playerId, long nextHit)
        {

            SendOrPostCallback callback = new SendOrPostCallback(
                delegate(object state)
                {
                    OnNotifyMove_UI(movedCard, gameId, playerId, nextHit);
                }
            );
            _uiSyncContext.Post(callback, "");
        }

        public void OnNotifyMove_UI(ServiceCard movedCard, int gameId, long playerId, long nextHit)
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

        public void OnPlayerFinished(int gameId, long playerId)
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

        public void OnGameOver(int gameId, long looserId)
        {
            if (gameId == _gameId) {
                Player looser = getPlayerById(looserId);
                Looser = looser.Name;
                Endframe = Visibility.Visible;
                
                //SAVE LOOSERS IN DATABASE?????
            }
        }

        public void OnPickUp(int gameId, long playerId)
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
