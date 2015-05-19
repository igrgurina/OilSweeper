using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace MainScreen.Helpers
{
    public class Swiper
    {
        public Swiper(UIElement element, Action onSwipedLeft, Action onSwipedRight )
        {
            int x1 = 0;
            element.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            element.ManipulationStarted += (s, e) => x1 = (int)e.Position.X;
            element.ManipulationCompleted += (s, e) =>
            {
                if ( (x1 - 150) > e.Position.X && onSwipedLeft != null)
                {
                    onSwipedLeft();
                }
                else if ( (x1 + 150) < e.Position.X && onSwipedRight != null)
                {
                    onSwipedRight();
                }
            };
        }
    }
}
