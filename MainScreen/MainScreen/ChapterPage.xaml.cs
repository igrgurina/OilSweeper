using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MainScreen.ViewModel;

namespace MainScreen
{
    public sealed partial class ChapterPage : Page
    {
        public ChapterPage()
        {
            InitializeComponent();
        }

        public ObservableCollection<ChapterViewModel> Chapters = new ObservableCollection<ChapterViewModel>();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = (ChapterViewModel)e.Parameter;
        }

        private void back_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(EducationPage));
        }

        private void quiz_image_Tapped(object sender, TappedRoutedEventArgs e) {
            Frame.Navigate(typeof(QuizPage));
        }

        private void Slide_Click(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                Frame.Navigate(typeof(SlidePage), e.ClickedItem);
            }
        }
    }
}
