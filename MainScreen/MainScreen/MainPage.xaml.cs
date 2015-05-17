using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Engine.Models;
using MainScreen.Extensions;
using MainScreen.ViewModel;

namespace MainScreen
{
    public sealed partial class MainPage : Page
    {
        private EducationViewModel educationContext { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Load the educational context
            educationContext = EducationContext.Load("Assets/Prezentacija.xml").ToViewModel();
        }

        private void play_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
        }

        private void learn_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(EducationPage), educationContext);
        }
    }
}
