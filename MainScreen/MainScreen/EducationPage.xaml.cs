using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MainScreen.ViewModel;
using Windows.Phone.UI.Input;

namespace MainScreen
{
    public sealed partial class EducationPage : Page
    {
        public EducationPage()
        {
            InitializeComponent();
            HardwareButtons.BackPressed += OnBackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = (EducationViewModel) e.Parameter;
        }

        private void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            Frame.Navigate(typeof(MainPage));          
        }

        private void quiz_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // TODO: Randomly choose Questions as a queue, in order to solve them one at a time
            Frame.Navigate(typeof(QuizPage));
        }

        private void MainPage_Click(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                Frame.Navigate(typeof(ChapterPage), e.ClickedItem);
            }
        }
    }
}
