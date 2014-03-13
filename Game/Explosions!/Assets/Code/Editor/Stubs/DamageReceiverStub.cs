using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Components.Actions;

namespace Assets.Code.Editor.Stubs
{
    class DamageReceiverStub : DamageReceiver
    {
        public int sentDamage;

        public Func<int, int> ModifyDamage;

        public override int ReceiveDamage(int damage)
        {
            this.sentDamage = damage;
            if (ModifyDamage != null)
            {
                damage = ModifyDamage(damage);
            }
            return damage;
        }
    }
}
