using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace WorkSchedule.Filters;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            var enumDescriptions = new List<string>();

            foreach (var enumValue in Enum.GetValues(context.Type))
            {
                var memberInfo = context.Type.GetMember(enumValue.ToString()!).FirstOrDefault();
                var descriptionAttribute = memberInfo?.GetCustomAttribute<DescriptionAttribute>();
                
                var enumValueName = enumValue.ToString();
                var enumValueDescription = descriptionAttribute?.Description ?? enumValueName;
                var enumNumericValue = (int)enumValue;

                schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiInteger(enumNumericValue));
                enumDescriptions.Add($"{enumNumericValue} = {enumValueName} ({enumValueDescription})");
            }

            schema.Description = string.Join(", ", enumDescriptions);
        }
    }
}
