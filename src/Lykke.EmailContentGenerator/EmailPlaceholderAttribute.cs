using System;

namespace Lykke.EmailContentGenerator
{
    public class EmailPlaceholderAttribute : Attribute
    {
        public EmailPlaceholderAttribute(string placeholder)
        {
            Placeholder = placeholder;
        }

        public string Placeholder { get; private set; }

    }
}
