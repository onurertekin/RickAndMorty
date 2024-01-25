using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Nova.Core.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.RegularExpressions;

namespace Host
{
    public class RequiredHeaderParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            try
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                #region X-Correlation-Id

                //OpenApiParameter p = new OpenApiParameter();
                //p.Name = "X-Correlation-Id";
                //p.Required = true;
                //p.Schema = new OpenApiSchema()
                //{
                //    Type = "string",
                //    Default = (IOpenApiAny)new OpenApiString(Guid.NewGuid().ToString())
                //};
                //p.In = ParameterLocation.Header;
                //p.Description = "";
                //operation.Parameters.Add(p);

                #endregion

                #region Accept-Language

                //List<string> languages = new List<string>() { "en-US", "tr-TR" };

                //OpenApiParameter p2 = new OpenApiParameter();
                //p2.Name = "Accept-Language";
                //p2.Required = false;
                //p2.Schema = new OpenApiSchema()
                //{
                //    Type = "string",
                //    Enum = languages.Select(x => (IOpenApiAny)new OpenApiString(x)).ToList()
                //};
                //p2.In = ParameterLocation.Header;
                //p2.Description = "";

                //string defaultLanguageCode = "en-US";
                //if (!string.IsNullOrEmpty(defaultLanguageCode))
                //    p2.Schema.Default = new OpenApiString(defaultLanguageCode);

                //operation.Parameters.Add(p2);

                #endregion

                #region AdditionalHeaderParameters

                var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

                #region RequiredHeaderParameters

                string[] requiredHeaderParameters = null;

                var requiredHeaderParametersAttribute = descriptor.EndpointMetadata.OfType<RequiredHeaderParametersAttribute>().FirstOrDefault();

                if (requiredHeaderParametersAttribute != null)
                {
                    //İlgili action'a özel RequiredHeaderParametersAttribute eklendiyse config yerine ilgili attribute dikkate alınıyor
                    requiredHeaderParameters = requiredHeaderParametersAttribute.GetParameters().Split(";");
                }
                else
                {
                    //Config'teki Swagger:RequiredHeaderParameters alanı dikkate alınıyor.
                    //requiredHeaderParameters = config.Swagger.RequiredHeaderParameters.Split(';');
                }

                if (requiredHeaderParameters != null)
                {
                    foreach (var rhp in requiredHeaderParameters)
                    {
                        if (rhp.ToLower().Trim() == "x-correlation-id" || rhp.ToLower().Trim() == "accept-language" || rhp.ToLower().Trim() == "")
                            continue;

                        AddNonBodyParameterToHeader(context, operation, rhp, true);
                    }
                }

                #endregion

                #endregion

                #region SubStatusCode_422

                foreach (var response in operation.Responses)
                {
                    if (response.Key == "422")
                    {
                        bool summaryFormatError = false;

                        var template = "<table class='custom-substatuscodes'><tbody><tr style='border-bottom: solid 1px #b7b7b7;'><td style='color: #ef3b3b;padding-left:10px;'>SubStatusCode</td><td style='color: #ef3b3b;padding-left:10px;'>Description</td></tr>#rows#</tbody></table>";

                        string[] rows = Regex.Split(response.Value.Description, "\r\n");

                        string rowsHtml = "";
                        foreach (var row in rows)
                        {
                            if (!row.Contains(":"))
                                summaryFormatError = true;

                            string[] columns = row.Split(':');
                            if (columns.Length > 1)
                                rowsHtml += "<tr><td style='padding-left:10px;'>" + columns[0] + "</td><td style='padding-left:10px;'>" + columns[1] + "</td></tr>";
                        }

                        template = template.Replace("#rows#", rowsHtml);

                        if (!summaryFormatError)
                            response.Value.Description = template;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Swagger AddRequiredHeaderParameterFilter'da hata oluştu : " + ex.Message);
            }
        }

        void AddNonBodyParameterToHeader(OperationFilterContext context, OpenApiOperation operation, string parameterName, bool isRequired)
        {
            OpenApiParameter nbp = new OpenApiParameter();
            nbp.Name = parameterName;
            nbp.Required = isRequired;
            nbp.Schema = new OpenApiSchema() { Type = "string" };
            nbp.In = ParameterLocation.Header;
            nbp.Description = "";

            operation.Parameters.Add(nbp);
        }
    }
}
