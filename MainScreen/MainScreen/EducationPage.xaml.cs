using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Engine.Models;
using MainScreen.Extensions;
using MainScreen.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MainScreen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EducationPage : Page
    {
        public ObservableCollection<ChapterViewModel> Chapters = new ObservableCollection<ChapterViewModel>();
        public EducationPage()
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
            var context = EducationContext.Load("Assets/Prezentacija.xml");
            DataContext = context.ToViewModel();
        }

        private void back_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void quiz_image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(QuizPage));
        }

        private void MainPage_Click(object sender, ItemClickEventArgs e)
        {
            ChapterViewModel output = e.ClickedItem as ChapterViewModel;
            int ChapterNumber = output.ChapterNumber;
            this.Frame.Navigate(typeof (ChapterPage), ChapterNumber);
        }
    }
}
