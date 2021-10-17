using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace DocsClone.Api.SwaggerCustom
{
    public class SwaggerResponseMimeTypeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attrs = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<SwaggerResponseMimeTypeAttribute>()
                .ToList();

            var declType = context.MethodInfo.DeclaringType;
            while (declType != null)
            {
                attrs.AddRange(declType
                    .GetCustomAttributes(true)
                    .OfType<SwaggerResponseMimeTypeAttribute>());

                declType = declType.DeclaringType;
            }

            if (attrs.Any())
            {
                foreach (var attr in attrs)
                {
                    HttpStatusCode statusCode = (HttpStatusCode)attr.StatusCode;
                    string statusString = attr.StatusCode.ToString();

                    if (!operation.Responses.TryGetValue(statusString, out OpenApiResponse response))
                    {
                        response = new OpenApiResponse();
                        operation.Responses.Add(statusString, response);
                    }

                    if (!string.IsNullOrEmpty(attr.Description))
                        response.Description = attr.Description;
                    else if (string.IsNullOrEmpty(response.Description))
                        response.Description = statusCode.ToString();

                    response.Content ??= new Dictionary<string, OpenApiMediaType>();

                    var openApiMediaType = new OpenApiMediaType();

                    string swaggerDataType =
                        IsNumericType(attr.Type) ? "number"
                        : IsStringType(attr.Type) ? "string"
                        : IsBooleanType(attr.Type) ? "boolean"
                        : null;

                    if (swaggerDataType == null)
                    {
                        // this is not a native type, try to register it in the repository
                        if (!context.SchemaRepository.TryLookupByType(attr.Type, out var schema))
                        {
                            schema = context.SchemaGenerator.GenerateSchema(attr.Type, context.SchemaRepository);

                            if (schema == null)
                                throw new InvalidOperationException($"Failed to register swagger schema type '{attr.Type.Name}'");
                        }

                        openApiMediaType.Schema = schema;
                    }
                    else
                    {
                        openApiMediaType.Schema = new OpenApiSchema
                        {
                            Type = swaggerDataType
                        };
                    }

                    foreach (string mediaType in attr.MediaTypes)
                        response.Content.Add(mediaType, openApiMediaType);
                }
            }
        }

        private bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsStringType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsBooleanType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return true;
                default:
                    return false;
            }
        }
    }
}
