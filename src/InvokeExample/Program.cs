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
            var content = contentGenerator.GenerateWelcomeFxTemplateAsync().Result;

            Console.WriteLine(content);
            Console.ReadLine();
        }

    }
}
