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

        public List<SourceFile> Scan()
        {
            List<SourceFile> files = new List<SourceFile>();
            files = TraverseDirs(_dir, files);

            return files;
        }

        private List<SourceFile> TraverseDirs(DirectoryInfo dir, List<SourceFile> files)
        {
            try
            {
                foreach (DirectoryInfo directoryInfor in dir.GetDirectories())
                {
                    TraverseDirs(directoryInfor, files);
                }

                foreach (FileInfo fileInfo in dir.GetFiles())
                {
                    files.Add(new DiskSourceFile(fileInfo));
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
