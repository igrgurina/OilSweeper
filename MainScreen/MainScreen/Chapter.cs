using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainScreen
{
    public class Chapter
    {
        private int chapterNumber;
        private string title;
        private string thumbnail;
        private List<Slide> slides;
        private List<Question> questions;

        public Chapter(int chapterNumber, string title, string thumbnail, List<Slide> slides, List<Question> questions)
        {
            this.chapterNumber = chapterNumber;
            this.title = title;
            this.thumbnail = thumbnail;
            this.slides = slides;
            this.questions = questions;
        }	

    }
}
