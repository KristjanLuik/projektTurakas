using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Turakas.ServiceReference1;

namespace TurakasLibrary
{
    public class Player: IPlayer, ITeenusCallback
    {
        private List<Card> _hand;
        private string _name;

       
        private int _id;
        private string _message;
        
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

        public List<Card> Hand
        {
            get { return _hand; }
            set { _hand = value; }
        }
        #endregion
        public Player() { }
        public Player(string name, int id)
        {
            _name = name;
            _id = id;
        }
        /// <summary>
        /// If  allowed moves one card to gameArea and updates Hand property 
        /// </summary>
        /// <returns>bool value indicateing wether the move is made</returns>
        public bool makeMove(Card card)
        {
            //TODO:
            return false;
        }
        /// <summary>
        /// Checks wether player is to hit and if so allowes to pick up by clearing the game area.
        /// </summary>
        /// <returns>List of picked up cards</returns>
        public List<Card> pickUp()
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
        public void updateHand(List<Card> arg)
        {

        }
        /// <summary>
        /// Checks wether player has cards left
        /// </summary>
        /// <returns>true, if player has at least 1 card; false otherwise</returns>
        public bool hasCards() {
            return false;
        }
        #region callback implementation

        public void onChatMessage(string from, string message)
        {
            throw new NotImplementedException();
        }

        public void onGetContacts(ObservableCollection<string> contacts)
        {
            throw new NotImplementedException();
        }

        public void onHit(ServiceCard hit)
        {
            throw new NotImplementedException();
        }

        public void onMove(ServiceCard card)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
