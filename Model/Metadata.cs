using System;
using System.Collections.Generic;
using System.Text;

namespace Dramarr.Data.Model
{
    public class Metadata
    {
        public Guid Id { get; set; }
        public Guid ShowId { get; set; }
        public string ImageUrl { get; set; }
        public string Plot { get; set; }
        public string Cast { get; set; }
        public string Language { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Metadata()
        {
        }

        public Metadata(Guid showId, string imageUrl, string plot, string cast, string language)
        {
            Id = Guid.NewGuid();
            ShowId = showId;
            ImageUrl = imageUrl;
            Plot = plot;
            Cast = cast;
            Language = language;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public Metadata(string imageUrl, string plot, string cast, string language)
        {
            Id = Guid.NewGuid();
            ShowId = new Guid();
            ImageUrl = imageUrl;
            Plot = plot;
            Cast = cast;
            Language = language;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
