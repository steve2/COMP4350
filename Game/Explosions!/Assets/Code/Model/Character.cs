using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Model
{
    [Serializable]
    public class Character
    {
		public int Id { get; private set; }
        public string Name { get; private set; }
		public int Exp { get; private set; }
		public int PlayTime { get; private set; }

        public static readonly Character SHOP = new Character(0, "Shop", 0, 0);

        public Character(int id, string name, int exp, int playTime)
        {
			this.Id = id;
            this.Name = name;
			this.Exp = exp;
			this.PlayTime = playTime;
        }

		public string ToString(){
			return this.Name + " " + this.Exp + " " + this.PlayTime;
		}
    }
}
