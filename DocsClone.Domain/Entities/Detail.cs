using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.Domain.Entities
{
    public class Detail
    {
        public long Id { get; set; }
        public User User { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PrimaryContactNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public int CreatedWithTimezone { get; set; }

        public DateTime DateModified { get; set; }

        public int ModifiedWithTimezone { get; set; }
    }
}
