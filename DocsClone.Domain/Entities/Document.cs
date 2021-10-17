using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DocsClone.Domain.Entities
{
    public class Document
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual List<Revision> Revisions { get; set; } = new List<Revision>(); 
        public string CurrentVersion { get; set; }
        public virtual List<DocumentPermission> Permissions { get; set; } = new List<DocumentPermission>();
        public virtual List<User> Users { get; set; } = new List<User>();
    }
}
