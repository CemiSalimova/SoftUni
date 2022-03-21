using System;
using System.Collections.Generic;

namespace MusicHub.Data.Models
{
    public partial class SongPerformer
    {
        public int SongId { get; set; }
        public int PerformerId { get; set; }

        public virtual Performer Performer { get; set; }
        public virtual Song Song { get; set; }
    }
}
