﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TurakasTest.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IServiceInterface", CallbackContract=typeof(TurakasTest.ServiceReference1.IServiceInterfaceCallback))]
    public interface IServiceInterface {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/Subscribe", ReplyAction="http://tempuri.org/IServiceInterface/SubscribeResponse")]
        void Subscribe(string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/Subscribe", ReplyAction="http://tempuri.org/IServiceInterface/SubscribeResponse")]
        System.Threading.Tasks.Task SubscribeAsync(string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/setSubscriberGameRef", ReplyAction="http://tempuri.org/IServiceInterface/setSubscriberGameRefResponse")]
        void setSubscriberGameRef(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/setSubscriberGameRef", ReplyAction="http://tempuri.org/IServiceInterface/setSubscriberGameRefResponse")]
        System.Threading.Tasks.Task setSubscriberGameRefAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/getJoinersList", ReplyAction="http://tempuri.org/IServiceInterface/getJoinersListResponse")]
        TurakasServiceLibrary.ServiceUser[] getJoinersList(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/getJoinersList", ReplyAction="http://tempuri.org/IServiceInterface/getJoinersListResponse")]
        System.Threading.Tasks.Task<TurakasServiceLibrary.ServiceUser[]> getJoinersListAsync(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/createGame", ReplyAction="http://tempuri.org/IServiceInterface/createGameResponse")]
        int createGame(string gameOwner);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/createGame", ReplyAction="http://tempuri.org/IServiceInterface/createGameResponse")]
        System.Threading.Tasks.Task<int> createGameAsync(string gameOwner);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/doesGameExist", ReplyAction="http://tempuri.org/IServiceInterface/doesGameExistResponse")]
        bool doesGameExist(string gameOwner);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/doesGameExist", ReplyAction="http://tempuri.org/IServiceInterface/doesGameExistResponse")]
        System.Threading.Tasks.Task<bool> doesGameExistAsync(string gameOwner);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/sendGameList", ReplyAction="http://tempuri.org/IServiceInterface/sendGameListResponse")]
        TurakasServiceLibrary.Game[] sendGameList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/sendGameList", ReplyAction="http://tempuri.org/IServiceInterface/sendGameListResponse")]
        System.Threading.Tasks.Task<TurakasServiceLibrary.Game[]> sendGameListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/addPlayersToGame", ReplyAction="http://tempuri.org/IServiceInterface/addPlayersToGameResponse")]
        TurakasServiceLibrary.ServiceUser[] addPlayersToGame(int gameid, TurakasServiceLibrary.ServiceUser[] pl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/addPlayersToGame", ReplyAction="http://tempuri.org/IServiceInterface/addPlayersToGameResponse")]
        System.Threading.Tasks.Task<TurakasServiceLibrary.ServiceUser[]> addPlayersToGameAsync(int gameid, TurakasServiceLibrary.ServiceUser[] pl);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/registerPlayerCandidate", ReplyAction="http://tempuri.org/IServiceInterface/registerPlayerCandidateResponse")]
        void registerPlayerCandidate(int Gameid, string candidateName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/registerPlayerCandidate", ReplyAction="http://tempuri.org/IServiceInterface/registerPlayerCandidateResponse")]
        System.Threading.Tasks.Task registerPlayerCandidateAsync(int Gameid, string candidateName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/getJoinCandidates", ReplyAction="http://tempuri.org/IServiceInterface/getJoinCandidatesResponse")]
        TurakasServiceLibrary.ServiceUser[] getJoinCandidates(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/getJoinCandidates", ReplyAction="http://tempuri.org/IServiceInterface/getJoinCandidatesResponse")]
        System.Threading.Tasks.Task<TurakasServiceLibrary.ServiceUser[]> getJoinCandidatesAsync(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/shuffle", ReplyAction="http://tempuri.org/IServiceInterface/shuffleResponse")]
        void shuffle(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/shuffle", ReplyAction="http://tempuri.org/IServiceInterface/shuffleResponse")]
        System.Threading.Tasks.Task shuffleAsync(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/initGameDeck", ReplyAction="http://tempuri.org/IServiceInterface/initGameDeckResponse")]
        void initGameDeck(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/initGameDeck", ReplyAction="http://tempuri.org/IServiceInterface/initGameDeckResponse")]
        System.Threading.Tasks.Task initGameDeckAsync(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/dealCards", ReplyAction="http://tempuri.org/IServiceInterface/dealCardsResponse")]
        void dealCards(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/dealCards", ReplyAction="http://tempuri.org/IServiceInterface/dealCardsResponse")]
        System.Threading.Tasks.Task dealCardsAsync(int gameid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/dealRound", ReplyAction="http://tempuri.org/IServiceInterface/dealRoundResponse")]
        void dealRound(int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/dealRound", ReplyAction="http://tempuri.org/IServiceInterface/dealRoundResponse")]
        System.Threading.Tasks.Task dealRoundAsync(int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/removeFromGame", ReplyAction="http://tempuri.org/IServiceInterface/removeFromGameResponse")]
        int removeFromGame(int gameId, TurakasServiceLibrary.ServiceUser player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/removeFromGame", ReplyAction="http://tempuri.org/IServiceInterface/removeFromGameResponse")]
        System.Threading.Tasks.Task<int> removeFromGameAsync(int gameId, TurakasServiceLibrary.ServiceUser player);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/notifyFirstMove", ReplyAction="http://tempuri.org/IServiceInterface/notifyFirstMoveResponse")]
        void notifyFirstMove(int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/notifyFirstMove", ReplyAction="http://tempuri.org/IServiceInterface/notifyFirstMoveResponse")]
        System.Threading.Tasks.Task notifyFirstMoveAsync(int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/notifyMove", ReplyAction="http://tempuri.org/IServiceInterface/notifyMoveResponse")]
        void notifyMove(TurakasServiceLibrary.ServiceCard cardMoved, int playerId, int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/notifyMove", ReplyAction="http://tempuri.org/IServiceInterface/notifyMoveResponse")]
        System.Threading.Tasks.Task notifyMoveAsync(TurakasServiceLibrary.ServiceCard cardMoved, int playerId, int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/setNextMoveAndHitId", ReplyAction="http://tempuri.org/IServiceInterface/setNextMoveAndHitIdResponse")]
        void setNextMoveAndHitId(int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/setNextMoveAndHitId", ReplyAction="http://tempuri.org/IServiceInterface/setNextMoveAndHitIdResponse")]
        System.Threading.Tasks.Task setNextMoveAndHitIdAsync(int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/notifyHitMade", ReplyAction="http://tempuri.org/IServiceInterface/notifyHitMadeResponse")]
        void notifyHitMade(int gameId, TurakasServiceLibrary.ServiceCard movedCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/notifyHitMade", ReplyAction="http://tempuri.org/IServiceInterface/notifyHitMadeResponse")]
        System.Threading.Tasks.Task notifyHitMadeAsync(int gameId, TurakasServiceLibrary.ServiceCard movedCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/pickUp", ReplyAction="http://tempuri.org/IServiceInterface/pickUpResponse")]
        void pickUp(int gameId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceInterface/pickUp", ReplyAction="http://tempuri.org/IServiceInterface/pickUpResponse")]
        System.Threading.Tasks.Task pickUpAsync(int gameId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceInterfaceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceInterface/OnNotifyFirstMove")]
        void OnNotifyFirstMove(int firstId, int hitId, int gameId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceInterface/OnDeal")]
        void OnDeal(TurakasServiceLibrary.ServiceCard[] cards, TurakasServiceLibrary.ServiceCard trump, int playerId, int gameId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceInterface/OnNotifyMove")]
        void OnNotifyMove(TurakasServiceLibrary.ServiceCard movedCard, int gameId, int playerId, int nextHit);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceInterface/OnPlayerFinished")]
        void OnPlayerFinished(int gameId, int playerId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceInterface/OnGameOver")]
        void OnGameOver(int gameId, int loserId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceInterface/OnHitMade")]
        void OnHitMade(TurakasServiceLibrary.ServiceCard movedCard, int gameId, int playerId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceInterface/OnRoundOver")]
        void OnRoundOver(int gameId, int newMoveId, int newHitId);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServiceInterface/OnPickUp")]
        void OnPickUp(int gameId, int looserId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceInterfaceChannel : TurakasTest.ServiceReference1.IServiceInterface, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceInterfaceClient : System.ServiceModel.DuplexClientBase<TurakasTest.ServiceReference1.IServiceInterface>, TurakasTest.ServiceReference1.IServiceInterface {
        
        public ServiceInterfaceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServiceInterfaceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServiceInterfaceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceInterfaceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceInterfaceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Subscribe(string nickname) {
            base.Channel.Subscribe(nickname);
        }
        
        public System.Threading.Tasks.Task SubscribeAsync(string nickname) {
            return base.Channel.SubscribeAsync(nickname);
        }
        
        public void setSubscriberGameRef(int id) {
            base.Channel.setSubscriberGameRef(id);
        }
        
        public System.Threading.Tasks.Task setSubscriberGameRefAsync(int id) {
            return base.Channel.setSubscriberGameRefAsync(id);
        }
        
        public TurakasServiceLibrary.ServiceUser[] getJoinersList(int gameid) {
            return base.Channel.getJoinersList(gameid);
        }
        
        public System.Threading.Tasks.Task<TurakasServiceLibrary.ServiceUser[]> getJoinersListAsync(int gameid) {
            return base.Channel.getJoinersListAsync(gameid);
        }
        
        public int createGame(string gameOwner) {
            return base.Channel.createGame(gameOwner);
        }
        
        public System.Threading.Tasks.Task<int> createGameAsync(string gameOwner) {
            return base.Channel.createGameAsync(gameOwner);
        }
        
        public bool doesGameExist(string gameOwner) {
            return base.Channel.doesGameExist(gameOwner);
        }
        
        public System.Threading.Tasks.Task<bool> doesGameExistAsync(string gameOwner) {
            return base.Channel.doesGameExistAsync(gameOwner);
        }
        
        public TurakasServiceLibrary.Game[] sendGameList() {
            return base.Channel.sendGameList();
        }
        
        public System.Threading.Tasks.Task<TurakasServiceLibrary.Game[]> sendGameListAsync() {
            return base.Channel.sendGameListAsync();
        }
        
        public TurakasServiceLibrary.ServiceUser[] addPlayersToGame(int gameid, TurakasServiceLibrary.ServiceUser[] pl) {
            return base.Channel.addPlayersToGame(gameid, pl);
        }
        
        public System.Threading.Tasks.Task<TurakasServiceLibrary.ServiceUser[]> addPlayersToGameAsync(int gameid, TurakasServiceLibrary.ServiceUser[] pl) {
            return base.Channel.addPlayersToGameAsync(gameid, pl);
        }
        
        public void registerPlayerCandidate(int Gameid, string candidateName) {
            base.Channel.registerPlayerCandidate(Gameid, candidateName);
        }
        
        public System.Threading.Tasks.Task registerPlayerCandidateAsync(int Gameid, string candidateName) {
            return base.Channel.registerPlayerCandidateAsync(Gameid, candidateName);
        }
        
        public TurakasServiceLibrary.ServiceUser[] getJoinCandidates(int gameid) {
            return base.Channel.getJoinCandidates(gameid);
        }
        
        public System.Threading.Tasks.Task<TurakasServiceLibrary.ServiceUser[]> getJoinCandidatesAsync(int gameid) {
            return base.Channel.getJoinCandidatesAsync(gameid);
        }
        
        public void shuffle(int gameid) {
            base.Channel.shuffle(gameid);
        }
        
        public System.Threading.Tasks.Task shuffleAsync(int gameid) {
            return base.Channel.shuffleAsync(gameid);
        }
        
        public void initGameDeck(int gameid) {
            base.Channel.initGameDeck(gameid);
        }
        
        public System.Threading.Tasks.Task initGameDeckAsync(int gameid) {
            return base.Channel.initGameDeckAsync(gameid);
        }
        
        public void dealCards(int gameid) {
            base.Channel.dealCards(gameid);
        }
        
        public System.Threading.Tasks.Task dealCardsAsync(int gameid) {
            return base.Channel.dealCardsAsync(gameid);
        }
        
        public void dealRound(int gameId) {
            base.Channel.dealRound(gameId);
        }
        
        public System.Threading.Tasks.Task dealRoundAsync(int gameId) {
            return base.Channel.dealRoundAsync(gameId);
        }
        
        public int removeFromGame(int gameId, TurakasServiceLibrary.ServiceUser player) {
            return base.Channel.removeFromGame(gameId, player);
        }
        
        public System.Threading.Tasks.Task<int> removeFromGameAsync(int gameId, TurakasServiceLibrary.ServiceUser player) {
            return base.Channel.removeFromGameAsync(gameId, player);
        }
        
        public void notifyFirstMove(int gameId) {
            base.Channel.notifyFirstMove(gameId);
        }
        
        public System.Threading.Tasks.Task notifyFirstMoveAsync(int gameId) {
            return base.Channel.notifyFirstMoveAsync(gameId);
        }
        
        public void notifyMove(TurakasServiceLibrary.ServiceCard cardMoved, int playerId, int gameId) {
            base.Channel.notifyMove(cardMoved, playerId, gameId);
        }
        
        public System.Threading.Tasks.Task notifyMoveAsync(TurakasServiceLibrary.ServiceCard cardMoved, int playerId, int gameId) {
            return base.Channel.notifyMoveAsync(cardMoved, playerId, gameId);
        }
        
        public void setNextMoveAndHitId(int gameId) {
            base.Channel.setNextMoveAndHitId(gameId);
        }
        
        public System.Threading.Tasks.Task setNextMoveAndHitIdAsync(int gameId) {
            return base.Channel.setNextMoveAndHitIdAsync(gameId);
        }
        
        public void notifyHitMade(int gameId, TurakasServiceLibrary.ServiceCard movedCard) {
            base.Channel.notifyHitMade(gameId, movedCard);
        }
        
        public System.Threading.Tasks.Task notifyHitMadeAsync(int gameId, TurakasServiceLibrary.ServiceCard movedCard) {
            return base.Channel.notifyHitMadeAsync(gameId, movedCard);
        }
        
        public void pickUp(int gameId) {
            base.Channel.pickUp(gameId);
        }
        
        public System.Threading.Tasks.Task pickUpAsync(int gameId) {
            return base.Channel.pickUpAsync(gameId);
        }
    }
}
