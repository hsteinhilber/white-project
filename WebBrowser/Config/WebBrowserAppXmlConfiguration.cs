using System.Collections.Generic;
using Bricks;
using Bricks.Core;
using White.Core.Logging;

namespace White.WebBrowser.Config
{
    public class WebBrowserAppXmlConfiguration : AssemblyConfiguration, WebBrowserConfiguration
    {
        private static readonly Dictionary<string, object> defaultValues = new Dictionary<string, object>();
        private static WebBrowserConfiguration instance;

        static WebBrowserAppXmlConfiguration()
        {
            defaultValues.Add(CodePath.Get(CodePath.New<WebBrowserConfiguration>().FirefoxSingleWindowCheckWait), 2000);
        }

        private WebBrowserAppXmlConfiguration() : base("White", "WebBrowser", defaultValues, WhiteLogger.Instance){}

        public static WebBrowserConfiguration Instance
        {
            get
            {
                if (instance == null) instance = new WebBrowserAppXmlConfiguration();
                return instance;
            }
        }

        public int FirefoxSingleWindowCheckWait
        {
            get { return S.ToInt(usedValues[CodePath.Get(CodePath.New<WebBrowserConfiguration>().FirefoxSingleWindowCheckWait)]); }
        }
    }
}