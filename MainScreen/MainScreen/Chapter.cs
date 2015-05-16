using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainScreen
{
    public class Chapter
    {
        int chapterNumber { get; set; }
        string title { get; set; }
        string thumbnail { get; set; }
        List<Slide> slides { get; set; }
        List<Question> questions { get; set; }

    }
}
