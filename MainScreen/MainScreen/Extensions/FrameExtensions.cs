using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace MainScreen.Extensions
{
    public static class FrameExtensions
    {
        public static void ChangeContext(this Frame frame, Page page, object newDataContext)
        {
            page.DataContext = newDataContext;
        }
    }
}
