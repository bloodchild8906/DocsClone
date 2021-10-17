
namespace DocsClone.Dto.V1.Document.Request
{
    public class CreateDocumentRequest
    {
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public string DocumentVersion { get; set; }
        public string DocumentData { get; set; }
        public int TimeZone { get; set; }
    }
}
