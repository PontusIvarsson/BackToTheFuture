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
        public FileSourceResource(FileInfo file)
            : base(new Uri(string.Format(string.Format("{0}\\{1}", file.Directory, file.Name))))
        {
            _file = file;
        }

        private FileInfo _file;

        

        public override Stream GetStream()
        {
            //var filePath = String.Format("{0}/{1}", Uri.Authority, Uri.AbsolutePath);
            return new FileStream(Uri.LocalPath, FileMode.Open);
        }


    }
}
