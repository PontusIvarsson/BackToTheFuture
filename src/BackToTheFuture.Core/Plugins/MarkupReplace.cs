using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Core.Plugins
{
    public class MarkupReplace : IFileTransformPlugin
    {
        public byte[] Transform(SourceFile file)
        {
            return file.GetRawSoruce();
        }
    }
}
