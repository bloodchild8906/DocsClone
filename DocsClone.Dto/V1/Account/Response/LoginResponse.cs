using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocsClone.Dto.V1.Account.Response
{
    public class LoginResponse:BaseResponseDto
    {
        public string? Token { get; set; }
    }
}
