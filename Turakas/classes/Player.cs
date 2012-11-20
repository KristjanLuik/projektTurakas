using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Turakas.classes
{
    public class Player : IPlayer, INotifyPropertyChanged
    {
        private ObservableCollection<Card> _hand;
        private string _name;
        private int _id;
        private string _message;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region propertid

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public ObservableCollection<Card> Hand
        {
            get { return _hand; }
            set { 
                if(!value.Equals(_hand)){
                    _hand = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
        public Player() { }
        public Player(string name)
        {
            _name = name;
            _hand = new ObservableCollection<Card>();
        }

        public Player(string p1, int p2)
        {
            // TODO: Complete member initialization
            this.Name = p1;
            this.Id = p2;
            _hand = new ObservableCollection<Card>();
        }
        
        public void makeMove(Card card)
        {
            foreach (Card c in _hand) {
                if (card.Kind.Equals(c.Kind) && card.Rank.Equals(c.Rank))
                {
                    Hand.Remove(c);
                    return;
                }
            }
        }
        /// <summary>
        /// Checks wether player is to hit and if so allowes to pick up by clearing the game area.
        /// </summary>
        /// <returns>List of picked up cards</returns>
        public ObservableCollection<Card> pickUp()
        {
            //TODO:
            return this._hand;
        }
        /// <summary>
        /// If allowed moves acceptable card on top of the previous card on the gameArea 
        /// </summary>
        /// <returns>bool value indicateing wether the hit is made</returns>
        public bool hit(Card target, Card hit)
        {
            //TODO:
            return false;
        }
        /// <summary>
        /// removes cards from Hand or adds cards to it.
        /// </summary>
        /// <param name="arg">cards to add or remove </param>
        public void updateHand(ObservableCollection<Card> arg)
        {

        }
        /// <summary>
        /// Checks wether player has cards left
        /// </summary>
        /// <returns>true, if player has at least 1 card; false otherwise</returns>
        public bool hasCards()
        {
            return false;
        }

    }
}
