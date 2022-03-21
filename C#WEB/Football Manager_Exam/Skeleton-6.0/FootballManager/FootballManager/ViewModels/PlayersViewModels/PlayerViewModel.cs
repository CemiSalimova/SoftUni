using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.ViewModels.PlayersViewModels
{
    public class PlayerViewModel
    {
        [StringLength(80)]
        public string FullName { get; set; }

        [StringLength(300)]
        public string ImageUrl { get; set; }
        [StringLength(20)]
        public string Position { get; set; }
        [Range(0, 10)]
        public byte Speed { get; set; }
        [Range(0, 10)]
        public byte Endurance { get; set; }
        public string Description { get; set; }
    }
}
