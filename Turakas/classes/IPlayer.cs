using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurakasLibrary
{
    /// <summary>
    /// Interface for descibing players actions
    /// </summary>
    interface IPlayer
    {
        bool makeMove(Card card);
        List<Card> pickUp();
        bool hit(Card target,Card hit);
        void updateHand(List<Card> arg);
        bool hasCards();

    }
}
