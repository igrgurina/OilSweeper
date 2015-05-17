using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MainScreen.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MainScreen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChapterPage : Page
    {
        public ChapterPage()
        {
            this.InitializeComponent();
        }

        public ObservableCollection<ChapterViewModel> Chapters = new ObservableCollection<ChapterViewModel>();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<SlideViewModel> mnr = new List<SlideViewModel>();
            var bvzz = new SlideViewModel()
            {
                SlideNumber = 3,
                Image = "Assets/learn_image.png",
                Title = "Nubarsko poglavnje",
                Text = "Lorem ipsum je glup i jbga sad što ćemo s tim mislim ono stvarno zašto je takav ja nemam blage",
                Thumbnail = "Assets/learn_image.png"
            };

            mnr.Add(bvzz);
            mnr.Add(bvzz);
            mnr.Add(bvzz);
            mnr.Add(bvzz);
            mnr.Add(bvzz);
            mnr.Add(bvzz);
            mnr.Add(bvzz);

            var nesto = new ChapterViewModel()
            {
                ChapterNumber = 1,
                Image = "Assets/back_image.png",
                Title = "Prvi test",
                Thumbnail = "bla.png",
                Slides = mnr  
            };

            var novo = new ChapterViewModel()
            {
                ChapterNumber = 2,
                Image = "Assets/play_image.png",
                Title = "Drugi test",
                Thumbnail = "bla.png"
            };

            Chapters.Add(nesto);
            Chapters.Add(novo);
            DataContext = Chapters.SingleOrDefault(c => c.ChapterNumber.Equals(e.Parameter));
            var x = 0;
        }

        private void back_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EducationPage));
        }

        private void quiz_image_Tapped(object sender, TappedRoutedEventArgs e) {
            this.Frame.Navigate(typeof(QuizPage));
        }

        private void Slide_Click(object sender, ItemClickEventArgs e)
        {
            SlideViewModel output = e.ClickedItem as SlideViewModel;
            int SlideNumber = output.SlideNumber;
            this.Frame.Navigate(typeof(SlidePage), SlideNumber);
        }
    }
}
