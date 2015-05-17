using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Engine.Models
{
    [XmlRoot("EducationalContent")]
    public class EducationContext
    {
        [XmlArray("Chapters")]
        [XmlArrayItem("Chapter")]
        public List<Chapter> Chapters { get; set; }

        public static EducationContext Load(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(EducationContext));
            XDocument document = XDocument.Load(file);
            return (EducationContext)serializer.Deserialize(document.CreateReader());
        }
    }
}
