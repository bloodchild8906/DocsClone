using System;

namespace DocsClone.Domain.Entities
{
    public class Revision
    {
        public long Id { get; set; }
        public Document Document { get; set; }

        public string DocumentVersion { get; set; }
        public byte[] DocumentData { get; set; }
        public byte[] Modifications { get; set; }
        public User DocumentOwner { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedWithTimezone { get; set; }
        public User CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
        public int ModifiedWithTimezone { get; set; }
        public User ModifiedBy { get; set; }

    }
}
