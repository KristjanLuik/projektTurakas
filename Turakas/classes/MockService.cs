using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //this._callbackInterface.doSomething("Plah!");
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
            //Random random = new Random();
            //int playerId = random.Next(0, g.Count);
            //_callbackInterface.OnNotifyFirstMove(playerId);
            //TODO, LISA GAMEID KONTROLL
            _callbackInterface.OnNotifyFirstMove(0,g.Id);
        }


        public void notifyMove(ServiceCard cardMoved, int playerId, int gameId)
        {
            Game g = listOfGames.ElementAt(gameId - 1);
            g.Players[playerId].CardsInHand -= 1;
            if (g.Players[playerId].CardsInHand == 0 && g.TopCardIndex <= 0) {
                g.Players[playerId].Finished = true;
            }
            int next;
            if(playerId == g.Count-1)
                next = 0;
            else
                next = playerId+1;
            _callbackInterface.OnNotifyMove(cardMoved, gameId, playerId, g.Players[playerId].Finished, next);//seepärast peakski teenusele lisaks olema back endis veel üks klass
            _callbackInterface.OnNotifyMove(new ServiceCard(10,1), 1, 1, g.Players[playerId].Finished, next);
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
        private bool _finished;

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
