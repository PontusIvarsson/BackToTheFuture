using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackToTheFuture.Core
{
    public class ManagedContent
    {
        public ManagedContent(string soruce)
        {
            Init(soruce);
        }

        public void Init(string source)
        {
            Source = source;
            Model = JObject.Parse(GetRawJsonMetaData());
        }

        public string GetRawJsonMetaData()
        {
            var rawJsonMetaData = Source;


            Regex regex = new Regex("<!-- meta(.*?)-->", RegexOptions.Singleline);
            MatchCollection matches = regex.Matches(Source);
            if (matches.Count == 1)
            {
                rawJsonMetaData = matches[0].Value.Replace("<!-- meta", "").Replace("-->", "").Trim();
                try
                {
                    JObject.Parse(rawJsonMetaData);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Could not parse string to Json.", ex);
                }
            }

            return rawJsonMetaData;
        }

        public string GetRawMarkUp()
        {
            Regex regex = new Regex("<!-- meta(.*?)-->", RegexOptions.Singleline);

            var rawRawMarkUp = regex.Replace(Source, "");
            return rawRawMarkUp.TrimStart();
        }

        public string ProccessPlugins()
        {
            throw new NotImplementedException();
        }

        public string Source { get; private set; }
        public string Processed { get; set; }

        public JObject Model { get; private set; }
        public HtmlTemplate Template { get; private set; }
    }

    public class HtmlTemplate
    {
        public HtmlTemplate(string templateSrc)
        {
            TemplateSoruce = templateSrc;
        }

        public string TemplateSoruce { get; private set; }

    }
}
