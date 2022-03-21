using System;
using System.Collections.Generic;

namespace MusicHub.Data.Models
{
    public partial class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int? ProducerId { get; set; }
        public decimal Price { get; set; }

        public virtual Producer Producer { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
