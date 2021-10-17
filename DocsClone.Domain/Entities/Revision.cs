using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DocsClone.Domain.Entities
{
    public class Revision
    {
        public long Id { get; set; }

        public string DocumentVersion { get; set; }
        public string DocumentData { get; set; }
        public string Modifications { get; set; }
        public virtual User DocumentOwner { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedWithTimezone { get; set; }
        public virtual User CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }
        public int ModifiedWithTimezone { get; set; }
        public virtual User ModifiedBy { get; set; }

    }
}
