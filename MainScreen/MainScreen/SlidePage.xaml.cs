using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MainScreen.Extensions;
using MainScreen.ViewModel;
using Windows.Phone.UI.Input;
using MainScreen.Helpers;

namespace MainScreen
{
    public sealed partial class SlidePage : Page
    {
        private int x1, x2;
        private SlideData data = new SlideData();
        public SlidePage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            new Swiper(MainPanel, OnSwipedLeft, OnSwipedRight);
            //HardwareButtons.BackPressed += OnBackPressed;
        }


        private void OnSwipedRight()
        {
            
        }

        private void OnSwipedLeft()
        {
            
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data = (SlideData)e.Parameter;
            DataContext = data.Slide;
        }

        private void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            Frame.GoBack();
        }

        private void back_Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var reverse = data.Chapter.Slides.ToList();
            reverse.Reverse();
            SlideViewModel previous = reverse.SkipWhile(c => c != data.Slide).Skip(1).FirstOrDefault();
            if (previous == null)
            {
                Frame.GoBack();
            }
            else
            {
                data.Slide = previous;
                Frame.ChangeContext(this, data.Slide);
            }
        }

        private void front_Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SlideViewModel next = data.Chapter.Slides.SkipWhile(c => c != data.Slide).Skip(1).FirstOrDefault();
            if (next == null)
            {
                Frame.GoBack();
            }
            else
            {
                data.Slide = next;
                Frame.ChangeContext(this, data.Slide);

            }
        }

        private void return_Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
