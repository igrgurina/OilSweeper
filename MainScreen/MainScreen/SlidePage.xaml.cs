using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using MainScreen.Extensions;
using MainScreen.ViewModel;

namespace MainScreen
{
    public sealed partial class SlidePage : Page
    {

        private SlideData data = new SlideData();
        public SlidePage()
        {
            this.InitializeComponent();
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

        private void ClearStack()
        {
            while (Frame.BackStack.LastOrDefault().SourcePageType == typeof(SlidePage))
            {
                Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
            }
            Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
        }

        private void back_Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var reverse = data.Chapter.Slides.ToList();
            reverse.Reverse();
            SlideViewModel next = reverse.SkipWhile(c => c != data.Slide).Skip(1).FirstOrDefault();
            if (next == null)
            {
                Frame.Navigate(typeof(ChapterPage), data.Chapter);
                ClearStack();
            }
            else
            {
                data.Slide = next;
                Frame.Navigate(typeof(SlidePage), data);
            }
        }

        private void front_Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SlideViewModel next = data.Chapter.Slides.SkipWhile(c => c != data.Slide).Skip(1).FirstOrDefault();
            if (next == null)
            {
                Frame.Navigate(typeof(ChapterPage), data.Chapter);
                ClearStack();
            }
            else
            {
                data.Slide = next;
                Frame.Navigate(typeof(SlidePage), data);
            }
        }

        private void return_Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChapterPage), data.Chapter);
            ClearStack();
        }
    }
}
