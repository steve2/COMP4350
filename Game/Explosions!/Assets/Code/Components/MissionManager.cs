using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Components
{
    public class MissionManager : GameComponent
    {
        public override void Start()
        {
			base.Start();
            GameInst.ShowCharacter();
        }

        public void Destroy()
        {
            GameInst.HideCharacter();
        }
    }
}
