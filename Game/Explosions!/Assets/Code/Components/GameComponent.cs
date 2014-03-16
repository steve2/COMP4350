using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code.Components
{
    /// <summary>
    /// All components that need access to the Game service should inherit from this
    /// </summary>
    public class GameComponent : MonoBehaviour
    {
        private Controller.Game gameInstance;
        public Controller.Game GameInst { get { return gameInstance; } }

        // Use this for initialization
        public virtual void Start()
        {
            this.gameInstance = Controller.Game.Instance;
        }
    }
}