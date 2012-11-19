using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turakas.classes
{
    public interface IServiceInterface
    {
        List<ServiceUser> getJoinersList(int gameid);
        int createGame(string gameOwner);
        Game[] sendGameList();
        ServiceUser[] addPlayersToGame(int gameid, ServiceUser[] pl);
        void registerPlayerCandidate(int Gameid, string candidateName);
        List<ServiceUser> getJoinCandidates(int gameid);
        void shuffle(int gameid);
        void initGameDeck(int gameid);
        void setCallbackInterface(IServiceCallbackInterface callbackInterface);
        void dealCards(int gameid);
        int removeFromGame(int gameId, ServiceUser player);
        void notifyFirstMove(int gameId);

    }

    public interface IServiceCallbackInterface
    {
        void OnNotifyFirstMove(int id);
        void OnDeal(ServiceCard[] cards, ServiceCard trump, int playerId);
    }
}
