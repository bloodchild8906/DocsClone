using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.Dto.V1.Validation
{
    public class ValidationDto:BaseResponseDto
    {
        public List<Error> Errors { get; set; } = new List<Error>();
    }
    public class Error
    {
        public string Message { get; set; }
    }
}
