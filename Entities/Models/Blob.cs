using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Blob
    {
        public int Id { get; set; }
        public long lenth { get; set; }
        public string name { get; set; }
        public string filename { get; set; }
        public byte[] Buffer { get; set; }
    }
}
