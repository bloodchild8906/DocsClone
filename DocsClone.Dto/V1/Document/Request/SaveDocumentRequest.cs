using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.Dto.V1.Document.Request
{
    public class SaveDocumentRequest
    {
        public string RevisionVersion { get; set; }
        public string DocumentData { get; set;}
        public int TimeZone { get; set; }
    }
}
