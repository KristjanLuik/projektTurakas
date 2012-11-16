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

        public int getNewGameId(ServicePlayer p)
        {
            ServiceGame g = new ServiceGame();
            g.Players = new ServicePlayer[6];
            g.Players[0] = p;
            g.Count = 1;
            g.ID = games.Count + 1;
            games.Add(g);
            return g.ID;
        }

        public void addPlayerToGame(ServiceGame game)
        {
            throw new NotImplementedException();
        }

        public ServiceCard hit(ServiceCard target, ServiceCard hit)
        {
            throw new NotImplementedException();
        }


        public ServiceCard move(ServiceCard card)
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
