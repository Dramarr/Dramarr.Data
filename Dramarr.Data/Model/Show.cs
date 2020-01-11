using Dramarr.Core.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static Dramarr.Core.Enums.SourceHelpers;

namespace Dramarr.Data.Model
{
    public class Show
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public Source Source { get; set; }
        public bool? Download { get; set; }
        public bool? Enabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Show()
        {
        }

        public Show(string url)
        {
            this.Id = Guid.NewGuid();
            this.Url = url;
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
            this.Download = false;
            this.Enabled = true;

            var name = string.Empty;
            var urlIdentifier = string.Empty;

            switch (url)
            {
                case string s when s.ToLowerInvariant().Contains("myasiantv"):
                    this.Source = Source.MYASIANTV;
                    urlIdentifier = url.Split('/')[4];
                    name = urlIdentifier.Replace("-", " ");
                    break;
                case string s when s.ToLowerInvariant().Contains("estrenosdoramas"):
                    this.Source = Source.ESTRENOSDORAMAS;
                    urlIdentifier = url.Replace("https://www.estrenosdoramas.net", "");
                    name = url.Split('/')[5].Replace("-", " ").Replace(".html", "");
                    break;
                case string s when s.ToLowerInvariant().Contains("kshow"):
                    this.Source = Source.KSHOW;
                    urlIdentifier = url.Replace("https://kshow.to/shows/", "").Replace("/", "");
                    name = urlIdentifier.Replace("-", " ").Replace("/", "");
                    break;
                default:
                    break;
            }

            this.Url = urlIdentifier;
            this.Title = new CultureInfo("en-US", false).TextInfo.ToTitleCase(name);
        }
    }
}
