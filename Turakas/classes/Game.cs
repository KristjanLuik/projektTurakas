using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurakasLibrary
{
    public class Game
    {
        private List<Player> _players;
        private Deck _deck;
        private int _id;
        private static Player _current;

        #region properties
        public static void setCurrentPlayer(Player current) {
            _current = current;
        }
        public static Player getCurrentPlayer()
        {
            return _current;
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        internal Deck Deck
        {
            get { return _deck; }
            set { _deck = value; }
        }

        public List<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        #endregion

        
    }
}
