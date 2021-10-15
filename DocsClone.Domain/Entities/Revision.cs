using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocsClone.Domain.Entities
{
    [Table("revisions")]
    public class Revision
    {
        [Column("revision_id")]

        public long Id { get; set; }
        [Column("document_id")]
        public Document Document { get; set; }

        [Column("document_version")]
        public string DocumentVersion { get; set; }
        [Column("document_data")]
        public string DocumentData { get; set; }
        [Column("modifications")]
        public string Modifications { get; set; }
        [Column("document_ownner")]
        public User DocumentOwner { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
        [Column("created_with_timezone")]
        public int CreatedWithTimezone { get; set; }
        [Column("created_by")]
        public User CreatedBy { get; set; }

        [Column("modified_on")]
        public DateTime ModifiedOn { get; set; }
        [Column("modified_with_timezone")]
        public int ModifiedWithTimezone { get; set; }
        [Column("modified_by")]
        public User ModifiedBy { get; set; }

    }
}
