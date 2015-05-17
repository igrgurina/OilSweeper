using System.Collections.Generic;
using System.Xml.Serialization;

namespace Engine.Models
{
    public class Question
    {
        [XmlElement]
        public int QuestionId { get; set; }

        [XmlElement]
        public string Text { get; set; }

        [XmlElement]
        public string Image { get; set; }

        [XmlArray("Options")]
        [XmlArrayItem("Option")]
        public List<string> Options { get; set; }

        [XmlElement]
        public string Correct { get; set; }
    }
}
