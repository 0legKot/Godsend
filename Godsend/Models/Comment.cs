using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Godsend.Models
{
    public class Comment
    {
        IUser User { get; set; }
        public string Text { get; set; }
        public DateTime TimePublished { get; set; }
        public IEnumerable<Comment> SubComments { get; set; }
    }
}
