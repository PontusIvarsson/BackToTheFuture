using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Core
{

    public class DiskSourceFile : SourceFile
    {
        public DiskSourceFile(FileInfo file)
        {
            _file = file;
            this.Name = file.Name;
            this.RelativeFolderPath = SanitzeFolderPath(file.FullName);
        }

        private FileInfo _file;

        private string SanitzeFolderPath(string path)
        {
            return path.Trim(' ');
        }


        public override byte[] GetRawSoruce()
        {
            return File.ReadAllBytes(this.Fullname);
        }
    }

    public class TargetFile : FileTypeBase
    {
        public TargetFile(string name, string relativeFolderPath)
        {
            Name = name;
            RelativeFolderPath = relativeFolderPath;

        }
    }

    public abstract class SourceFile : FileTypeBase
    {
        public abstract byte[] GetRawSoruce();
    }

    public abstract class FileTypeBase
    {
        public string Name { get; set; }
        public string RelativeFolderPath { get; set; }
        public string Fullname { get { return SanitizePath(string.Format("{0}\\{1}", RelativeFolderPath, Name)); } }


        private string SanitizePath(string path)
        {
            path = path.Trim('/');
            path = path.Trim();
            return path;
        }
    }
}
