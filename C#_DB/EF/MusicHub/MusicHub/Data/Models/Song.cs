using System;
using System.Collections.Generic;

namespace MusicHub.Data.Models
{
    public partial class Song
    {
        public Song()
        {
            SongPerformers = new HashSet<SongPerformer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? AlbumId { get; set; }
        public int WriterId { get; set; }
        public decimal Price { get; set; }
        public Genre Genre { get; set; }

        public virtual Album Album { get; set; }
        public virtual Writer Writer { get; set; }
        public virtual ICollection<SongPerformer> SongPerformers { get; set; }
    }
}
