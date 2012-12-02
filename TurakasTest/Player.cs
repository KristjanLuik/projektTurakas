using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Runtime.CompilerServices;

namespace TurakasTest
{
    public class Player : INotifyPropertyChanged
    {
        private ObservableCollection<Card> _hand;
        private string _name;
        private long _id;
        private string _message;
        public event PropertyChangedEventHandler PropertyChanged;
        private Color _color;
        private string _uri = "ms-appx:///Assets/images/b2fv.png";
        private System.Windows.Visibility _visible = System.Windows.Visibility.Visible;
        private System.Windows.Visibility _finished = System.Windows.Visibility.Collapsed;

        public System.Windows.Visibility Finished
        {
            get { return _finished; }
            set
            {
                if (!value.Equals(_finished))
                {
                    _finished = value;
                    NotifyPropertyChanged("Finished");
                }
            }
        }
        public System.Windows.Visibility Visible
        {
            get { return _visible; }
            set
            {
                if (!value.Equals(_visible))
                {
                    _visible = value;
                    NotifyPropertyChanged("Visible");
                }
            }
        }

        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region propertid

        public Color Color
        {
            get { return _color; }
            set
            {
                if (!value.Equals(_color))
                {
                    _color = value;
                    NotifyPropertyChanged("Color");
                }
            }
        }


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

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public ObservableCollection<Card> Hand
        {
            get { return _hand; }
            set
            {
                if (!value.Equals(_hand))
                {
                    _hand = value;
                    NotifyPropertyChanged("Hand");
                }
            }
        }
        #endregion
        public Player() { }
        public Player(string name)
        {
            _name = name;
            _hand = new ObservableCollection<Card>();
            _color = new Color();
        }

        public Player(string p1, long p2)
        {
            // TODO: Complete member initialization
            this.Name = p1;
            this.Id = p2;
            _hand = new ObservableCollection<Card>();
            _color = new Color();
        }

        public void makeMove(Card card)
        {
            foreach (Card c in _hand)
            {
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

