using System.Xml.Serialization;
using System.Linq;

namespace Engine.Models
{
    public class Slide
    {
        private string _text;

        [XmlElement]
        public int PageNumber { get; set; }

        [XmlElement]
        public string Title { get; set; }

        [XmlElement]
        public string Thumbnail { get; set; }

        [XmlElement]
        public string Image { get; set; }

        [XmlElement]
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = string.Join(" ", value.Split('\n', '\r').Select(s => s.Trim()).Where(s => s != ""));
            }
        }
    }
}