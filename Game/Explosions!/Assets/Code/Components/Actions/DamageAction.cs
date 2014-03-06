using UnityEngine;
using System.Collections;
using Assets.Code.Model;

namespace Assets.Code.Components.Actions
{
    [RequireComponent(typeof(AttributeManager))]
    public abstract class DamageAction : GameAction
    {
        private AttributeManager attr;

        protected AttributeManager AttrMgr;

        public override void Start()
        {
            attr = GetComponent<AttributeManager>();
        }

        /// <summary>
        /// Perform this damage action
        /// </summary>
        public override void Perform()
        {
            int damage = AttrMgr.GetAttributeValue(AttributeType.Damage);
            //TODO: Percent calculation?
            PerformImpl(damage);
        }

        //Concrete implementation of specific damage action
        protected abstract void PerformImpl(int damage);
    }
}