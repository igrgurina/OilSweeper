using System.Collections.Generic;
using System.Xml.Serialization;

namespace Engine.Models
{
    public class Chapter
    {
        [XmlElement]
        public int ChapterNumber { get; set; }

        [XmlElement]
        public string Title { get; set; }

        [XmlElement]
        public string Thumbnail { get; set; }

        [XmlArray("Slides")]
        [XmlArrayItem("Slide")]
        public List<Slide> Slides { get; set; }

        [XmlArray("Questions")]
        [XmlArrayItem("Question")]
        public List<Question> Questions { get; set; } 

    }
}