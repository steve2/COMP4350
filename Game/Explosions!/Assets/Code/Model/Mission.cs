using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Model
{
	[Serializable]
	public class Mission
	{
		public int ID { get; private set; }
		
		public Mission(int ID)
		{
			this.ID = ID;
		}
	}
}