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
            ret.Awake();
            return ret;
        }

        [Test]
        public void TestAddNewAttributes()
        {
            const int expectedDamage = 50;
            const int expectedHealth = 100;
            AttributeManager mgr = Create();
            List<GameAttribute> attributes = new List<GameAttribute>();
            attributes.Add(new GameAttribute(AttributeType.Damage, expectedDamage));
            attributes.Add(new GameAttribute(AttributeType.Health, expectedHealth));

            mgr.AddAttributes(attributes);

            //We should have the new attributes
            Assert.AreEqual(expectedDamage, mgr.GetAttributeValue(AttributeType.Damage));
            Assert.AreEqual(expectedHealth, mgr.GetAttributeValue(AttributeType.Health));
        }

        [Test]
        public void TestAddExistingAttributes()
        {
            const int damageA = 20;
            const int damageB = 25;
            const int expectedDamage = damageA + damageB;
            AttributeManager mgr = Create();

            mgr.AddAttributes(new GameAttribute[] { new GameAttribute(AttributeType.Damage, damageA)});
            mgr.AddAttributes(new GameAttribute[] { new GameAttribute(AttributeType.Damage, damageB)});

            //The attribute should be updated
            Assert.AreEqual(expectedDamage, mgr.GetAttributeValue(AttributeType.Damage));
        }

        [Test]
        public void TestSubtractNewAttributes()
        {
            AttributeManager mgr = Create();
            List<GameAttribute> attributes = new List<GameAttribute>();
            attributes.Add(new GameAttribute(AttributeType.Damage,  100));
            attributes.Add(new GameAttribute(AttributeType.Health, 50));

            mgr.SubtractAttributes(attributes);

            //We should not have created any attributes 
            Assert.AreEqual(0, mgr.GetAttributeValue(AttributeType.Damage));
            Assert.AreEqual(0, mgr.GetAttributeValue(AttributeType.Health));
        }

        [Test]
        public void TestSubtractExistingAttributes()
        {
            const int damageA = 50;
            const int damageB = 13;
            const int expectedDamage = damageA - damageB;
            AttributeManager mgr = Create();

            mgr.AddAttributes(new GameAttribute[] { new GameAttribute(AttributeType.Damage, damageA) }); //Need to call Add first to create it
            mgr.SubtractAttributes(new GameAttribute[] { new GameAttribute(AttributeType.Damage, damageB) });

            //The attribute should be updated
            Assert.AreEqual(expectedDamage, mgr.GetAttributeValue(AttributeType.Damage));
        }

        [Test]
        public void TestGetNonExisting()
        {
            AttributeManager mgr = Create();
            Assert.AreEqual(0, mgr.GetAttributeValue(AttributeType.Damage));
        }
    }
}
#endif