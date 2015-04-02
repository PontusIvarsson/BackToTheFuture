using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Core.ResourceTypes
{

    public abstract class TargetResource : ResourceTypeBase
    {
        public TargetResource(Uri uri) : base(uri) { }
    }

    public abstract class SourceResource : ResourceTypeBase
    {
        public SourceResource(Uri uri) : base(uri) { }
    }

    public abstract class ResourceTypeBase
    {
        public ResourceTypeBase(Uri uri)
        {
            Uri = uri;
        }

        protected  Uri Uri { get; set; }

        public string Name { get { return Uri.AbsoluteUri; } }
        public abstract Stream GetStream();

    }
}
