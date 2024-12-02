using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Engine;

namespace mynance.src.exceptions
{
    class ExceptionMessage
    {
        private string Message_enUS;
        private string Message_srLatnRS;

        public ExceptionMessage(string message_enUS, string message_srLatnRS)
        {
            Message_enUS = message_enUS;
            Message_srLatnRS = message_srLatnRS;
        }

        public String GetLocalized()
        {
            if (LocalizeDictionary.Instance.Culture.TwoLetterISOLanguageName == "en") return Message_enUS;
            else return Message_srLatnRS;
        }
    }
}
