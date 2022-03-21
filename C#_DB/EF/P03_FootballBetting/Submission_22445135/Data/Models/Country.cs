﻿using System;
using System.Collections.Generic;

namespace P03_FootballBetting.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            Towns = new HashSet<Town>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
