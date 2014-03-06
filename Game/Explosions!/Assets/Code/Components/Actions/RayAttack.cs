using Assets.Code.Model;
using UnityEngine;

namespace Assets.Code.Components.Actions
{
    public class RayAttack : DamageAction
    {
        [SerializeField]
        private LayerMask mask = 1;

        //TODO: Allow specifying a "target", which could be a rigidbody or simply a location

        protected override void PerformImpl(int damage)
        {
            RaycastHit hit;
            Vector3 dir = transform.forward; //Default to straight forward
            int range = AttrMgr.GetAttributeValue(AttributeType.Range);

            //TODO: Ray cast to target position
            //Raycast 
            if (Physics.Raycast(transform.position, dir, out hit, mask) && 
                InRange(hit.transform.position, range)) //Range check
            {
                ApplyDamage(hit.collider.gameObject, damage);
            }
        }

        /// <summary>
        /// Checks if squared distance is less than squared range
        /// Magnitude is not used because square root is much less efficient than multipling the range
        /// </summary>
        /// <param name="pos">The final position that will be range checked</param>
        /// <param name="range">Maximum range</param>
        /// <returns>Returns whether or not the hit position is within the maximum range</returns>
        //TODO: Make this a "Condition"?
        public bool InRange(Vector3 pos, int range)
        {
			Vector3 dist = pos - transform.position;
            return (dist).sqrMagnitude <= (range * range); 
        }

        /// <summary>
        /// Applies damage to the targets DamageReceiver
        /// Returns if
        /// </summary>
        /// <param name="go">GameObject instance (Could be null)</param>
        /// <param name="damage"></param>
        /// <returns>How much damage is actually dealt</returns>
        protected int ApplyDamage(GameObject go, int damage)
        {
            int ret = 0;
            DamageReceiver recv;
            if (go != null)
            {
                recv = go.GetComponent<DamageReceiver>();
                if (recv != null)
                {
                    ret = recv.ReceiveDamage(damage);
                }
            }
            return ret;
        }
    }
}
