using DocsClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.Dto.V1.Document.Response
{
    public class DocumentResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CurrentVersion { get; set; }
        public AcessLevel AccessLevel { get; set; }
        public List<RevisionResponse> RevisionResponses { get; set; }

    }
    public class RevisionResponse
    { 
        public long Id { get; set; }
        public string DocumentVersion { get; set; }
        public string DocumentData { get; set; }
        public string DocumentModifications { get; set; }
    }
}
