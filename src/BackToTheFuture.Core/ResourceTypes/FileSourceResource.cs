using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Core.ResourceTypes
{
    public class FileSourceResource : SourceResource
    {
        public FileSourceResource(FileInfo file, Uri baseUri)
            : base(RelativeUri(file, baseUri))
        {
            FileInfo = file;
        }

        private static Uri RelativeUri(FileInfo fileInfo, Uri baseUri)
        {
            Uri fileUri = new Uri(fileInfo.FullName);
            var resultUri = baseUri.MakeRelativeUri(fileUri);
            return resultUri;
        }

        public FileInfo FileInfo { get; set; }

        public override Stream GetStream()
        {
            return new FileStream(FileInfo.FullName, FileMode.Open);
        }
    }
}
