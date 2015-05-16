using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainScreen
{
    class Question
    {
        private int questionID;
        private string text;
        private string image;
        private List<string> options;
        private string correct;

        public Question(int questionID, string text, string image, List<string> options, string correct)
        {
            this.questionID = questionID;
            this.text = text;
            this.image = image;
            this.options = options;
            this.correct = correct;
        }

    }

}
