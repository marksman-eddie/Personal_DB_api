using System;
namespace TestForTNNC.Models
{
    public class WorkerModel
    {
        public WorkerModel()
        {
        }
		public WorkerModel(int personal,int divisionId,string surname, string firstname, string fathername)
		{
			
            Surname = surname;
			Fathername = fathername;
            Firstname = firstname;
            Division_Id = divisionId;
            Personal_Id = personal;

        }

        public int Id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Fathername { get; set; }
        public int Position_Id { get; set; }       
        public int Personal_Id { get; set; }
        public int Division_Id { get; set; }

    }
}
