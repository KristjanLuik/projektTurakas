using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        void makeMove(Card card);
        ObservableCollection<Card> pickUp();
        bool hit(Card target,Card hit);
        void updateHand(ObservableCollection<Card> arg);
        bool hasCards();

    }
}
