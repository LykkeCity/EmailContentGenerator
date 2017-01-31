using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Common;
using Common.Log;

namespace Lykke.EmailContentGenerator
{



    public class EmailContentGenerator
    {
        private readonly EmailContentGeneratorSettings _settings;
        private readonly ILog _log;

        public EmailContentGenerator(EmailContentGeneratorSettings settings, ILog log)
        {
            _settings = settings;
            _log = log;
            _settings.BaseUrl = _settings.BaseUrl.AddLastSymbolIfNotExists('/');
        }

        public async Task<string> GenerateContentAsync(EmailTemplates emailTemplate, object dataModel)
        {
            var content = await emailTemplate.ReadContentAsync(_settings.BaseUrl, _log);

            if (dataModel != null)
                content = content.ApplyEmailContentModel(dataModel);

            await emailTemplate.CheckUnreplacedPlaceholdersAsync(content, _log);

            return content;
        }

    }

}
