using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Engine.Models
{
    public class Question
    {
        private string _explanation;

        [XmlElement]
        public int QuestionId { get; set; }

        [XmlElement]
        public string Text { get; set; }

        [XmlElement]
        public string Image { get; set; }

        [XmlElement]
        public string Explanation
        {
            get { return _explanation; }
            set
            {
                _explanation =  string.Join(" ", value.Split('\n', '\r').Select(s => s.Trim()).Where(s => s != ""));
            }
        }

        [XmlArray("Options")]
        [XmlArrayItem("Option")]
        public List<string> Options { get; set; }

        [XmlElement]
        public string Correct { get; set; }
    }
}
