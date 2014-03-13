using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Model
{
    [Serializable]
    public class Character
    {
        public string Name { get; private set; }
		public int Exp { get; private set; }
		public int PlayTime { get; private set; }

        public static readonly Character SHOP = new Character("Shop", 0, 0);

        public Character(string name, int exp, int playTime)
        {
            this.Name = name;
			this.Exp = exp;
			this.PlayTime = playTime;
        }

		public string ToString(){
			return this.Name + " " + this.Exp + " " + this.PlayTime;
		}
    }
}
