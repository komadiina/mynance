using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mynance.src.exceptions
{
    class LocalizedMessages
    {
        public static LocalizedMessages Instance { get; private set; } = new LocalizedMessages();
        private static Dictionary<string, ExceptionMessage> ExceptionMessages { get; set; }


        private LocalizedMessages()
        {
            if (Instance != null)
                Instance = new LocalizedMessages();

            loadMessages();
        }

        private static Dictionary<string, ExceptionMessage> loadMessages()
        {
            ExceptionMessages = [];

            var exceptions = new ConfigurationBuilder().AddJsonFile("res/localization/exceptions.json", false, true).Build();

            foreach (var obj in exceptions.GetChildren())
            {
                String message_enUS = "";
                String message_srLatnRS = "";

                // loosely coded but idc
                foreach (var loc in obj.GetChildren())
                {
                    if (loc.Key.ToString() == "en-US") message_enUS = loc.Value.ToString();
                    else message_srLatnRS = loc.Value.ToString();
                }

                Trace.WriteLine(String.Format(message_enUS, message_srLatnRS));

                ExceptionMessages.Add(obj.Key, new ExceptionMessage(message_enUS, message_srLatnRS));
            }

            return ExceptionMessages;
        }

        public string GetMessage(String exceptionKey) => ExceptionMessages[exceptionKey].GetLocalized();
    }
}
