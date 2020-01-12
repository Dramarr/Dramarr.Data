using System;
using System.Collections.Generic;
using System.Text;
using static Dramarr.Core.Enums.EpisodeHelpers;

namespace Dramarr.Data.Model
{
    /// <summary>
    /// Episode 
    /// </summary>
    public class Episode
    {
        public Guid Id { get; set; }
        public Guid ShowId { get; set; }
        public string Url { get; set; }
        public string Filename { get; set; }
        public EpisodeStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Episode()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="showId"></param>
        /// <param name="url"></param>
        /// <param name="filename"></param>
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
