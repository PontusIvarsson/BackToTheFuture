using System.IO;
using Shouldly;
using BackToTheFuture.Tests.Infra;

namespace BackToTheFuture.Core.Tests
{
    public class ManagedContentTests
    {
        protected readonly ManagedContent managedContent;
        const string SourceTemplate = "./testdata/template.tpl.html";

        public ManagedContentTests()
        {
            var text = File.ReadAllText(SourceTemplate);
            managedContent = new ManagedContent(text);
        }

        [SectionInFile]
        public void Should_GetRawJsonMetaData(string expected)
        {
            managedContent.GetRawJsonMetaData().ShouldBe(expected);
        }
    }
}
