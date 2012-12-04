using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TurakasServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceInterface : IServiceInterface
    {
        
        private static List<Game> listOfGames = new List<Game>();
        private static readonly List<Subscriber> subscrbers = new List<Subscriber>();
        private static HashSet<OperationContext> op = new HashSet<OperationContext>();
        private static long subscribersId = 0;

        #region subscribers methods

        public long Subscribe(string nickname)
        {
            if (GetMe() == null)
            {
                Subscriber me = new Subscriber(nickname);
                me.Id = subscribersId +1; 
                subscrbers.Add(me);
                subscribersId += 1;
                ////////////////////
                
            }
            return GetMe().Id;
        }

        public void Unsubscribe()
        {
            subscrbers.Remove(GetMe());
        }

        /** Get the subscriber entry for method caller */
        public Subscriber GetMe()
        {
            IServiceCallback cb = OperationContext.
                                        Current.
                                            GetCallbackChannel<IServiceCallback>();
            //return subscrbers.Single(item => item.callback == cb);
            foreach (Subscriber s in subscrbers)
            {
                if (s.callback.Equals(cb))
                    return s;
            }
            return null;
        }

        #endregion

        public List<ServiceUser> getJoinersList(int gameid)
        {
            if (listOfGames.Count() >= gameid)
            {
                Game curr = listOfGames.ElementAt(gameid - 1);
                return curr.Joiners;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Game with this id does not exist");
            }
        }

        public bool doesGameExist(string gameOwner)
        {
            Subscriber me = GetMe();
            if (me == null || me.SubscrGameId < 0)
                return false; 
            else
                return true;
        }
        
        public int createGame(string gameOwner)
        {
            Subscriber me = GetMe();
            if (me == null)
                throw new FaultException("Subscriberit ei leitud");
            Game newGame = new Game();
            newGame.Id = listOfGames.Count + 1;
            me.SubscrGameId = newGame.Id;
            newGame.State = 0;
            newGame.Players = new ServiceUser[6];
            ServiceUser owner = new ServiceUser();
            owner.Id = me.Id;
            owner.Name = gameOwner;
            newGame.Players[0] = owner;
            newGame.Count = 1;
            newGame.Owner = gameOwner;
            newGame.Joiners = new List<ServiceUser>();

            /////////////////////////////////////////////////////////
            //ServiceUser mock1 = new ServiceUser();
            //mock1.Name = "roosa";
            //ServiceUser mock2 = new ServiceUser();
            //mock2.Name = "panter";
            //newGame.Joiners.Add(mock1);
            //newGame.Joiners.Add(mock2);

            ////////////////////////////////////////////////////////////////
            listOfGames.Add(newGame);
            return newGame.Id;
        }

        internal void setOwner(Game g, ServiceUser owner)
        {
            g.Owner = owner.Name;
            g.Players[0] = owner;
        }

        public Game[] sendGameList()
        {
            List<Game> games = new List<Game>();
            foreach (Game g in listOfGames)
            {
                if (g.State == 0)
                    games.Add(g);
            }
            return games.ToArray();
        }

        // LISA CALLBACK ET KA TEISTE MÄNGIJATE OTHERPLAYERS VÄÄRTUSTATAKS
        public void addPlayersToGame(int gameid, ServiceUser[] pl)
        {
            Game g = listOfGames.ElementAt(gameid - 1);
            //add users to game players array
            foreach (ServiceUser p in pl)
            {
                int index = g.Count;
                g.Players[index] = p;
                g.Count = g.Count + 1;
            }
            foreach (ServiceUser u in g.Players)
            {
                if (u != null) { 
                    Subscriber s = getSubscriberById(u.Id);
                    IServiceCallback cb = s.callback;
                    cb.OnPlayersAdded(gameid, g.Players);
                }
            }
            
        }

        public void turnPage(int gameId)
        {
            // TURN PAGE ON EVERY PLAYER IN GAME
            Game g = listOfGames.ElementAt(gameId - 1);
            for (int i = 0; i < g.Count; i++)
            {

                ServiceUser p = g.Players[i];
                Subscriber s = getSubscriberById(p.Id);
                IServiceCallback cb = s.callback;
                
                cb.OnGameStart(gameId);
                Trace.WriteLine("SERVICE: Calling back to turn page done!");
            }
        }

        public void registerPlayerCandidate(int Gameid, string candidateName)
        {

            Game g = listOfGames.ElementAt(Gameid - 1);
            Subscriber me = GetMe();
            ServiceUser candidate = new ServiceUser();
            candidate.Name = me.nickname;
            candidate.Id = me.Id;
            g.Joiners.Add(candidate);
        }


        public ServiceUser[] getJoinCandidates(int gameid)
        {
            Game g = listOfGames.ElementAt(gameid - 1);
            return g.Joiners.ToArray();
        }

        /// <summary>
        /// Fisher and Yates' method
        /// </summary>
        public void shuffle(int gameid)
        {
            Game g = listOfGames.ElementAt(gameid - 1);
            ServiceCard tmp;
            Random r = new Random();
            int randomIndex;
            for (int j = g.Deck.Length - 1; j > 0; j--)
            {
                randomIndex = r.Next(g.Deck.Length - j);
                tmp = g.Deck[j];
                g.Deck[j] = g.Deck[randomIndex];
                g.Deck[randomIndex] = tmp;
            }
        }


        public void initGameDeck(int gameid)
        {
            Game g = listOfGames.ElementAt(gameid - 1);
            g.Deck = new ServiceCard[36];
            int cnt = 0;
            for (int s = 1; s < 5; s++)
            {
                for (int i = 6; i < 15; i++)
                {
                    ServiceCard c = new ServiceCard();
                    c.Kind = s;
                    c.Rank = i;
                    g.Deck[cnt] = c;
                    cnt += 1;
                }
            }
            g.TopCardIndex = g.Deck.Length - 1;
            g.State = 1; //game has started
        }

        public void dealCards(int gameid)
        {
            Subscriber me = GetMe();
            Trace.WriteLine("SERVICE: Dealing card !!!");
            Game g = listOfGames.ElementAt(gameid - 1);
            int deckIndex = g.Deck.Length - 1;

            for (int i = 0; i < g.Count; i++)
            {
                ServiceCard[] cards = new ServiceCard[6];
                for (int j = 0; j < 6; j++)
                {
                    cards[j] = g.Deck[deckIndex];
                    deckIndex = deckIndex - 1;
                }
                ServiceUser p = g.Players[i];
                p.CardsInHand = 6;
                Subscriber s = getSubscriberById(p.Id);
                IServiceCallback cb = s.callback;
                Trace.WriteLine("SERVICE: Calling callback " );
                Trace.WriteLine("SERVICE: Calling player " +s.nickname);
                Trace.WriteLine("SERVICE: Subscribers " + subscrbers.Count);
                cb.OnDeal(cards, g.Deck[0], g.Players[i].Id, g.Id);
                Trace.WriteLine("SERVICE: Calling back done!");
            }
            g.TopCardIndex = deckIndex;
            Trace.WriteLine("SERVICE: deal done!");
        }

        public void dealRound(int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);

            for (int i = 0; i < g.Count; i++)
            {
                List<ServiceCard> cards = new List<ServiceCard>();
                int index = (g.Count - g.MovesIndex - i) % g.Count;
                ServiceUser u = g.Players[index];
                while (u.CardsInHand < 6 && g.TopCardIndex >= 0)
                {
                    cards.Add(g.Deck[g.TopCardIndex]);
                    g.TopCardIndex -= 1;
                }
                Subscriber s = getSubscriberById(u.Id);
                IServiceCallback cb = s.callback;
                cb.OnDeal(cards.ToArray(), g.Deck[0], index, gameId);   //////????????????????????????????????????
            }
        }

        public void removeFromGame(int gameId, long id)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            ServiceUser p;
            if (id == g.Players[0].Id)
            {
                listOfGames.Remove(g);
                for (int i = 0; i < g.Count; i++)
                {
                    Subscriber s = getSubscriberById(g.Players[i].Id);
                    IServiceCallback cb = s.callback;
                    cb.OnGameOver(gameId, g.Players[0].Id);
                }
            }
            else
            {
                //remove player from array
                for (int i = 0; i < g.Count; i++)
                {
                    p = g.Players[i];
                    if (p.Id == id)
                    {
                        g.Players[i] = null;
                        for (int j = i + 1; j < g.Count; j++)
                        {
                            g.Players[j - 1] = g.Players[j];
                        }
                        g.Count = g.Count - 1;
                    }
                }
                for (int i = 0; i < g.Count; i++)
                {
                    Subscriber s = getSubscriberById(g.Players[i].Id);
                    IServiceCallback cb = s.callback;
                    Trace.WriteLine("SERVICE: Calling game over");
                    cb.OnGameOver(gameId, id);
                }
            }
        }


        public void notifyFirstMove(int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            //valib juhusliku mängija, kes soovib muudab koodi nõnda, et käib väiksema trumbi omanik
            Random random = new Random();
            int tmp = random.Next(0, g.Count);
            long playerId = g.Players[tmp].Id;
            g.MovesIndex = tmp;
            g.HitsIndex = tmp < g.Count - 1 ? tmp + 1 : 0;
            for (int i = 0; i < g.Count; i++)
            {
                ServiceUser p = g.Players[i];
                Subscriber s = getSubscriberById(p.Id);
                IServiceCallback cb = s.callback;

                cb.OnNotifyFirstMove(playerId, g.Players[g.HitsIndex].Id, g.Id);
                Trace.WriteLine("SERVICE: Calling back to notify first move!");
            }
            
        }

        private ServiceUser getUserById(int gameId, long id)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            foreach (ServiceUser u in g.Players)
            {
                if (u.Id == id)
                    return u;
            } return null;
        }

        /// <summary>
        /// Sends card to the game. Severely unreasonable approach
        /// </summary>
        /// <param name="cardMoved">card moved</param>
        /// <param name="playerId">playet that made the move</param>
        /// <param name="gameId">game in view</param>
        public void notifyMove(ServiceCard cardMoved, long playerId, int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            ServiceUser player = getUserById(gameId, playerId);
            player.CardsInHand -= 1;
            g.NrOfCardsOnTable += 1;
            
            if (player.CardsInHand == 0 && g.TopCardIndex <= 0)
            {
                player.Finished = true;

                if (isGameOver(gameId))
                {
                    for (int i = 0; i < g.Count; i++)
                    {
                        ServiceUser p = g.Players[i];
                        Subscriber s = getSubscriberById(p.Id);
                        IServiceCallback cb = s.callback;

                        cb.OnGameOver(gameId, g.Players[g.HitsIndex].Id);
                        Trace.WriteLine("SERVICE: Calling back to notify Game Over!");
                    }

                    return;
                }
                else
                {
                    for (int i = 0; i < g.Count; i++)
                    {
                        ServiceUser p = g.Players[i];
                        Subscriber s = getSubscriberById(p.Id);
                        IServiceCallback cb = s.callback;

                        cb.OnPlayerFinished(gameId, playerId);
                        Trace.WriteLine("SERVICE: Calling back to notify first move!");
                    }
                }
                    
            }
            for (int i = 0; i < g.Count; i++)
            {
                ServiceUser p = g.Players[i];
                Subscriber s = getSubscriberById(p.Id);
                IServiceCallback cb = s.callback;

                cb.OnNotifyMove(cardMoved, gameId, playerId, g.Players[g.HitsIndex].Id);//seepärast peakski teenusele lisaks olema back endis veel üks klass
                Trace.WriteLine("SERVICE: Calling back to notify first move!");
            }
            
            
        }


        #region helper methods
        public void setSubscriberGameRef(int id)
        {
            Subscriber s = GetMe();
            if (s == null)
                throw new FaultException("subscribe pole onnestunud");
            s.SubscrGameId = id;
        }

        public List<ServiceUser> getPlayersNotFinished(int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            List<ServiceUser> result = new List<ServiceUser>();
            foreach (ServiceUser u in g.Players)
            {
                if (u != null && u.Finished == false)
                    result.Add(u);
            }
            return result;
        }

        public int getNextActivePlayerIndex(Game g, int start)
        {
            int next = start;
            for (int i = next; i < 6; i++)
            {
                ServiceUser u = g.Players[i] != null ? g.Players[i] : g.Players[i - g.Count];
                if (u.Finished == false)
                    return i;
            }
            throw new ArgumentOutOfRangeException("Game is not ended, but could not find active players");
        }


        public bool isGameOver(int gameId)
        {
            return getPlayersNotFinished(gameId).Count > 1;
        }

        /// <summary>
        /// Method gets called at the end of the round
        /// </summary>
        /// <param name="gameId"></param>
        public void setNextMoveAndHitId(int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            if (!g.PickedUp)
            {
                if (!g.Players[g.HitsIndex].Finished)
                {
                    g.MovesIndex = g.HitsIndex;
                    g.HitsIndex = getNextActivePlayerIndex(g, g.HitsIndex);
                }
                else
                {
                    g.MovesIndex = getNextActivePlayerIndex(g, g.HitsIndex);
                    g.HitsIndex = getNextActivePlayerIndex(g, g.MovesIndex);
                }
            }
            g.PickedUp = false;
        }


        #endregion




        public void notifyHitMade(int gameId, ServiceCard movedCard)
        {
            Trace.WriteLine("SERVICE: notify hit made");
           
            Game g = listOfGames.ElementAt(gameId - 1);
            g.Players[g.HitsIndex].CardsInHand -= 1;
            g.NrOfCardsOnTable += 1;
             Trace.WriteLine("SERVICE: cards on table "+g.NrOfCardsOnTable);
            if (g.Players[g.HitsIndex].CardsInHand == 0 && g.TopCardIndex <= 0)
            {
                g.Players[g.HitsIndex].Finished = true;
                if (isGameOver(gameId))
                {
                    for (int i = 0; i < g.Count; i++)
                    {
                        int looser = getNextActivePlayerIndex(g, g.HitsIndex);

                        Subscriber s = getSubscriberById(g.Players[i].Id);
                        IServiceCallback cb = s.callback;
                        cb.OnGameOver(gameId, g.Players[looser].Id);
                    }

                    return;
                }
                else {
                    for (int i = 0; i < g.Count; i++)
                    {
                        Subscriber s = getSubscriberById(g.Players[i].Id);
                        IServiceCallback cb = s.callback;
                        cb.OnPlayerFinished(gameId, g.Players[g.HitsIndex].Id);
                    }
                }
                   
            }
            if (g.NrOfCardsOnTable == 12)
            {
                setNextMoveAndHitId(gameId);
                for (int i = 0; i < g.Count; i++)
                {
                    Subscriber s = getSubscriberById(g.Players[i].Id);
                    IServiceCallback cb = s.callback;
                    Trace.WriteLine("SERVICE: calling round over");
                    cb.OnRoundOver(gameId, g.Players[g.MovesIndex].Id, g.Players[g.HitsIndex].Id);
                }
                
            }
            else
            {
                for (int i = 0; i < g.Count; i++)
                {
                    Subscriber s = getSubscriberById(g.Players[i].Id);
                    IServiceCallback cb = s.callback;
                    cb.OnHitMade(movedCard, gameId, g.Players[g.HitsIndex].Id);
                }
            }
        }


        public void pickUp(int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            g.PickedUp = true;
            g.Players[g.HitsIndex].CardsInHand += g.NrOfCardsOnTable;
            g.NrOfCardsOnTable = 0;
            setNextMoveAndHitId(gameId);

            for (int i = 0; i < g.Count; i++)
            {
                Subscriber s = getSubscriberById(g.Players[i].Id);
                IServiceCallback cb = s.callback;
                cb.OnPickUp(gameId, g.Players[g.HitsIndex].Id);
            }
            if (!isGameOver(gameId))
            {
                for (int i = 0; i < g.Count; i++)
                {
                    Subscriber s = getSubscriberById(g.Players[i].Id);
                    IServiceCallback cb = s.callback;
                    cb.OnRoundOver(gameId, g.Players[g.MovesIndex].Id, g.Players[g.HitsIndex].Id);
                }
            }
            else
                for (int i = 0; i < g.Count; i++)
                {
                    Subscriber s = getSubscriberById(g.Players[i].Id);
                    IServiceCallback cb = s.callback;
                     cb.OnGameOver(gameId, g.Players[g.HitsIndex].Id);
                }
               
        }

        public static Subscriber getSubscriberById(long id)
        {
            foreach (Subscriber s in subscrbers)
            {
                if (s.Id == id)
                    return s;
            }
            return null;
        }

        public static List<Subscriber> getSubscribers()
        {
            return subscrbers;
        }
    }

    public class Subscriber
    {
        /* Nickname for subscriber */
        public string nickname;
        /* Interface for message notify */
        public IServiceCallback callback;
        private int _subscrGameId = -1;
        private long id;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        public int SubscrGameId
        {
            get { return _subscrGameId; }
            set { _subscrGameId = value; }
        }

        public Subscriber(string nickname)
        {
            this.nickname = nickname;
            this.callback = OperationContext.
                                Current.
                                    GetCallbackChannel<IServiceCallback>();
        }

        public bool IsChannelOpen()
        {
            return ((ICommunicationObject)callback).State == CommunicationState.Opened;
        }
    }

}
