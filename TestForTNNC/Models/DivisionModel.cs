using System;
using System.Collections.Generic;

namespace TestForTNNC.Models
{
    public class DivisionModel
    {
		public DivisionModel(int id, int? levelId, string name)
		{
			Id = id;
			LevelId = levelId;
			Name = name;
		}

		public int Id { get; set; }

		public int? LevelId { get; set; }

		public string Name { get; set; }
		public List<WorkerModel> Workers { get; set; } = new List<WorkerModel>();

		public override string ToString()
		{
			return Name;
		}
	}
}
