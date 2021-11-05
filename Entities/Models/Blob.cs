using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum FileTypes
    {
        Image,
        TextDocument,
        Video
    }
    public class Blob
    {
        public int Id { get; set; }
        public FileTypes FileType { get; set; }
        public byte[] Buffer { get; set; }
    }
}
