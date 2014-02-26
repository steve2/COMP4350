using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Model
{
    [Serializable]
    public class Character
    {
        public string Name { get; private set; }
        public static readonly Character SHOP = new Character("Shop");

        public Character(string name)
        {
            this.Name = name;
        }
    }
}
