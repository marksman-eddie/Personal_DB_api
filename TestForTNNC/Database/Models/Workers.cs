using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestForTNNC.Database.Models
{
    public class Workers
    {
        public Workers()
        { }
        public int id { get; set; }
        public string surname { get; set; }
        public string firstname { get; set; }
        public string fathername { get; set; }
        public int position_Id { get; set; }
        
        public int personal_id { get; set; }
        public int division_id { get; set; }
    }
}
