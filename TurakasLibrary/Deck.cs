using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurakasLibrary
{
   public class Deck
    {
        private Card[] deck;
        Kind _trump;
        private int _deckSize;
        private int _topCardIndex;
        public Kind Trump
        {
            get { return _trump; }
        }

         public Deck(){
            deck = new Card[_deckSize];
            var kindValues = Enum.GetValues(typeof(Kind));
            var rankValues = Enum.GetValues(typeof(CardRank));
            setTrump();
            int cnt = 0;
             foreach(Kind k in kindValues){
                 foreach (CardRank r in rankValues)
                 {
                     deck[cnt]=(new Card(k, r, k.Equals(_trump)));
                 }   
             }
             _topCardIndex = deck.Length - 1;
        }//constructor

         public void setTrump() { 
            Random r = new Random();
            int i = r.Next(4);
            switch (i)
            {
                case 1:
                    _trump = Kind.c; break;
                case 2:
                    _trump = Kind.d; break;
                case 3:
                    _trump = Kind.h; break;
                default:
                    _trump = Kind.s; break;
            }
         }
        /// <summary>
         /// Fisher and Yates' method
        /// </summary>
         public void shuffle()
         {
             Card tmp = new Card();
             Random r = new Random();
             int randomIndex;
             for (int j = deck.Count() - 1; j>0; j--)
             {
                 randomIndex = r.Next(deck.Count() - j);
                 tmp = deck[j];
                 deck[j] = deck[randomIndex];
                 deck[randomIndex] = tmp;
             }
         }

         public Card remove() { 
             _topCardIndex -= 1;
             return deck[_topCardIndex + 1];
         }

         public bool isEmpty() {
             return _topCardIndex < 0;
         }
    }
}
