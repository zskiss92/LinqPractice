using System.ComponentModel.DataAnnotations;

namespace LinqPractice.Models
{
    public class MuseumObject
    {
        [Key]
        public string ObjectInventoryNumber { get; set; } = string.Empty;
        public string ObjectPersistentIdentifier { get; set; } = string.Empty;
        public string ObjectTitle { get; set; } = string.Empty;
        public string ObjectType { get; set; } = string.Empty;
        public string ObjectCreator { get; set; } = string.Empty;
        public int ObjectCreationDate { get; set; }
        public string ObjectImage { get; set; } = string.Empty;
    }
}
