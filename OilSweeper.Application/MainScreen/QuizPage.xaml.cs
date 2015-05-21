using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Media;
using MainScreen.ViewModel;

namespace MainScreen
{
    public sealed partial class QuizPage : Page
    {
        public QuizPage()
        {
            InitializeComponent();
            HardwareButtons.BackPressed += OnBackPressed;
        }

        private List<QuestionViewModel> Questions = new List<QuestionViewModel>();
        private bool canClick = true;
        private int i = 0;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Questions = (List<QuestionViewModel>)e.Parameter;
            DataContext = Questions.ElementAt(i);
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

        private void back_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;

            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
                e.Handled = true;
            }
        }

        private void Answer_OnClick(object sender, RoutedEventArgs e)
        {
            if (canClick)
            {
                Button a = (Button)sender;
                if (a.DataContext.ToString() == Questions.ElementAt(i).Correct)
                {
                    a.Background = new SolidColorBrush(Windows.UI.Colors.Green);

                    Correct.Visibility =
                        Explanation.Visibility =
                        ExplanationLabel.Visibility =
                        Next.Visibility = Visibility.Visible;
                    canClick = false;
                }
                else
                {
                    a.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                }
            }
        }

        private void NextQuestion(object sender, RoutedEventArgs e)
        {
            i++;
            if (i < Questions.Count())
            {
                DataContext = Questions.ElementAt(i);
                Correct.Visibility =
                    Explanation.Visibility =
                    ExplanationLabel.Visibility =
                    Next.Visibility = Visibility.Collapsed;
                canClick = true;
            }
            else
            {
                Frame frame = Window.Current.Content as Frame;

                if (frame != null && frame.CanGoBack)
                {
                    frame.GoBack();
                }
            }
        }
    }
}
