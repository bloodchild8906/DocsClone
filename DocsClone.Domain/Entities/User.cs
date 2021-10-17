using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DocsClone.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        public virtual List<Document> Documents { get; set; } = new List<Document>();

        public virtual Detail Detail { get; set; }

    }
}
