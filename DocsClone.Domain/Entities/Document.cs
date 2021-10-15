using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocsClone.Domain.Entities
{
    [Table("documents")]
    public class Document
    {
        [Column("document_id")]
        public long Id { get; set; }
        [Column("users")]
        public List<User> User { get; set; }
        [Column("revisions")]
        public List<Revision> Revisions { get; set; }
        [Column("current_revision")]
        public string CurrentVersion { get; set; }
        [Column("access_level")]
        public int AccessLevel { get; set; }

    }
}
