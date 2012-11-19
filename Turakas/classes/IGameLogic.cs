using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turakas.classes
{
    interface IGameLogic
    {
        void deal();
        Player makesNextMove();
        Player makesNextHit();
        Player madeLastMove();
        bool isLegalHit(Card target, Card hit);
        bool isLegalMove(Card card);
        bool isRoundComplete();
    }
}
