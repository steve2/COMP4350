using Assets.Code.Model;
using UnityEngine;

namespace Assets.Code.Components.Actions
{
    public class RayAttack : DamageAction
    {
        [SerializeField]
        private int defaultRange = 0;
        [SerializeField]
        private LayerMask mask = 1;

        private Ray lastRay;

        //TODO: Allow specifying a "target", which could be a rigidbody or simply a location

        public int Range
        {
            get
            {
                if (AttrMgr != null)
                {
                    //TODO: Perform calculations? (Percentages, etc)
                    return AttrMgr.GetAttributeValue(AttributeType.Range);
                }
                else
                {
                    return defaultRange;
                }
            }
        }

        //TODO: Exclude this if we're not in debug mode
        public void Update()
        {
        }

        public Vector3 StartPosition
        {
            get
            {
                //TODO: Start from GunBarrel, etc?
                if (Source == null)
                {
                    return transform.position;
                }
                return Source.transform.position; //TODO: What if source is null?
            }
        }
        //TODO: EndPosition

        protected override bool PerformImpl(int damage)
        {
            RaycastHit hit;
            Vector3 dir = transform.forward; //Default to straight forward

            //TODO: Ray cast to target position (Change direction
            //Raycast 
            Ray ray = new Ray(StartPosition, dir);
            Debug.DrawRay(ray.origin, ray.direction*Range, Color.red, 1); //Draw a ray for 1 second
            this.lastRay = ray; //TODO: Maybe build a queue for the view?
            if (Physics.Raycast(ray, out hit, Range, mask))
            {
                ApplyDamage(hit.collider.gameObject, damage);
                return true;
            }

            return false;
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
