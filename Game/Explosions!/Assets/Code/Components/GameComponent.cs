using UnityEngine;
using System.Collections;

namespace Assets.Code.Components
{
    /// <summary>
    /// All components that need access to the Game service should inherit from this
    /// </summary>
    public class GameComponent : MonoBehaviour
    {
        private Game gameInstance;
        public Game Game { get { return gameInstance; } }

        // Use this for initialization
        public virtual void Start()
        {
            this.gameInstance = Game.Instance;
        }
    }
}