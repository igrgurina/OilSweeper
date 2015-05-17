using System.Xml.Serialization;

namespace Engine.Models
{
    public class Slide
    {
        [XmlElement]
        public int PageNumber { get; set; }

        [XmlElement]
        public string Title { get; set; }

        [XmlElement]
        public string Thumbnail { get; set; }

        [XmlElement]
        public string Image { get; set; }

        [XmlElement]
        public string Text { get; set; }
    }
}