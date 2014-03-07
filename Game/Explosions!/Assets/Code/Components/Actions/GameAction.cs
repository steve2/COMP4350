using UnityEngine;
using System.Collections;

namespace Assets.Code.Components.Actions
{
    public abstract class GameAction : MonoBehaviour
    {
        [SerializeField]
        private string name;

        public virtual GameObject Source { get; set; }
        public virtual GameObject Target { get; set; }
        public string Name { get { return name; } }
        public abstract void Perform();
        public abstract void Start();
    }
}