using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Common.Log;

namespace Lykke.EmailContentGenerator
{
    public static class EmailContentGeneratorUtils
    {

        private const string PlaceholderOpen = "@[";
        private const string PlaceholderClose = "]";

        public static async Task<string> ReadContentAsync(this EmailTemplates emailTemplate, string baseUrl, ILog log)
        {

            try
            {
                var httpClient = new HttpClient();
                return await httpClient.GetStringAsync($"{baseUrl}{emailTemplate}.html");
            }
            catch (Exception e)
            {
                await log.WriteErrorAsync("EmailContentGenerator", "Reading email content", emailTemplate.ToString(), e);
                throw;
            }
        }


        private static string SubstitutePlaceHolder(string content, string placeHolder, string value)
        {
            return content.Replace(PlaceholderOpen + placeHolder + PlaceholderClose, value);
        }


        public static string ApplyEmailContentModel(this string content, object dataModel)
        {

            var typeInfo = dataModel.GetType().GetTypeInfo();

            foreach (var propertyInfo in typeInfo.GetProperties())
            {

                var placeholderAttr = propertyInfo.GetCustomAttribute<EmailPlaceholderAttribute>();

                var placeHolder = placeholderAttr?.Placeholder ?? propertyInfo.Name;

                var value = propertyInfo.GetValue(dataModel).ToString();

                content = SubstitutePlaceHolder(content, placeHolder, value);
            }

            return content;
        }

        public static async Task CheckUnreplacedPlaceholdersAsync(this EmailTemplates emailTemplate, string content, ILog log)
        {
            var index = content.IndexOf(PlaceholderOpen, StringComparison.Ordinal);
            if (index < 0)
                return;

            var closeIndex = content.IndexOf(PlaceholderClose, index, StringComparison.Ordinal);

            var placeholder = content.Substring(index, closeIndex - index + 1);
            await log.WriteWarningAsync("EmailContentGenerator", "Generate Email content", emailTemplate.ToString(),
                "Found not replaced placeholder: " + placeholder);
        }
    }
}
