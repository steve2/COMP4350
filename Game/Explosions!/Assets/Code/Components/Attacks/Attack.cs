using UnityEngine;
using System.Collections;

namespace Assets.Code.Components.Attacks
{
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField]
        private LayerMask mask;
        //TODO: Allow specifying a "target", which could be a rigidbody or simply a location

        public abstract void Use(int damage, int range);
        public abstract bool InRange(Vector3 pos, int range);

        protected LayerMask Mask { get { return mask; } }
    }
}