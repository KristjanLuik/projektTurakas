using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TurakasServiceLibrary
{

    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface IServiceInterface
    {
        [OperationContract]
        long Subscribe(string nickname);
        [OperationContract]
        void setSubscriberGameRef(int id);
        [OperationContract]
        List<ServiceUser> getJoinersList(int gameid);
        [OperationContract]
        int createGame(string gameOwner);
        [OperationContract]
        bool doesGameExist(string gameOwner);
        [OperationContract]
        Game[] sendGameList();
        [OperationContract]
        void addPlayersToGame(int gameid, ServiceUser[] pl);
        [OperationContract]
        void registerPlayerCandidate(int Gameid, string candidateName);
        [OperationContract]
        ServiceUser[] getJoinCandidates(int gameid);
        [OperationContract]
        void shuffle(int gameid);
        [OperationContract]
        void initGameDeck(int gameid);
        //[OperationContract]
        //void setCallbackInterface(IServiceCallbackInterface callbackInterface);
        [OperationContract]
        void dealCards(int gameid);
        [OperationContract]
        void dealRound(int gameId);
        [OperationContract]
        void removeFromGame(int gameId, long id);
        [OperationContract]
        void notifyFirstMove(int gameId);
        [OperationContract]
        void notifyMove(ServiceCard cardMoved, long playerId, int gameId);
        [OperationContract]
        void setNextMoveAndHitId(int gameId);
        [OperationContract]
        void notifyHitMade(int gameId, ServiceCard movedCard);
        [OperationContract]
        void pickUp(int gameId);
        [OperationContract]
        void turnPage(int gameId);

    }
    [DataContract]
    public class Game
    {
        private List<ServiceUser> joiners;
        private int id;
        private ServiceUser[] players;
        private int count = 1;
        private int state; // 0 -> not started 
        private string owner;
        private ServiceCard[] deck;
        private int _topCardIndex;
        private int movesIndex;
        private int hitsIndex;
        private int _nrOfCardsOnTable = 0;
        private bool _pickedUp = false;

        [DataMember]
        public bool PickedUp
        {
            get { return _pickedUp; }
            set { _pickedUp = value; }
        }
        [DataMember]
        public int NrOfCardsOnTable
        {
            get { return _nrOfCardsOnTable; }
            set { _nrOfCardsOnTable = value; }
        }
        [DataMember]
        public int HitsIndex
        {
            get { return hitsIndex; }
            set { hitsIndex = value; }
        }
        [DataMember]
        public int MovesIndex
        {
            get { return movesIndex; }
            set { movesIndex = value; }
        }
        [DataMember]
        public int TopCardIndex
        {
            get { return _topCardIndex; }
            set { _topCardIndex = value; }
        }
        [DataMember]
        public ServiceCard[] Deck
        {
            get { return deck; }
            set { deck = value; }
        }
        [DataMember]
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        [DataMember]
        public List<ServiceUser> Joiners
        {
            get { return joiners; }
            set { joiners = value; }
        }
        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [DataMember]
        public ServiceUser[] Players
        {
            get { return players; }
            set { players = value; }
        }
        [DataMember]
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        [DataMember]
        public int State
        {
            get { return state; }
            set { state = value; }
        }

    }
    [DataContract]
    public class ServiceUser
    {
        private string _name;
        private long _id;
        private int _cardsInHand;
        private bool _finished = false;

        [DataMember]
        public bool Finished
        {
            get { return _finished; }
            set { _finished = value; }
        }
        [DataMember]
        public int CardsInHand
        {
            get { return _cardsInHand; }
            set { _cardsInHand = value; }
        }
        [DataMember]
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string toString()
        {
            return this.Name;
        }
    }
    [DataContract]
    public class ServiceCard
    {
        private int _rank;
        private int _kind;
        [DataMember]
        public int Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }
        [DataMember]
        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

    }


    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnNotifyFirstMove(long firstId, long hitId, int gameId);
        [OperationContract(IsOneWay = true)]
        void OnDeal(ServiceCard[] cards, ServiceCard trump, long playerId, int gameId);
        [OperationContract(IsOneWay = true)]
        void OnNotifyMove(ServiceCard movedCard, int gameId, long playerId, long nextHit);
        [OperationContract(IsOneWay = true)]
        void OnPlayerFinished(int gameId, long playerId);
        [OperationContract(IsOneWay = true)]
        void OnGameOver(int gameId, long loserId);
        //[OperationContract(IsOneWay = true)]
        //void OnGameEnded(int gameId);
        [OperationContract(IsOneWay = true)]
        void OnHitMade(ServiceCard movedCard, int gameId, long playerId);
        [OperationContract(IsOneWay = true)]
        void OnRoundOver(int gameId, long newMoveId, long newHitId);
        [OperationContract(IsOneWay = true)]
        void OnPickUp(int gameId, long looserId);
        [OperationContract(IsOneWay = true)]
        void OnGameStart(int gameId);
        [OperationContract(IsOneWay = true)]
        void OnPlayersAdded(int gameId, ServiceUser[] players);
    }
}
