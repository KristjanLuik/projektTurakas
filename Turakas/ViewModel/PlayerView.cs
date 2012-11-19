﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turakas.classes;

namespace Turakas.ViewModel
{
    //public delegate List<string> generateJoinersList();

    public class PlayerView:IServiceCallbackInterface
    {
        private ObservableCollection<Card> _cardsOnTable;
        private List<Player> _otherPlayers;
        private Player _currentPlayer;
        public IServiceInterface _interfaceUsed;
        private int _gameId;
        private Kind _trump;

        
        #region constructors

        public PlayerView(string viewerName) {
            this._currentPlayer = new Player(viewerName);
            this._otherPlayers = new List<Player>();
            this._interfaceUsed = new MockService();
            this._interfaceUsed.setCallbackInterface(this);
        }

        #endregion

        #region propertid

        public Kind Trump
        {
            get { return _trump; }
            set { _trump = value; }
        }

        public ObservableCollection<Card> CardsOnTable
        {
            get { return _cardsOnTable; }
            set { _cardsOnTable = value; }
        }
        public int GameId
        {
            get { return _gameId; }
            set { _gameId = value; }
        }
        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set { _currentPlayer = value; }
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
        }

        public void deal() {
            _interfaceUsed.dealCards(_gameId);
        }
        public void doSomething(string parameter)
        {
            // Do someting
        }
        public Card serviseCardToCard(ServiceCard arg) {
            Card card = new Card(arg.Kind, arg.Rank);
            return card;
        }

        public void OnDeal(ServiceCard[] cards, ServiceCard trump, int playerId)
        {
            _trump = serviseCardToCard(trump).Kind;
            if (playerId == 0)
            {
                foreach (ServiceCard sc in cards)
                {
                    _currentPlayer.Hand.Add(serviseCardToCard(sc));
                }
            }
            else {
                foreach (Player p in _otherPlayers)
                {
                    if (p.Id == playerId) {
                        foreach (ServiceCard sc in cards)
                        {
                            p.Hand.Add(serviseCardToCard(sc));
                        }
                    }
                }
            }
        }


    }
}