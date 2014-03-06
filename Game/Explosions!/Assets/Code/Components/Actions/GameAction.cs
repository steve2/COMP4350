using UnityEngine;
using System.Collections;

namespace Assets.Code.Components.Actions
{
    public abstract class GameAction : MonoBehaviour
    {
        public abstract void Perform();
        public abstract void Start();
    }
}