using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Components.Actions;

namespace Assets.Code.Editor.Stubs
{
    class GameActionStub : GameAction
    {
        public bool performed = false;

        public override void Start()
        {
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public override bool Perform()
        {
            performed = true;
            return true;
        }
    }
}
