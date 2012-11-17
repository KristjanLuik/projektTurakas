using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TurakasServiceLibrary
{

    [ServiceContract(CallbackContract = typeof(ITeenusCallback))]
    public interface ITeenus
    {
        [OperationContract]
        ServicePlayer[] getSubscribers(ServiceGame game);

        [OperationContract]
        int[] getNewGameAndPlayerId(ServicePlayer p);

        [OperationContract]
        void addPlayerToGame(ServiceGame game, string newPlayer);

        [OperationContract]
        ServiceCard hit(ServiceCard target, ServiceCard hit);

        [OperationContract]
        ServiceCard move(int card_rank, int card_kind);

        [OperationContract]
        ServiceCard subscribeTogame(int game, string name);

        [OperationContract]
        void sendChatMessage(string message, int gameId, int fromID);

        [OperationContract]
        void Subscribe(string user);

        [OperationContract]
        void Unsubscribe();

        [OperationContract]
        void gameEnded();
    }
    #region  data contract

    [DataContract]
    public class ServiceGame
    {
        private int _ID;
        private ServicePlayer[] _players;
        private int _count;
        private Subscriber[] _subscribers;

        [DataMember]
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        [DataMember]
        public ServicePlayer[] Players
        {
            get { return _players; }
            set { _players = value; }
        }

        [DataMember]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        
    }

    [DataContract]
    public class ServicePlayer 
    {
        private string _nickName;
        private int _id;
        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [DataMember]
        public string Name
        {
            get { return _nickName; }
            set { _nickName = value; }
        }
    }
    [DataContract]
    public class ServiceCard{
        
        private int _kind;
        private int _rank;
        [DataMember]
        public int Rank
        {
         get { return _rank; }
         set { _rank = value; }
        }
        [DataMember]
        public int Kind
        {
          get { return _kind; }
          set { _kind = value; }
        }
    }

    #endregion

    public interface ITeenusCallback
    {
        [OperationContract(IsOneWay = true)]
        void onChatMessage(string from, string message);

        [OperationContract(IsOneWay = true)]
        void onGetContacts(string[] contacts);

        [OperationContract(IsOneWay = true)]
        void onHit(ServiceCard hit);

        [OperationContract(IsOneWay = true)]
        void onMove(ServiceCard card);
    }
}
