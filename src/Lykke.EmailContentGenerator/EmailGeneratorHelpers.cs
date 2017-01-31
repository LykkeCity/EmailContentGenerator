using System;
using System.Threading.Tasks;

namespace Lykke.EmailContentGenerator
{
    public enum EmailTemplates
    {
        WelcomeTemplate, WelcomeFxTemplate, UserRegisteredTemplate
    }

    public class GeneratorBaseModel
    {
        internal static string GetCurrentYear()
        {
            return DateTime.UtcNow.Year.ToString();
        }


        public string Year { get; set; } = GetCurrentYear();
    }

    public static class GeneratorExtentions
    {

        public static Task<string> GenerateWelcomeTemplateAsync(this EmailContentGenerator generator)
        {
            return generator.GenerateContentAsync(EmailTemplates.WelcomeTemplate, new GeneratorBaseModel());
        }

        public static Task<string> GenerateWelcomeFxTemplateAsync(this EmailContentGenerator generator)
        {
            return generator.GenerateContentAsync(EmailTemplates.WelcomeFxTemplate, new GeneratorBaseModel());
        }

        public static Task<string> GenerateUserRegisteredTemplateAsync(this EmailContentGenerator generator, string fullName, string email, string contactPhone, DateTime registered, string country)
        {
            return generator.GenerateContentAsync(EmailTemplates.UserRegisteredTemplate, 
                new
                {
                    FullName = fullName,
                    Email = email,
                    ContactPhone = contactPhone,
                    DateTime = registered.ToString(),
                    Country = country
                });
        }

    }

}
