using BackToTheFuture.Core.ResourceTypes;
using System;
using System.Collections.Generic;
using System.IO;

namespace BackToTheFuture.Core.SiteSources
{
    public class DirectorySiteSource : ISiteSource
    {
        DirectoryInfo _dir;

        public DirectorySiteSource(DirectoryInfo dir)
        {
            _dir = dir;
        }

        public List<SourceResource> Scan()
        {
            List<SourceResource> files = new List<SourceResource>();
            files = TraverseDirs(_dir, files);

            return files;
        }

        private List<SourceResource> TraverseDirs(DirectoryInfo dir, List<SourceResource> files)
        {
            try
            {
                foreach (DirectoryInfo directoryInfor in dir.GetDirectories())
                {
                    TraverseDirs(directoryInfor, files);
                }

                foreach (FileInfo fileInfo in dir.GetFiles())
                {
                    files.Add(new FileSourceResource(fileInfo));
                }
            }
            catch(Exception ex)
            {
                ;
            }
            
            return files;
        }
    }
}
