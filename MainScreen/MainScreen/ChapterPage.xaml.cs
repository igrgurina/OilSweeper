using System.Collections.ObjectModel;
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
            HardwareButtons.BackPressed += OnBackPressed;
        }

        private SlideData data = new SlideData();
        //public ObservableCollection<ChapterViewModel> Chapters = new ObservableCollection<ChapterViewModel>();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            data.Chapter = (ChapterViewModel)e.Parameter;
            DataContext = (ChapterViewModel)e.Parameter;
        }

        private void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            Frame frame = Window.Current.Content as Frame;

            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        private void quiz_image_Tapped(object sender, TappedRoutedEventArgs e) {
            Frame.Navigate(typeof(QuizPage));
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
