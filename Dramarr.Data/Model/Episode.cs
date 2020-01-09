using System;
using System.Collections.Generic;
using System.Text;
using static Dramarr.Core.Enums.EnumsHelpers;

namespace Dramarr.Data.Model
{
    public class Episode
    {
        public Guid Id { get; set; }
        public Guid ShowId { get; set; }
        public string Url { get; set; }
        public string Filename { get; set; }
        public EpisodeStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Episode()
        {
        }

        public Episode(Guid showId, string url, string filename)
        {
            this.Id = Guid.NewGuid();
            this.ShowId = showId;
            this.Url = url;
            this.Filename = filename;
            this.Status = EpisodeStatus.UNKNOWN;
            this.CreatedAt = DateTime.UtcNow;
            this.UpdatedAt = DateTime.UtcNow;
        }
    }
}
