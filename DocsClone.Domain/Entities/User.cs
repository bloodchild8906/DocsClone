using System.ComponentModel.DataAnnotations.Schema;

namespace DocsClone.Domain.Entities
{
    [Table("users")]
    public class User
    {
        [Column("user_id")]
        public long Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

    }
}
