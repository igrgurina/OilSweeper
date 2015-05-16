using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainScreen
{
    public class Question
    {
        int questionID { get; set; }
        string text { get; set; }
        string image { get; set; }
        List<string> options { get; set; }
        string correct { get; set; }

    }
}
