using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TurakasServiceLibrary
{
    
    public class Teenus : ITeenus
    {
        /** 
         * List of all subscribers to the chat service. Entries to this list are 
         * added by Subscribe() method and removed by Unsubscribe() method.
         */
        private static readonly List<Subscriber> subscrbers = new List<Subscriber>();
        /**
         *List to hold active Games 
         */
        private static readonly List<ServiceGame> games = new List<ServiceGame>();

        /** Get the subscriber entry for method caller */
        public Subscriber GetMe()
        {
            ITeenusCallback cb = OperationContext.
                                        Current.
                                            GetCallbackChannel<ITeenusCallback>();
            //return subscrbers.Single(item => item.callback == cb);
            foreach (Subscriber s in subscrbers)
            {
                if (s.callback.Equals(cb))
                    return s;
            }
            return null;
        }

        public ServicePlayer[] getSubscribers(ServiceGame game)
        {
            throw new NotImplementedException();
        }
  
        /// <summary>
        /// returns both, players id and new games id
        /// </summary>
        /// <param name="p"> player that initiates the game gets id 1 </param>
        /// <returns>Returns array id where id[0]= game id and id[1] = player id</returns>
        public int[] getNewGameId(ServicePlayer p)
        {
            ServiceGame g = new ServiceGame();
            g.Players = new ServicePlayer[6];
            g.Players[0] = p;
            g.Count = 1;
            g.ID = games.Count + 1;
            games.Add(g);
            int[] id = { g.ID, 1};
            return id;
        }

        public void addPlayerToGame(ServiceGame game, string newPlayer)
        {
            

        }

        public ServiceCard hit(ServiceCard target, ServiceCard hit)
        {
            throw new NotImplementedException();
        }


        public ServiceCard move(int card_rank, int card_kind)
        {
            throw new NotImplementedException();
        }




        public int[] getNewGameAndPlayerId(ServicePlayer p)
        {
            throw new NotImplementedException();
        }

        public ServiceCard subscribeTogame(int game, string name)
        {
            throw new NotImplementedException();
        }

        public void sendChatMessage(string message, int gameId, int fromID)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(string user)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe()
        {
            throw new NotImplementedException();
        }

        public void gameEnded()
        {
            throw new NotImplementedException();
        }
    }
    public class Subscriber
    {
        /* Nickname for subscriber */
        public string nickname;
        /* Interface for message notify */
        public ITeenusCallback callback;

        public Subscriber(string nickname)
        {
            this.nickname = nickname;
            this.callback = OperationContext.
                                Current.
                                    GetCallbackChannel<ITeenusCallback>();
        }

        public bool IsChannelOpen()
        {
            return ((ICommunicationObject)callback).State == CommunicationState.Opened;
        }
    }
}
