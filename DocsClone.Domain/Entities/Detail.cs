using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocsClone.Domain.Entities
{
    public class Detail
    {
        [Column("detail_id")]
        public long Id { get; set; }
        [Column("user_id")]
        public User User { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("surname")]
        public string Surname { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Column("primary_contact_number")]
        public string PrimaryContactNumber { get; set; }

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("created_with_timezone")]
        public int CreatedWithTimezone { get; set; }

        [Column("date_modified")]
        public DateTime DateModified { get; set; }

        [Column("modified_with_timezone")]
        public int ModifiedWithTimezone { get; set; }
    }
}
