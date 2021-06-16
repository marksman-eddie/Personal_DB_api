using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestForTNNC.Database.Models
{
    public class Position
    {
        public int id { get; set; }
        public int? division_id { get; set; }
        public string name { get; set; }
        [ForeignKey("division_id")]
        public Division Division_position { get; set; }
    }
}
