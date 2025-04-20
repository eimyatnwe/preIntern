using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http;

namespace API.Operations
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formFileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile));

            if (formFileParams.Any())
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Required = true,
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        {
                            "multipart/form-data", new OpenApiMediaType
                            {
                                Encoding = new Dictionary<string, OpenApiEncoding>
                                {
                                    ["file"] = new OpenApiEncoding { Style = ParameterStyle.Form }
                                },
                                Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        ["file"] = new OpenApiSchema 
                                        { 
                                            Type = "string", 
                                            Format = "binary",
                                            Description = "The file to upload"
                                        },
                                        ["fileName"] = new OpenApiSchema 
                                        { 
                                            Type = "string",
                                            Description = "Name of the file"
                                        },
                                        ["title"] = new OpenApiSchema 
                                        { 
                                            Type = "string",
                                            Description = "Title of the image"
                                        }
                                    },
                                    Required = new HashSet<string>{ "file" }
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}