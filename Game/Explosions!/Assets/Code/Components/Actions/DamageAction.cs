using UnityEngine;
using System.Collections;

namespace Assets.Code.Components.Actions
{
    public abstract class DamageAction : GameAction
    {
        //TODO: Allow specifying a "target", which could be a rigidbody or simply a location

        public abstract void Use(int damage, int range);
        public abstract bool InRange(Vector3 pos, int range);

        public override void Perform()
        {
            int damage = 0;
            int range = 0;
            Use(damage, range);
        }
    }
}