using System;
using System.Linq;

namespace DocsClone.Api.SwaggerCustom
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SwaggerResponseMimeTypeAttribute : Attribute
    {
        public int StatusCode { get; set; }
        public Type Type { get; set; }
        public string[] MediaTypes { get; set; }
        public string Description { get; set; }

        public SwaggerResponseMimeTypeAttribute(int statusCode, Type type, params string[] mediaTypes)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!mediaTypes?.Any() ?? true) throw new ArgumentNullException(nameof(mediaTypes));

            StatusCode = statusCode;
            Type = type;
            MediaTypes = mediaTypes;
        }
    }
}
