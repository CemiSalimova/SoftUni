using System;
using System.Collections.Generic;

namespace P03_FootballBetting.Data.Models
{
    public partial class Bet
    {
        public int BetId { get; set; }
        public decimal Amount { get; set; }
        public string Prediction { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
