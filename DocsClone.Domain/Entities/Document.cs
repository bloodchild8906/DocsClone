using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.Domain.Entities
{
    public class Document
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Revisions { get; set; }
        public string CurrentVersion { get; set; }
        public int AccessLevel { get; set; }

    }
}
