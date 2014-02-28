#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Code.Editor.Tests.Components
{
	[ExecuteInEditMode]
	class TestHealth
	{
		[Test]
		public void TestPercentHealth()
		{
			// Create a health object
			// Should be initialized with 100% health
			Health health = new Health ();
			Assert.AreEqual (1, health.PercentHealth);
			Assert.IsFalse (health.isEmpty ());

			// Decrease and increase health within the acceptable range
			// Float values are too precise, so the assertions fail 
			health.decreaseHealth (0.4f);
			Assert.AreEqual(0.6f, health.PercentHealth);
			health.increaseHealth (0.3f);
			Assert.AreEqual(0.9f, health.PercentHealth);
			health.decreaseHealth (0.6f);
			Assert.AreEqual(0.3f, health.PercentHealth);
			health.increaseHealth (5f);
			Assert.AreEqual(0.8f, health.PercentHealth);
			health.decreaseHealth (0.7f);
			Assert.AreEqual(0.1f, health.PercentHealth);

			// Increase health outside of the acceptable range
			// Health should not exceed 100%
			health.increaseHealth (0.5f);
			Assert.AreEqual(1, health.PercentHealth);
			health.increaseHealth (1);
			Assert.AreEqual(1, health.PercentHealth);
			health.increaseHealth (100);
			Assert.AreEqual (1, health.PercentHealth);
			health.increaseHealth (1000);
			Assert.AreEqual (1, health.PercentHealth);

			// Decrease health outside of the acceptable range
			// Health should not fall below 0%
			health.decreaseHealth (0.5f);
			Assert.AreEqual (0.5f, health.PercentHealth);
			health.decreaseHealth (1);
			Assert.AreEqual(0, health.PercentHealth);
			health.decreaseHealth (1000);
			Assert.AreEqual (0, health.PercentHealth);
			Assert.IsTrue (health.isEmpty ());

			// Increase health after it has reached 0%
			// Once health reaches 0, it is "dead" and any health increases cannot be applied
			health.increaseHealth (1);
			Assert.AreNotEqual (1, health.PercentHealth);
			Assert.AreEqual (0, health.PercentHealth);
			Assert.IsFalse (health.increaseHealth (1));
			Assert.IsTrue (health.isEmpty ());
		}
	}
}
#endif