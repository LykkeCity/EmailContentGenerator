using System;
using Common.Log;
using Lykke.EmailContentGenerator;

namespace InvokeExample
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var log = new LogToConsole();

            var generatorSettings = new EmailContentGeneratorSettings
            {
                BaseUrl = ""
            };

            var contentGenerator = new EmailContentGenerator(generatorSettings, log);
            var content = contentGenerator.GenerateContentAsync(EmailTemplates.WelcomeFxTemplate, new { Years ="2015"}).Result;

            Console.WriteLine(content);
            Console.ReadLine();
        }

    }
}
