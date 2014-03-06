#if UNITY_EDITOR
using Assets.Code.Components.Actions;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Code.Editor.Tests.Components.Attacks
{
    [ExecuteInEditMode]
    class TestRayAttack
    {
        private GameObject Create()
        {
            GameObject go = new GameObject();
            go.AddComponent<RayAttack>();
            //Default positioning
            go.transform.position = Vector3.zero;
            go.transform.forward = Vector3.forward;
            return go;
        }

        private GameObject CreateTarget(Vector3 pos)
        {
            GameObject go = new GameObject();
            go.transform.position = pos;
            return go;
        }

        private void TestRelativeRangeTrue(GameObject attacker, RayAttack attack, int range, int distance)
        {
			Vector3 attackerPos = attacker.transform.position;
            Assert.IsTrue(attack.InRange(attackerPos + attacker.transform.forward * distance, range));
			Assert.IsTrue(attack.InRange(attackerPos -(attacker.transform.forward * distance), range)); //back
			Assert.IsTrue(attack.InRange(attackerPos + attacker.transform.right * distance, range));
			Assert.IsTrue(attack.InRange(attackerPos -(attacker.transform.right * distance), range)); //left
        }

        private void TestRelativeRangeFalse(GameObject attacker, RayAttack attack, int range, int distance)
        {
			Vector3 attackerPos = attacker.transform.position;
			Assert.IsFalse(attack.InRange(attackerPos + attacker.transform.forward * distance, range));
			Assert.IsFalse(attack.InRange(attackerPos -(attacker.transform.forward * distance), range)); //back
			Assert.IsFalse(attack.InRange(attackerPos + attacker.transform.right * distance, range));
			Assert.IsFalse(attack.InRange(attackerPos -(attacker.transform.right * distance), range)); //left
        }

        [Test]
        public void TestInsideRange()
        {
            GameObject attacker = Create();
            RayAttack attack = attacker.GetComponent<RayAttack>();
            TestRelativeRangeTrue(attacker, attack, 500, 400);
        }

        [Test]
        public void TestInsideRangeMoved()
        {
            GameObject attacker = Create();
            RayAttack attack = attacker.GetComponent<RayAttack>();
            attacker.transform.position = new Vector3(10, 7, 30); //Position shouldn't matter
            TestRelativeRangeTrue(attacker, attack, 500, 400);
        }

        [Test]
        public void TestExactRange()
        {
            GameObject attacker = Create();
            RayAttack attack = attacker.GetComponent<RayAttack>();
            TestRelativeRangeTrue(attacker, attack, 300, 300);
        }

        [Test]
        public void TestExactRangeMoved()
        {
            GameObject attacker = Create();
            RayAttack attack = attacker.GetComponent<RayAttack>();
            attacker.transform.position = new Vector3(3, -10, 5); //Position shouldn't matter
            TestRelativeRangeTrue(attacker, attack, 300, 300);
        }

        [Test]
        public void TestOutsideRange()
        {
            GameObject attacker = Create();
            RayAttack attack = attacker.GetComponent<RayAttack>();
            TestRelativeRangeFalse(attacker, attack, 300, 500);
        }

        [Test]
        public void TestOutsideRangeMoved()
        {
            GameObject attacker = Create();
            RayAttack attack = attacker.GetComponent<RayAttack>();
            attacker.transform.position = new Vector3(38, 5, -5); //Position shouldn't matter
            TestRelativeRangeFalse(attacker, attack, 300, 500);
        }

        [Test]
        public void TestCloseRanges()
        {
            GameObject attacker = Create();
            RayAttack attack = attacker.GetComponent<RayAttack>();
            TestRelativeRangeFalse(attacker, attack, 300, 301);
            TestRelativeRangeTrue(attacker, attack, 300, 299);
        }

        [Test]
        public void TestCloseRangesMoved()
        {
            GameObject attacker = Create();
            RayAttack attack = attacker.GetComponent<RayAttack>();
            attacker.transform.position = new Vector3(-4, 50, 1); //Position shouldn't matter
            TestRelativeRangeFalse(attacker, attack, 300, 301);
            TestRelativeRangeTrue(attacker, attack, 300, 299);
        }

    }
}
#endif