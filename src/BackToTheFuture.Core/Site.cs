using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackToTheFuture.Core
{

    public class StaticSite
    {
        public StaticSite(string path)
        {
            _path = path;
            EnsurePaths();
        }

        string _path;


        public string FullTargetPath { get { return string.Format("{0}\\{1}", _path, "target"); } }
        public string FullSourcePath { get { return string.Format("{0}\\{1}", _path, "source"); } }


        public void RefreshStaticContent()
        {
            var features = ScanSource();
            foreach (var feature in features)
            {
                feature.TransformToTarget();
            }
        }

        private void EnsurePaths()
        {
            Directory.CreateDirectory(FullSourcePath);
            Directory.CreateDirectory(FullTargetPath);
        }

        private List<Feature> ScanSource()
        {
            var features = new List<Feature>();

            foreach (var path in Directory.GetDirectories(FullSourcePath))
                features.Add(new Feature(this, null, path));
            return features;
        }

    }

    public class Feature
    {
        public Feature(StaticSite site, Feature parent, string folderPath)
        {
            Site = site;
            ParentFeature = parent;
            Name = new DirectoryInfo(folderPath).Name;
            FullSourcePath = folderPath;


            Features = new List<Feature>();
            foreach (var path in Directory.GetDirectories(FullSourcePath))
            {
                Features.Add(new Feature(Site, this, path));
            }

            if (File.Exists(_jsonMetaDataPath))
            {
                var json = File.ReadAllText(_jsonMetaDataPath);
            }

            if (File.Exists(_templateHtmlPath))
            {
                TemplateHtml = File.ReadAllLines(_templateHtmlPath).ToList<string>();
            }
            else
            {
                Feature tempParent = null;
                while(tempParent != null)
                {
                    tempParent = parent;
                    if(tempParent.TemplateHtml != null && tempParent.TemplateHtml.Any())
                    {
                        TemplateHtml = tempParent.TemplateHtml;
                    }
                }
            }

            if(TemplateHtml == null || !TemplateHtml.Any())
            {
                TemplateHtml = new List<string>();
                TemplateHtml.Add(string.Format("<html><body>{0}</body></html>", TemplateEnum._CONTENT_.ToString()));
            }
        }

        public Feature ParentFeature { get; set; }
        public List<Feature> Features { get; set; }


        public void TransformToTarget()
        {
            
            Console.WriteLine("Creating folder: " + FullTargetPath);

            Directory.CreateDirectory(FullTargetPath);
            foreach (var file in FilesInSource(new List<string> { ".jpg", ".gif", ".png", ".html" }))
                RawCopyToTarget(file);

            foreach (var file in FilesInSource(new List<string> { ".md" }))
                TransformMdToTarget(file);

            foreach (var feature in Features)
            {
                Directory.CreateDirectory(feature.FullTargetPath);
                feature.TransformToTarget();
            }
        }

        private List<string> FilesInSource(List<string> ext)
        {
            var myFiles = Directory.GetFiles(FullSourcePath, "*.*", SearchOption.AllDirectories)
                 .Where(s => ext.Any(e => s.EndsWith(e)));
            return myFiles.ToList<string>();
        }

        private void TransformMdToTarget(string src)
        {
            var target = GetNewTargetNameForSoruce(src, ".html");
            var markdown = new MarkdownDeep.Markdown();
            markdown.ExtraMode = true;
            markdown.SafeMode = false;
            

            var templateString = String.Join(String.Empty, TemplateHtml);
            var contentString = String.Join("\n\r", GetFileContent(src));
            contentString = markdown.Transform(contentString);
            var mergedContent = templateString.Replace(TemplateEnum._CONTENT_.ToString(), contentString);


            using (var file = File.CreateText(target))
            {
                file.Write(mergedContent);
                file.Close();
            }
        }

        private void RawCopyToTarget(string src)
        {
            var target = GetNewTargetNameForSoruce(src);
            File.Copy(src, target, true);
        }

        private string GetNewTargetNameForSoruce(string src, string newExtention = null)
        {
            var target = src.Replace(FullSourcePath, FullTargetPath);
            if (newExtention != null)
            {
                target = Path.ChangeExtension(target, string.Format(".{0}", newExtention.TrimStart('.')));
            }
            return target;
        }

        private string[] GetFileContent(string src)
        {
            return File.ReadAllLines(src);
        }

        public StaticSite Site { get; private set; }
        public string FullTargetPath { get { return FullSourcePath.Replace(Site.FullSourcePath, Site.FullTargetPath); } }
        public string FullSourcePath { get; private set; }
        public string Name { get; set; }
        public bool Ignore { get; set; }
        public List<string> TemplateHtml  { get; set; }

        string _jsonMetaDataPath { get { return string.Format("{0}/{1}", FullSourcePath, "metadata.json"); } }
        string _templateHtmlPath { get { return string.Format("{0}/{1}", FullSourcePath, "template.html"); } }

    }

    public enum TemplateEnum
    {
        _CONTENT_
    }
}
