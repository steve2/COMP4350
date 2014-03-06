using UnityEngine;
using System;
using System.Collections;

namespace Assets.Code.Components.Attacks
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(AttributeManager))]
    public class DamageReceiver : MonoBehaviour
    {
        private Health health;
        private AttributeManager attr;

        public void Start()
        {
            health = GetComponent<Health>();
            attr = GetComponent<AttributeManager>();
        }

        public int ReceiveDamage(int damage)
        {
            //TODO: Query AttributeManager for damage reduction (Armor, etc)
            int finalDamage = Math.Max(damage, 0); //Don't accept negative damage

            health.decreaseHealth(finalDamage);
            return finalDamage;
        }
    }
}