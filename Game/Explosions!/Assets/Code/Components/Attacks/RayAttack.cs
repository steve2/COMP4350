using UnityEngine;

namespace Assets.Code.Components.Attacks
{
    public class RayAttack : Attack
    {
        public override void Use(int damage, int range)
        {
            RaycastHit hit;
            Vector3 dir = transform.forward; //Default to straight forward
            //TODO: Ray cast to target position

            //Raycast 
            if (Physics.Raycast(transform.position, dir, out hit, Mask) && 
                InRange(hit.transform.position, range)) //Range check
            {
                ApplyDamage(hit.collider.gameObject, damage);
            }
        }

        /// <summary>
        /// Checks if squared distance is less than squared range
        /// Magnitude is not used because square root is much less efficient than multipling the range
        /// </summary>
        /// <param name="hitPos">The position of the hit</param>
        /// <param name="range">Maximum range</param>
        /// <returns>Returns whether or not the hit position is within the maximum range</returns>
        private bool InRange(Vector3 hitPos, int range)
        {
            return (hitPos - transform.position).sqrMagnitude < (range * range); 
        }

        /// <summary>
        /// Applies damage to the targets DamageReceiver
        /// Returns if
        /// </summary>
        /// <param name="go">GameObject instance (Could be null)</param>
        /// <param name="damage"></param>
        /// <returns>How much damage is actually dealt</returns>
        private int ApplyDamage(GameObject go, int damage)
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
