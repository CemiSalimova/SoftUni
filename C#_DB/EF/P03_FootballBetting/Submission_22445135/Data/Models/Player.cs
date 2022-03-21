﻿using System;
using System.Collections.Generic;

namespace P03_FootballBetting.Data.Models
{
    public partial class Player
    {
        public Player()
        {
            PlayerStatistics = new HashSet<PlayerStatistic>();
        }

        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int SquadNumber { get; set; }
        public string Initials { get; set; }
       
        public int TeamId { get; set; }
        public int PositionId { get; set; }
        public bool IsInjured { get; set; }

        public virtual Position Position { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; }
    }
}