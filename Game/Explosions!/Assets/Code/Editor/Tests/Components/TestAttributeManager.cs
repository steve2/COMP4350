#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestAttributeManager
    {
        private AttributeManager Create()
        {
            AttributeManager ret = new AttributeManager();
            ret.Start();
            return ret;
        }

        [Test]
        public void TestAddAttributes()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void TestSubtractAttributes()
        {
            Assert.Inconclusive();
        }
    }
}
#endif