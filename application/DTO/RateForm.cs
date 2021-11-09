using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class RateForm
    {
        public int userId { get; set; }

        public int postId { get; set; }

        public LikeStatus LikeStatus { get; set; }
    }
}
