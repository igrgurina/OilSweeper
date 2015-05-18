using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MainScreen.ViewModel;

namespace MainScreen
{
    public sealed partial class SlidePage : Page
    {
        public SlidePage()
        {
            this.InitializeComponent();
            this.ManipulationStarting += MainPage_ManipulationStarting;
            this.ManipulationStarted += MainPage_ManipulationStarted;
            this.ManipulationInertiaStarting += MainPage_ManipulationInertiaStarting;
            this.ManipulationDelta += MainPage_ManipulationDelta;
            this.ManipulationCompleted += MainPage_ManipulationCompleted;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = (SlideViewModel)e.Parameter;
        }

        private void back_Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;

            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        void MainPage_ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            Debug.WriteLine("MainPage_ManipulationStarting");
        }
        void MainPage_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            Debug.WriteLine("MainPage_ManipulationStarted");
        }
        void MainPage_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {
            Debug.WriteLine("MainPage_ManipulationInertiaStarting");
        }
        void MainPage_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            Debug.WriteLine("MainPage_ManipulationDelta");
        }
        void MainPage_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            Debug.WriteLine("MainPage_ManipulationCompleted");
        }

        /*private void UIElement_OnManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }*/
    }
}
