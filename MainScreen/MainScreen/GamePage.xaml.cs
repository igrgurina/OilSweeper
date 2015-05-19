using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;

namespace MainScreen
{
    public sealed partial class GamePage : Page
    {
        public GamePage()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += OnBackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            e.Handled = true;
            Frame.Navigate(typeof(MainPage));
        }

    }
}
