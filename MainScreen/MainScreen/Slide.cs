using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainScreen
{
    public class Slide
    {
        private int slideNumber;
        private string title;
        private string thumbnail;
        private string image;
        private string text;

        public Slide(int slideNumber, string title, string thumbnail, string image, string text)
        {
            this.slideNumber = slideNumber;
            this.title = title;
            this.thumbnail = thumbnail;
            this.image = image;
            this.text = text;
        }

    }

}
