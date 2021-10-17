

using System.Collections.Generic;

namespace DocsClone.Dto.V1.Validation
{
    public class CreateDocumentValidator
    {
        private ValidationDto Validitator { get; }
        public CreateDocumentValidator()
        {
            Validitator = new ValidationDto();
        }
        public CreateDocumentValidator GetValidation(string name)
        {
            if (string.IsNullOrEmpty(name))
                Validitator.Errors.Add(new Error { Message = "a document name is required" });
            return this;
        }
        public bool HasError => Validitator.Errors.Count > 0;
        public List<Error> Errors => Validitator.Errors;
    }
}
