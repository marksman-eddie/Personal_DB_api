using System;
using System.Collections.Generic;
using TestForTNNC.Database.Models;

namespace TestForTNNC.Models
{
    public class LevelModel
    {
		public LevelModel() { }
		public LevelModel(int id, int? parentId, string name)
		{
			Id = id;
			ParentId = parentId;
			Name = name;
		}

		public int Id { get; set; }

		public int? ParentId { get; set; }

		public string Name { get; set; }

		public List<LevelModel> Children { get; set; } = new List<LevelModel>();

		public List<DivisionModel> Divisions { get; set; } = new List<DivisionModel>();

		


		public override string ToString()
		{
			return Name;
		}
	}
}
