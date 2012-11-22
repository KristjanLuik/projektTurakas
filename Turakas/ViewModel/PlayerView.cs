﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Turakas.classes;
using Turakas.Views;
using Windows.UI.Xaml;

namespace Turakas.ViewModel
{

    public class PlayerView:IServiceCallbackInterface, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private ObservableCollection<Card> _cardsOnTable;
        private List<Player> _otherPlayers;
        private Player _currentPlayer;
        public IServiceInterface _interfaceUsed;
        private int _gameId;
        private Card _trump;
        private int _moveIndex;//player to make next move
        private int _hitIndex;//player to make hit
        private int _moveNr; //move to be made in round
        public MainPage pageRef;
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
            this._interfaceUsed = new MockService();
            this._interfaceUsed.setCallbackInterface(this);
            this._moveNr = 1;
        }

        #endregion

        #region propertid

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

        public List<ServiceUser> getOtherApplyers(Game g)
        {
            List<ServiceUser> potentialPlayers = _interfaceUsed.getJoinCandidates(g.Id);
            return potentialPlayers;
        }

        public List<ServiceUser> getJoiners()
        {
            _gameId = _interfaceUsed.createGame(_currentPlayer.Name);
            List<ServiceUser> potentialPlayers = _interfaceUsed.getJoinersList(_gameId);
            return potentialPlayers;
        }
        public ObservableCollection<Game> getGames()
        {
            //TODO ensure that list will be refreshed after a period of time
            ObservableCollection<Game> unstartedGames = new ObservableCollection<Game>();
            Game[] res = _interfaceUsed.sendGameList();
            foreach (Game g in res) {
                unstartedGames.Add(g);
            }
            return unstartedGames;
        }

        public void addNewPlayer(List<ServiceUser> players)
        {
            this._currentPlayer.Id = 0;
            if (players == null)
                return;
            if (_otherPlayers.Count + players.Count <= 5)
            {
                ServiceUser[] playersWithId = _interfaceUsed.addPlayersToGame(_gameId, players.ToArray());

                foreach (Player p in serviceUserToPlayer(playersWithId))
                {
                    _otherPlayers.Add(p);
                }
                
            }
            else { return; }
        }

        public List<Player> serviceUserToPlayer(ServiceUser[] u)
        {
            List<Player> ret = new List<Player>();
            ServiceUser s;
            int i;
            for (i = 0; i < u.Length; i++)
            {   
                s = u[i];
                if (s != null)
                {
                    Player p = new Player(s.Name, s.Id);
                    ret.Add(p);
                }
            }
            return ret;
        }
        public ServiceUser[] playerToserviceUser(List<Player> u)
        {
            ServiceUser[] ret = new ServiceUser[u.Count];
            Player s;
            for (int i = 0; i < u.Count; i++)
            {
                s = u[i];
                ServiceUser p = new ServiceUser(s.Name, s.Id);
                ret[i] = p;
            }
            return ret;
        }

        public void applyForSelectedGame(string candidateName, Game g) {
            _interfaceUsed.registerPlayerCandidate(g.Id, candidateName);
        }
        public void initGame() {
            _interfaceUsed.initGameDeck(_gameId);
            _interfaceUsed.shuffle(_gameId);
            deal();
        }

        public void deal() {
            _interfaceUsed.dealCards(_gameId);
            _interfaceUsed.notifyFirstMove(_gameId);
        }
        public int endPressed()
        {
           int id = _interfaceUsed.removeFromGame(_gameId,new ServiceUser(_currentPlayer.Name, _currentPlayer.Id)) ;
           if (id != 0)
           {
               _otherPlayers.RemoveAt(id-1);
           }
           return id;
        }

        public Card serviseCardToCard(ServiceCard arg) {
            Card card = new Card(arg.Kind, arg.Rank);
            return card;
        }
        public ServiceCard cardToServiceCard(Card c) {
            ServiceCard ret = new ServiceCard((int)c.Rank, (int)c.Kind);
            return ret;
        }

        public void OnDeal(ServiceCard[] cards, ServiceCard trump, int playerId, int GameId)
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

        public void OnNotifyFirstMove(int id, int gameId)
        {
            if(_gameId == gameId)
            _moveIndex = id;
        }


        public void moveMade(Card c) {
            _interfaceUsed.notifyMove(cardToServiceCard(c), _currentPlayer.Id, _gameId);
            //_moveNr += 1;
            _currentPlayer.makeMove(c);
            //_cardsOnTable.Add(c);
        }

        public void OnNotifyMove(ServiceCard movedCard, int gameId, int playerId, int nextHit)
        {
            
            if (_gameId == gameId) {
                Card c = serviseCardToCard(movedCard);
                referToView(c);
                if (_moveNr == 6)
                {
                    _moveIndex = -1; //küsi järgmise käigu õigus
                }
                MoveNr += 1;
                CardsOnTable.Add(c);
                HitIndex = nextHit;
            }
        }

        public void referToView(Card c)
        {
            MainPage.notifyGameAreaUpdate(c, pageRef);
        }
    }
}
