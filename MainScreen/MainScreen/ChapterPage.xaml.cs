using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MainScreen.ViewModel;
using MainScreen.Extensions;
using Windows.Phone.UI.Input;

namespace MainScreen
{
    public sealed partial class ChapterPage : Page
    {
        public ChapterPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private SlideData data = new SlideData();
        public ObservableCollection<ChapterViewModel> Chapter = new ObservableCollection<ChapterViewModel>();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data.Chapter = (ChapterViewModel)e.Parameter;
            DataContext = (ChapterViewModel)e.Parameter;
            //HardwareButtons.BackPressed += OnBackPressed;
        }

        /*protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            HardwareButtons.BackPressed -= OnBackPressed;
        }*/


        private void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;

            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        private void back_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;

            if (frame != null && frame.CanGoBack)
            {
                //var x = Frame.BackStack.LastOrDefault();
                e.Handled = true;
                frame.GoBack();
                //var y = Frame.BackStack.LastOrDefault();
                //var a = 2;
            }
        }

        private void quiz_image_Tapped(object sender, TappedRoutedEventArgs e) {
            Frame.Navigate(typeof(QuizPage), ((ChapterViewModel)DataContext).Questions);
        }

        private void Slide_Click(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                data.Slide = (SlideViewModel) e.ClickedItem;
                Frame.Navigate(typeof(SlidePage), data);
            }
        }
    }
}
