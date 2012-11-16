using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TurakasLibrary;
using System.Text;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turakas.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Player pl;

        public MainPage()
        {
            this.InitializeComponent();
            pl = new Player();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            addImagesToCards();

            foreach (Card kaart in pl.Hand)
            {
                gwPl1Hand.Items.Add(kaart.Image);
            }
            gwPl1Hand.CanDragItems = true;
            gwPl1Hand.CanReorderItems = true;
            gwPl1Hand.AllowDrop = true;
            
        }
        /// <summary>
        /// Add images to players cards
        /// </summary>
        public void addImagesToCards()
        {
            foreach (Card kaart in pl.Hand)
            {
                StringBuilder fileName = new StringBuilder();
                fileName.Append(@"/Assets/images/");
                fileName.Append(kaart.Kind.ToString());
                fileName.Append(((int)kaart.Rank).ToString());
                fileName.Append(".png");
                string fn = fileName.ToString();
                kaart.Image.Source = ImageFromRelativePath(this, fn);
            }

        }

        /// <summary>
        /// Thank you StackOverFlow! Uses relative URI string to get the BitmapImage from file.
        /// </summary>
        /// <param name="parent">FrameworkElement assotiated with image</param>
        /// <param name="path">String representing relative URI</param>
        /// <returns></returns>
        public static Windows.UI.Xaml.Media.Imaging.BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            Windows.UI.Xaml.Media.Imaging.BitmapImage result = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            result.UriSource = uri;
            return result;
        }

    }
}
