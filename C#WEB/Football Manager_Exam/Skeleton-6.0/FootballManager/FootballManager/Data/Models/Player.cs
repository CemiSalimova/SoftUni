using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Data.Models
{
    public class Player
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; }

        [Required]
        [StringLength(80)]
        public string FullName { get; set; }
        [StringLength(300)]
        public string ImageUrl { get; set; }
       
        [Required]
        [StringLength(20)]
        public string Position { get; set; }

        [Range(0, 10)]
        public byte Speed { get; set; }
        [Range(0, 10)]
        public byte Endurance { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        public ICollection<UserPlayer> UserPlayers { get; set; }

        public Player()
        {
            UserPlayers = new List<UserPlayer>();
            Id = Guid.NewGuid().ToString();
        }
    }
}
//•	Has Id – an int, Primary Keyok
//•	Has FullName – a string (required); min.length: 5, max.length: 80ok
//•	Has ImageUrl – a string (required)ok
//•	Has Position – a string (required); min.length: 5, max.length: 20ok
//•	Has Speed – a byte (required); cannot be negative or bigger than 10ok
//•	Has Endurance – a byte (required); cannot be negative or bigger than 10ok
//•	Has a Description – a string with max length 200 (required)ok
//•	Has UserPlayers collection
