namespace SocialNetwork.Entities.Models
{
    public class Blob : ParentModel
    {
        public long Lenth { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Buffer { get; set; }
    }
}
