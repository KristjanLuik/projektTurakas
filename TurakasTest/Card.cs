using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace TurakasTest
{
    public enum CardRank { six = 6, seven, eight, nine, ten, jack, queen, king, ace }
    public enum Kind { c = 1, s, d, h }

    public class Card
    {
        private Kind _kind;
        private CardRank _rank;
        private bool _trump;
        private System.Windows.Controls.Image _image;

        public System.Windows.Controls.Image Image
        {
            get { return _image; }
            set
            {

                _image = value;

            }
        }
        public Kind Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        public CardRank Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public bool Trump
        {
            get { return _trump; }
            set { _trump = value; }
        }
        public Card() { }
        public Card(Kind kind, CardRank rank, bool trump)
        {
            _trump = trump;
            _kind = kind;
            _rank = rank;
            _image = new Image();
            _image.Stretch = Stretch.Fill;

            //Uri uri = new Uri("/images/c14.png", UriKind.Relative);
            //System.Diagnostics.Debug.WriteLine(uri);
            //_image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(uri);
            //"images/"+_kind.ToString()+_rank.ToString()+".png", UriKind.Relative)); ;
        }
        public Card(int kind, int rank)
        {
            _kind = (Kind)kind;
            _rank = (CardRank)rank;
            _image = new Image();
            _image.Stretch = Stretch.Fill;
        }
        public Card(Image im)
        {
            string name = im.Name;
            name = name.Trim();
            string kind = name.Substring(1, 1);
            Kind kindVal = (Kind)Enum.Parse(typeof(Kind), kind);
            string rank = name.Substring(2);
            CardRank rankVal = (CardRank)Enum.Parse(typeof(CardRank), rank);
            _kind = kindVal;
            _rank = rankVal;
            _image = new Image();
            _image.Stretch = Stretch.Fill;
        }

    }
}