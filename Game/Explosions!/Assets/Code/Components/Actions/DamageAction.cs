using UnityEngine;
using System.Collections;
using Assets.Code.Model;

namespace Assets.Code.Components.Actions
{
    /// <summary>
    /// An action that applies damage to something
    /// </summary>
    public abstract class DamageAction : GameAction
    {
        /// <summary>
        /// Damage actions can work with, or without an AttributeManager
        /// </summary>
        private AttributeManager attr;
        [SerializeField]
        private int defaultDamage = 0;

        protected AttributeManager AttrMgr;

        public override GameObject Source
        {
            get
            {
                return base.Source;
            }
            set
            {
                if (value != null)
                {
                    this.attr = value.GetComponent<AttributeManager>();
                }
                base.Source = value;
            }
        }

        public int Damage
        {
            get
            {
                if (AttrMgr != null)
                {
                    //TODO: Perform calculations? (Percentages, etc)
                    return AttrMgr.GetAttributeValue(AttributeType.Damage);
                }
                else
                {
                    return defaultDamage;
                }
            }
        }

        public override void Start()
        {
            attr = null;
        }

        /// <summary>
        /// Perform this damage action
        /// </summary>
        public override bool Perform()
        {
            return PerformImpl(Damage);
        }

        //Concrete implementation of specific damage action
        protected abstract bool PerformImpl(int damage);
    }
}