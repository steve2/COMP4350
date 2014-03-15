using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Model
{
    public class Recipe
    {
        public string Name { get; private set; }
		public int Quantity { get; private set; }
		public int RecipeID { get; private set; }

		public Recipe (int cost, int id, string name)
		{
			this.Name = name;
			this.Quantity = cost;
			this.RecipeID = id;
		}
		//TODO: Implement recipe
    }
}
