using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace WorkSchedule.Swagger.Filters;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            
            var enumValues = new List<object>();
            var enumDescriptions = new List<string>();

            foreach (var enumValue in Enum.GetValues(context.Type))
            {
                var enumName = Enum.GetName(context.Type, enumValue);
                if (enumName != null)
                {
                    var fieldInfo = context.Type.GetField(enumName);
                    var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
                    
                    enumValues.Add((int)enumValue);
                    enumDescriptions.Add(descriptionAttribute?.Description ?? enumName);
                }
            }

            // Adiciona os valores do enum
            foreach (var value in enumValues)
            {
                schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiInteger((int)value));
            }

            // Adiciona as descrições como extensão personalizada
            var enumNamesArray = new Microsoft.OpenApi.Any.OpenApiArray();
            foreach (var desc in enumDescriptions)
            {
                enumNamesArray.Add(new Microsoft.OpenApi.Any.OpenApiString(desc));
            }
            schema.Extensions.Add("x-enumNames", enumNamesArray);

            // Adiciona descrição geral do schema
            if (enumDescriptions.Any())
            {
                schema.Description = string.Join(", ", enumValues.Zip(enumDescriptions, (value, desc) => $"{value}: {desc}"));
            }
        }
    }
}
