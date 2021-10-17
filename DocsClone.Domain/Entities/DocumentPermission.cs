using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.Domain.Entities
{
    public enum AcessLevel
    {
        READ,WRITE,FULL
    }
    public class DocumentPermission
    {
        public long id { get; set; }
        public virtual User User { get; set; }
        public AcessLevel Access { get; set; }
        public virtual Document Document { get; set; }
    }
}
