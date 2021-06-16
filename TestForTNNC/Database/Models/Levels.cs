using System;
using System.Collections.Generic;

namespace TestForTNNC.Database.Models
{
    public class Levels
    {
        public Levels()
        {

        }
        
        public int id { get; set; }
        public int? parent_id { get; set; }
        public string name { get; set; }
        
    }
}
