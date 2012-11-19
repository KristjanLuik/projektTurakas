using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;


namespace Turakas.classes
{
    public enum CardRank {six=6, seven, eight, nine, ten, jack, queen, king, ace}
    public enum Kind {c=1, s, d, h}

    public class Card
    {
        private Kind _kind;
        private CardRank _rank;
        private bool _trump;
        private Windows.UI.Xaml.Controls.Image _image;

        public Windows.UI.Xaml.Controls.Image Image
        {
            get { return _image; }
            set { _image = value; }
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

        
    }
}
