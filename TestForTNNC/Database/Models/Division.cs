using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestForTNNC.Database.Models
{
    public class Division
    {
        public Division()
        { }
        public Division(int id, int? levelId, string name)
        {
            this.id = id;
            this.levels_id = levelId;
            this.name = name;
        }
        [Key]
        public int id { get; set; }
        public int? levels_id { get; set; }
        [ForeignKey("levels_id")]
        public Levels GetLevels { get; set; }
        public string name { get; set; }
        
    }
}
