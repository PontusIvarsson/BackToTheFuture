﻿using BackToTheFuture.Core.SiteTargets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Core.ResourceTypes
{
    public class MemoryTargetResource : TargetResource
    {
        public MemoryTargetResource(string name, Stream stream)
            : base(new Uri(name, UriKind.Relative))
        {
            using (MemoryStream temp = new MemoryStream())
            {
                stream.CopyTo(temp);
                _bytes = temp.ToArray();
            }
        }

        byte[] _bytes;

        public override Stream GetStream()
        {
            return new MemoryStream(_bytes);
        }

    }

}
