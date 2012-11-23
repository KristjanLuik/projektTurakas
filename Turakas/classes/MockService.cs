using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Turakas.classes
{
    public class MockService:IServiceInterface
    {
        #region mock data
        public static ServiceUser pl1 = new ServiceUser("Mari");
        public static ServiceUser pl2 = new ServiceUser("Jüri");
        public static ServiceUser pl3 = new ServiceUser("Jaan");
        Game mock1 = new Game(new List<ServiceUser>() { pl1, pl2, pl3 }, 1, 0);
        Game mock2 = new Game(new List<ServiceUser>() { pl1, pl2, pl3 }, 2, 0);
        #endregion
        private List<Game> listOfGames = new List<Game>();
        //private List<string> _cardKinds = new List<string>() {"c", "s", "d", "h"};
        private IServiceCallbackInterface _callbackInterface;

        public void addToList()
        {
            listOfGames.Add(mock1);
            listOfGames.Add(mock2);
        }

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

        public int createGame(string gameOwner)
        {
            //Game newGame = new Game();
            //newGame.Id = listOfGames.Count() + 1;
            //newGame.State = 0;
            //newGame.Players = new Player[6];
            //newGame.Players[0] = new ServicePlayer(gameOwner, 0);
            //newGame.Count = 1;
            //newGame.Owner = gameOwner;
            //listOfGames.Add(newGame);
            //return newGame.Id;
            //
            addToList();
            ServiceUser owner = new ServiceUser(gameOwner);
            Game g = listOfGames.ElementAt(0);
            owner.Id = 0;
            g.Count = 1;
            setOwner(g,owner);
            return 1;
        }
        internal void setOwner(Game g,ServiceUser owner)
        {
            g.Owner = owner.Name;
            g.Players[0] = owner;
        }

        public Game[] sendGameList() {
            List<Game> games = new List<Game>();
            foreach (Game g in listOfGames)
            {
                if (g.State == 0)
                    games.Add(g);
            }
            return games.ToArray();
        }


        public ServiceUser[] addPlayersToGame(int gameid, ServiceUser[] pl)
        {
            Game g = listOfGames.ElementAt(gameid - 1);
            ServiceUser[] ret = new ServiceUser[pl.Length];
            foreach(ServiceUser p in pl)
            {
                int index = g.Count;
                g.Players[index] = p;
                p.Id = g.Count;
                g.Count = g.Count + 1;
                ret[index - 1] = p;
               
            }
            return ret;
        }


        public void registerPlayerCandidate(int Gameid, string candidateName)
        {
            Game g = listOfGames.ElementAt(Gameid - 1);
            ServiceUser candidate = new ServiceUser(candidateName);
            g.Joiners.Add(candidate);
        }


        public List<ServiceUser> getJoinCandidates(int gameid)
        {
            Game g = listOfGames.ElementAt(gameid - 1);
            return g.Joiners;
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
            for (int s = 1; s < 5; s++ )
            {
                for (int i = 6; i < 15; i++)
                {
                    g.Deck[cnt] = (new ServiceCard(i, s));
                    cnt += 1;
                }
            }
            g.TopCardIndex = g.Deck.Length - 1;
            g.State = 1; //game has started
        }


        public void setCallbackInterface(IServiceCallbackInterface callbackInterface)
        {
            this._callbackInterface = callbackInterface;
        }


        public void dealCards(int gameid)
        {
            Game g = listOfGames.ElementAt(gameid - 1);
            int deckIndex = g.Deck.Length - 1;
            
            for (int i = 0; i < g.Count; i++) {
                ServiceCard[] cards = new ServiceCard[6];
                for (int j = 0; j < 6; j++)
                {
                    cards[j] = g.Deck[deckIndex];
                    deckIndex = deckIndex - 1;
                }
                g.Players[i].CardsInHand = 6;
                //TODO, LISA GAMEID KONTROLL
                this._callbackInterface.OnDeal(cards, g.Deck[0], g.Players[i].Id, g.Id);
            }
            g.TopCardIndex = deckIndex;
        }

        public void dealRound(int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
           
            for (int i = 0; i < g.Count; i++)
            {
                List<ServiceCard> cards = new List<ServiceCard>();
                int index = (g.Count - g.MovesIndex -i) % g.Count;
                ServiceUser u = g.Players[index];
                while (u.CardsInHand < 6 && g.TopCardIndex >= 0) {
                    cards.Add(g.Deck[g.TopCardIndex]);
                    g.TopCardIndex -= 1;
                }
                _callbackInterface.OnDeal(cards.ToArray(), g.Deck[0], index, gameId);
            }
        }

        public int removeFromGame(int gameId, ServiceUser player)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            ServiceUser p;
            if (player.Name.Equals(g.Players[0].Name) && player.Id.Equals(g.Players[0].Id))
            {
                listOfGames.Remove(g);
                return 0;
            }
            else {
                for(int i=0; i<6;i++)
                {
                    p = g.Players[i];
                    if (p.Id.Equals(player.Id) && p.Name.Equals(player.Name))
                    {
                        g.Players[i] = null;
                        g.Count = g.Count - 1;
                        return p.Id;
                    }
                }
            } return 0;
        }


        public void notifyFirstMove(int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            //valib juhusliku mängija, kes soovib muudab koodi nõnda, et käib väiksema trumbi omanik
            Random random = new Random();
            int playerId = random.Next(0, g.Count);
            g.MovesIndex = playerId;
            g.HitsIndex = playerId < g.Count-1 ? playerId +1 : 0;
            _callbackInterface.OnNotifyFirstMove(playerId, g.HitsIndex,g.Id);
            //TODO, LISA GAMEID KONTROLL
            _callbackInterface.OnNotifyFirstMove(0, g.HitsIndex,g.Id);
        }

        /// <summary>
        /// Sends card to the game.
        /// </summary>
        /// <param name="cardMoved">card moved</param>
        /// <param name="playerId">playet that made the move</param>
        /// <param name="gameId">game in view</param>
        public void notifyMove(ServiceCard cardMoved, int playerId, int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            g.Players[playerId].CardsInHand -= 1;
            g.NrOfCardsOnTable += 1;
            if (g.Players[playerId].CardsInHand == 0 && g.TopCardIndex <= 0) {
                g.Players[playerId].Finished = true;
                if (isGameOver(gameId))
                {
                    _callbackInterface.OnGameOver(gameId, g.HitsIndex);
                    return;
                }
                else
                    _callbackInterface.OnPlayerFinished(gameId, playerId);
            }
               
            _callbackInterface.OnNotifyMove(cardMoved, gameId, playerId, g.HitsIndex);//seepärast peakski teenusele lisaks olema back endis veel üks klass
           // _callbackInterface.OnPlayerFinished(gameId, 3);
           // _callbackInterface.OnNotifyMove(new ServiceCard(10,1), 1, 1, next);
        }

        
        #region helper methods

        public List<ServiceUser> getPlayersNotFinished(int gameId) {
            Game g = listOfGames.ElementAt(gameId - 1);
            List<ServiceUser> result = new List<ServiceUser>();
            foreach (ServiceUser u in g.Players) {
                if (u != null && u.Finished == false)
                    result.Add(u);
            }
            return result;
        }

        public int getNextActivePlayer(Game g, int start)
        {
            int next = start;
            for (int i = next; i < 6; i++ )
            {
                ServiceUser u = g.Players[i] != null ? g.Players[i] : g.Players[i - g.Count];
                if (u.Finished == false)
                    return u.Id;
            }
            return -1;
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
                    g.HitsIndex = getNextActivePlayer(g, g.HitsIndex);
                }
                else
                {
                    g.MovesIndex = getNextActivePlayer(g, g.HitsIndex);
                    g.HitsIndex = getNextActivePlayer(g, g.MovesIndex);
                }
            }
            g.PickedUp = false;
        }


        #endregion




        public void notifyHitMade(int gameId, ServiceCard movedCard)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            g.Players[g.HitsIndex].CardsInHand -= 1;
            g.NrOfCardsOnTable += 1;
            if (g.Players[g.HitsIndex].CardsInHand == 0 && g.TopCardIndex <= 0)
            {
                g.Players[g.HitsIndex].Finished = true;
                if (isGameOver(gameId))
                {
                    _callbackInterface.OnGameOver(gameId, getNextActivePlayer(g, g.HitsIndex));
                    return;
                }else
                    _callbackInterface.OnPlayerFinished(gameId, g.HitsIndex);
            }
            if (g.NrOfCardsOnTable == 12)
            {
                setNextMoveAndHitId(gameId);
                _callbackInterface.OnRoundOver(gameId, g.MovesIndex, g.HitsIndex);
            }
            else {
                _callbackInterface.OnHitMade(movedCard, gameId, g.HitsIndex);
            }
        }


        public void pickUp(int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            g.PickedUp = true;
            g.Players[g.HitsIndex].CardsInHand += g.NrOfCardsOnTable;
            g.NrOfCardsOnTable = 0;
            setNextMoveAndHitId(gameId);
            _callbackInterface.OnPickUp(gameId, g.HitsIndex);
            if (!isGameOver(gameId))
            {
                _callbackInterface.OnRoundOver(gameId, g.MovesIndex, g.HitsIndex);
            }
            else
                _callbackInterface.OnGameOver(gameId, g.HitsIndex);
        }
    }


    public class Game {
        private List<ServiceUser> joiners;
         private int id;
         private ServiceUser[] players;
         private int count=1;
        private int state; // 0 -> not started 
        private string owner;
        private ServiceCard[] deck;
        private int _topCardIndex;
        private int movesIndex;
        private int hitsIndex;
        private int _nrOfCardsOnTable = 0;
        private bool _pickedUp = false;

        public bool PickedUp
        {
            get { return _pickedUp; }
            set { _pickedUp = value; }
        }

        public int NrOfCardsOnTable
        {
            get { return _nrOfCardsOnTable; }
            set { _nrOfCardsOnTable = value; }
        }
        public int HitsIndex
        {
            get { return hitsIndex; }
            set { hitsIndex = value; }
        }

        public int MovesIndex
        {
            get { return movesIndex; }
            set { movesIndex = value; }
        }

        public int TopCardIndex
        {
            get { return _topCardIndex; }
            set { _topCardIndex = value; }
        }

        public ServiceCard[] Deck
        {
            get { return deck; }
            set { deck = value; }
        }

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public Game(List<ServiceUser> join, int id, int st)
        {
            joiners = join;
            this.id = id;
            state = st;
            ServiceUser first = new ServiceUser(owner,0);
            players = new ServiceUser[6];
            deck = new ServiceCard[36];

        }

        public List<ServiceUser> Joiners
        {
            get { return joiners; }
            set { joiners = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public ServiceUser[] Players
        {
            get { return players; }
            set { players = value; }
        }
       
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        

        public int State
        {
            get { return state; }
            set { state = value; }
        }
        public Game()
        {

        }
    }

    public class ServiceUser {
        private string _name;
        private int _id;
        private int _cardsInHand;
        private bool _finished = false;

        public bool Finished
        {
            get { return _finished; }
            set { _finished = value; }
        }

        public int CardsInHand
        {
            get { return _cardsInHand; }
            set { _cardsInHand = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public ServiceUser() { }
        public ServiceUser(string name) {
            _name = name;
        }
        public ServiceUser(string name, int id)
        {
            _name = name;
            _id = id;
        }
    }

   public class ServiceCard
        {
            private int _rank;
            private int _kind;

            public int Kind
            {
                get { return _kind; }
                set { _kind = value; }
            }

            public int Rank
            {
                get { return _rank; }
                set { _rank = value; }
            }
            public ServiceCard(int rank, int kind) {
                _rank = rank;
                _kind = kind;
            }
            public ServiceCard() { }
        }
    
}
