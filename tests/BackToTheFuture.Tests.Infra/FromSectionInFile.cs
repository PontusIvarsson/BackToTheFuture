using Fixie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BackToTheFuture.Tests.Infra
{
    public class FromSectionInFile : ParameterSource
    {

        public IEnumerable<object[]> GetParameters(MethodInfo method)
        {
            var parameters = new List<object[]>();
            string prefix = "SectionInFile";

            if (method.GetCustomAttribute<SectionInFileAttribute>(true) == null)
            {
                return parameters;
            }

            var testClassName = method.DeclaringType.Name;
            var resultSectionName = method.Name.Replace("prefix", "");


            var fileExists = false;
            var resultFileName = "";
            string[] folders = new string[] { "./", "./tests", "./tests/testdata", "./testdata" };
            foreach (var filenme in folders.Select(x => x + "/" + testClassName + ".txt"))
            {
                if (File.Exists(filenme))
                {
                    resultFileName = filenme;
                    fileExists = true;
                    break;
                }
            }

            if (fileExists)
            {
                var allText = File.ReadAllText(resultFileName);

                var resultSections = allText.Split(new string[] { "####expected for " }, StringSplitOptions.None);

                foreach (var resultSection in resultSections)
                {
                    if (resultSection.Contains(method.Name))
                    {
                        var result = resultSection.Replace(method.Name, "");
                        TrimFirstAndLastLine(ref result);

                        parameters.Add(new string[] { result });
                    }
                }
            }


            return parameters;
        }

        private string TrimFirstAndLastLine(ref string str)
        {
            string[] lines = str.Split(Environment.NewLine.ToCharArray()).Skip(2).ToArray();
            str = string.Join(Environment.NewLine, lines);
            str = str.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
            str = str.TrimEnd('\n').TrimEnd('\r');

            return str;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SectionInFileAttribute : Attribute
    {

    }
}
