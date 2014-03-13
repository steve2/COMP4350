#if UNITY_EDITOR
using Assets.Code.Components;
using Assets.Code.Components.Actions;
using Assets.Code.Editor.Stubs;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Code.Editor.Tests.Components.Actions
{
    [ExecuteInEditMode]
    class TestRayAttack
    {
        private GameObject Create(bool protectedVisible = false)
        {
            GameObject go = new GameObject();
            if (protectedVisible)
                go.AddComponent<RayAttackAccess>();
            else
                go.AddComponent<RayAttack>();
            //Default positioning
            go.transform.position = Vector3.zero;
            go.transform.forward = Vector3.forward;
            return go;
        }

        private GameObject CreateTarget(Vector3 pos)
        {
            GameObject go = new GameObject();
            go.AddComponent<DamageReceiver>();
            go.AddComponent<Health>();
            go.AddComponent<AttributeManager>();
            go.transform.position = pos;
            return go;
        }

        private GameObject CreateUnitTarget(Vector3 pos)
        {
            GameObject go = new GameObject();
            go.AddComponent<DamageReceiverStub>();
            go.transform.position = pos;
            return go;
        }

        private GameObject CreateIntegrationTarget(Vector3 pos)
        {
            GameObject go = new GameObject();
            go.AddComponent<DamageReceiver>();
            go.AddComponent<Health>();
            go.AddComponent<AttributeManager>();
            go.transform.position = pos;
            return go;
        }

        //TODO: Perform integration test on full: RayAttack -> DamageReceiver(AttributeManager) -> Health 

        #region ApplyDamage
        [Test]
        public void TestApplyDamage()
        {
            int expectedDmg = 50;
            GameObject attacker = Create(true);
            RayAttackAccess attack = attacker.GetComponent<RayAttackAccess>();
            GameObject target = CreateUnitTarget(Vector3.zero);
            //TODO: Find out how to mock properly in Unity
            DamageReceiverStub stub = target.GetComponent<DamageReceiverStub>();
            Assert.AreEqual(expectedDmg, attack._ApplyDamage(target, expectedDmg)); //Verify damage is returned correctly in the simple case
            Assert.AreEqual(expectedDmg, stub.sentDamage); //Verify the damage sent is the same as what was returned in the simple case
        }

        [Test]
        public void TestApplyDamage_Halfed()
        {
            int sentDmg = 50;
            int expectedDmg = sentDmg / 2;
            GameObject attacker = Create(true);
            RayAttackAccess attack = attacker.GetComponent<RayAttackAccess>();
            GameObject target = CreateUnitTarget(Vector3.zero);
            //TODO: Find out how to mock properly in Unity
            DamageReceiverStub stub = target.GetComponent<DamageReceiverStub>();
            stub.ModifyDamage = (dmg) => { return dmg / 2; };
            Assert.AreEqual(expectedDmg, attack._ApplyDamage(target, sentDmg)); //Verify half damage is returned
            Assert.AreEqual(sentDmg, stub.sentDamage); //Verify the damage sent is correct
        }

        [Test]
        public void TestApplyDamage_NullGameObject()
        {
            GameObject attacker = Create(true);
            RayAttackAccess attack = attacker.GetComponent<RayAttackAccess>();
            Assert.AreEqual(0, attack._ApplyDamage(null, 50)); //Verify no damage is returned if GameObject was null
        }

        [Test]
        public void TestApplyDamage_NullDamageReceiver()
        {
            GameObject attacker = Create(true);
            RayAttackAccess attack = attacker.GetComponent<RayAttackAccess>();
            GameObject target = new GameObject();
            Assert.AreEqual(0, attack._ApplyDamage(target, 50)); //Verify no damage is returned if DamageReceiver was null
        } 
        #endregion

        //TODO: Remove
        #region InRange
        //private void TestRelativeRangeTrue(GameObject attacker, RayAttack attack, int range, int distance)
        //{
        //    Vector3 attackerPos = attacker.transform.position;
        //    Assert.IsTrue(attack.InRange(attackerPos + attacker.transform.forward * distance, range));
        //    Assert.IsTrue(attack.InRange(attackerPos - (attacker.transform.forward * distance), range)); //back
        //    Assert.IsTrue(attack.InRange(attackerPos + attacker.transform.right * distance, range));
        //    Assert.IsTrue(attack.InRange(attackerPos - (attacker.transform.right * distance), range)); //left
        //}

        //private void TestRelativeRangeFalse(GameObject attacker, RayAttack attack, int range, int distance)
        //{
        //    Vector3 attackerPos = attacker.transform.position;
        //    Assert.IsFalse(attack.InRange(attackerPos + attacker.transform.forward * distance, range));
        //    Assert.IsFalse(attack.InRange(attackerPos - (attacker.transform.forward * distance), range)); //back
        //    Assert.IsFalse(attack.InRange(attackerPos + attacker.transform.right * distance, range));
        //    Assert.IsFalse(attack.InRange(attackerPos - (attacker.transform.right * distance), range)); //left
        //}

        //[Test]
        //public void TestInsideRange()
        //{
        //    GameObject attacker = Create();
        //    RayAttack attack = attacker.GetComponent<RayAttack>();
        //    TestRelativeRangeTrue(attacker, attack, 500, 400);
        //}

        //[Test]
        //public void TestInsideRangeMoved()
        //{
        //    GameObject attacker = Create();
        //    RayAttack attack = attacker.GetComponent<RayAttack>();
        //    attacker.transform.position = new Vector3(10, 7, 30); //Position shouldn't matter
        //    TestRelativeRangeTrue(attacker, attack, 500, 400);
        //}

        //[Test]
        //public void TestExactRange()
        //{
        //    GameObject attacker = Create();
        //    RayAttack attack = attacker.GetComponent<RayAttack>();
        //    TestRelativeRangeTrue(attacker, attack, 300, 300);
        //}

        //[Test]
        //public void TestExactRangeMoved()
        //{
        //    GameObject attacker = Create();
        //    RayAttack attack = attacker.GetComponent<RayAttack>();
        //    attacker.transform.position = new Vector3(3, -10, 5); //Position shouldn't matter
        //    TestRelativeRangeTrue(attacker, attack, 300, 300);
        //}

        //[Test]
        //public void TestOutsideRange()
        //{
        //    GameObject attacker = Create();
        //    RayAttack attack = attacker.GetComponent<RayAttack>();
        //    TestRelativeRangeFalse(attacker, attack, 300, 500);
        //}

        //[Test]
        //public void TestOutsideRangeMoved()
        //{
        //    GameObject attacker = Create();
        //    RayAttack attack = attacker.GetComponent<RayAttack>();
        //    attacker.transform.position = new Vector3(38, 5, -5); //Position shouldn't matter
        //    TestRelativeRangeFalse(attacker, attack, 300, 500);
        //}

        //[Test]
        //public void TestCloseRanges()
        //{
        //    GameObject attacker = Create();
        //    RayAttack attack = attacker.GetComponent<RayAttack>();
        //    TestRelativeRangeFalse(attacker, attack, 300, 301);
        //    TestRelativeRangeTrue(attacker, attack, 300, 299);
        //}

        //[Test]
        //public void TestCloseRangesMoved()
        //{
        //    GameObject attacker = Create();
        //    RayAttack attack = attacker.GetComponent<RayAttack>();
        //    attacker.transform.position = new Vector3(-4, 50, 1); //Position shouldn't matter
        //    TestRelativeRangeFalse(attacker, attack, 300, 301);
        //    TestRelativeRangeTrue(attacker, attack, 300, 299);
        //} 
        #endregion

    }
}
#endif