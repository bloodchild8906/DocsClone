using System.Collections.Generic;

namespace DocsClone.Domain.Entities
{
    public class Document
    {
        public long Id { get; set; }
        public List<User> User { get; set; }
        public List<Revision> Revisions { get; set; }
        public string CurrentVersion { get; set; }
        public int AccessLevel { get; set; }

    }
}
