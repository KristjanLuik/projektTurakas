using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turakas.classes
{
    class GameLogic:IGameLogic
    {
        private Deck _deck;
        private List<Player> _players;//ordered by id-s
        private List<Card> _cardsOnTable;
        public GameLogic()
        {
        }



        /// <summary>
        /// The first player to get cards is the player that made the last move. Next will be
        /// the player berore him (player whose id is less) 
        /// </summary>
        public void deal()
        {
            int active;
            Player current = madeLastMove(); // case where no moves have been made is to be covered in referenced method
            active = _players.IndexOf(current);
            int round = 0;
            while (round < _players.Count())
            {
                while (current.Hand.Count() < 6 && !_deck.isEmpty())
                {
                    current.Hand.Add(_deck.remove());
                }
                active = (active > 0 ? active - 1 : _players.Count() - 1);
                current = _players.ElementAt(active);
                round += 1;
            }
        }

        public Player makesNextMove()
        {
            throw new NotImplementedException();
        }

        public Player makesNextHit()
        {
            throw new NotImplementedException();
        }

        public Player madeLastMove()
        {
            throw new NotImplementedException();
        }

        public bool isLegalHit(Card target, Card hit)
        {
            return ((target.Kind.Equals(hit.Kind) && target.Rank < hit.Rank) || (!target.Kind.Equals(hit.Kind) && hit.Trump));
        }

        public bool isLegalMove(Card card)
        {
            if (_cardsOnTable.Count == 0)
                return true;
            else { 
                foreach(Card c in _cardsOnTable){
                    if (c.Rank.Equals(card.Rank))
                        return true;
                }
                return false;
            }
        }

        public bool isRoundComplete()
        {
            throw new NotImplementedException();
        }
       
    }
}
