using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Components;
using Assets.Code.Model;
using SimpleJSON;

namespace Assets.Code.Controller
{
    public class LocalServer : Server
    {
        private IEnumerable<Item> inventory;
        private IEnumerable<Item> equipment;

        public LocalServer() : base("localhost") { }

        //Nothing should be calling this (Catches lack of overriding a functionality)
        protected override void AsyncSend(string path, JSONClass json, Action<JSONNode> asyncReturn)
        {
            throw new NotImplementedException(); 
        }

        protected override JSONNode Send(string path, JSONClass json)
        {
            throw new NotImplementedException(); 
        }

        public override void IsAlive(Action<bool> asyncReturn)
        {
            asyncReturn(true);
        }

        public override void UseRecipe(Recipe recipe, Model.Character inChar, Character outChar, Action<bool> asyncReturn)
        {
            asyncReturn(true);
        }

        public override void GetInventory(Character character, Action<IEnumerable<Item>> asyncReturn)
        {
            asyncReturn(inventory);
        }

        public override void GetEquipment(Character character, Action<IEnumerable<Item>> asyncReturn)
        {
            asyncReturn(equipment);
        }

        internal void SetInventory(List<Item> inventory)
        {
            this.inventory = inventory;
        }

        internal void SetEquipment(List<Item> equipment)
        {
            this.equipment = equipment;
        }
    }
}
