#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Collections.Generic;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestAttributeManager
    {
        private AttributeManager Create()
        {
            GameObject go = new GameObject();
            go.AddComponent<AttributeManager>();
            AttributeManager ret = go.GetComponent<AttributeManager>();
            //TODO: Find a way to get Unity to fully initialize this component
            ret.Start();
            return ret;
        }

        [Test]
        public void TestAddNewAttributes()
        {
            const int expectedDamage = 50;
            const int expectedHealth = 100;
            AttributeManager mgr = Create();
            List<ItemAttribute> attributes = new List<ItemAttribute>();
            attributes.Add(new ItemAttribute(AttributeType.Damage, expectedDamage));
            attributes.Add(new ItemAttribute(AttributeType.Health, expectedHealth));

            mgr.AddAttributes(attributes);

            //We should have the new attributes
            Assert.That(mgr.GetAttributeValue(AttributeType.Damage) == expectedDamage);
            Assert.That(mgr.GetAttributeValue(AttributeType.Health) == expectedHealth);
        }

        [Test]
        public void TestAddExistingAttributes()
        {
            const int damageA = 20;
            const int damageB = 25;
            const int expectedDamage = damageA + damageB;
            AttributeManager mgr = Create();

            mgr.AddAttributes(new ItemAttribute[] { new ItemAttribute(AttributeType.Damage, damageA)});
            mgr.AddAttributes(new ItemAttribute[] { new ItemAttribute(AttributeType.Damage, damageB)});

            //The attribute should be updated
            Assert.That(mgr.GetAttributeValue(AttributeType.Damage) == expectedDamage);
        }

        [Test]
        public void TestSubtractNewAttributes()
        {
            AttributeManager mgr = Create();
            List<ItemAttribute> attributes = new List<ItemAttribute>();
            attributes.Add(new ItemAttribute(AttributeType.Damage,  100));
            attributes.Add(new ItemAttribute(AttributeType.Health, 50));

            mgr.SubtractAttributes(attributes);

            //We should not have created any attributes
            Assert.That(mgr.GetAttributeValue(AttributeType.Damage) == 0);
            Assert.That(mgr.GetAttributeValue(AttributeType.Health) == 0);
        }

        [Test]
        public void TestSubtractExistingAttributes()
        {
            const int damageA = 50;
            const int damageB = 13;
            const int expectedDamage = damageA - damageB;
            AttributeManager mgr = Create();

            mgr.AddAttributes(new ItemAttribute[] { new ItemAttribute(AttributeType.Damage, damageA) }); //Need to call Add first to create it
            mgr.SubtractAttributes(new ItemAttribute[] { new ItemAttribute(AttributeType.Damage, damageB) });

            //The attribute should be updated
            Assert.That(mgr.GetAttributeValue(AttributeType.Damage) == expectedDamage);
        }

        public void TestGetNonExisting()
        {
            AttributeManager mgr = Create();
            Assert.That(mgr.GetAttributeValue(AttributeType.Damage) == 0);
        }
    }
}
#endif