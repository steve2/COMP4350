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
		#region PercentHealth

		// Decrease and increase health within the acceptable range
		// Health percentage will not exceed 100% or fall below 0%
		[Test]
		public void TestAcceptableRange()
		{
			// Create a health object
			// Should be initialized with 100% health
			Health health = new Health ();
			Assert.AreEqual (1, health.PercentHealth);
			Assert.IsFalse (health.isEmpty ());
			
			// Increase and decrease health
			health.decreaseHealth (40);
			Assert.AreEqual(0.6f, health.PercentHealth);
			health.increaseHealth (30);
			Assert.AreEqual(0.9f, health.PercentHealth);
			health.decreaseHealth (60);
			Assert.AreEqual(0.3f, health.PercentHealth);
			health.increaseHealth (50);
			Assert.AreEqual(0.8f, health.PercentHealth);
			health.decreaseHealth (70);
			Assert.AreEqual(0.1f, health.PercentHealth);
			health.decreaseHealth (5);
			Assert.AreEqual(0.05f, health.PercentHealth);
			health.increaseHealth (50);
			Assert.AreEqual(0.55f, health.PercentHealth);
			health.decreaseHealth (54);
			Assert.AreEqual(0.01f, health.PercentHealth);
		}

		// Increase health outside of the acceptable range
		// Health should not exceed 100%
		[Test]
		public void TestUnacceptableRangeIncrease()
		{
			// Create a health object
			// Should be initialized with 100% health
			Health health = new Health ();
			Assert.AreEqual (1, health.PercentHealth);
			Assert.IsFalse (health.isEmpty ());
			
			// Increase health
			health.increaseHealth (1);
			Assert.AreEqual(1, health.PercentHealth);
			health.increaseHealth (10);
			Assert.AreEqual (1, health.PercentHealth);
			health.increaseHealth (100);
			Assert.AreEqual (1, health.PercentHealth);
			health.increaseHealth (1000);
			Assert.AreEqual (1, health.PercentHealth);

			// Decrease health
			health.decreaseHealth (1);
			Assert.AreEqual (0.99f, health.PercentHealth);
		}

		// Decrease health outside of the acceptable range
		// Health should not fall below 0%
		[Test]
		public void TestUnacceptableRangeDecrease()
		{
			// Create a health object
			// Should be initialized with 100% health
			Health health = new Health ();
			Assert.AreEqual (1, health.PercentHealth);
			Assert.IsFalse (health.isEmpty ());

			//  Decrease health
			health.decreaseHealth (50);
			Assert.AreEqual (0.5f, health.PercentHealth);
			health.decreaseHealth (100);
			Assert.AreEqual(0, health.PercentHealth);
			health.decreaseHealth (1000);
			Assert.AreEqual (0, health.PercentHealth);
			Assert.IsTrue (health.isEmpty ());
		}

		// Increase health after it has reached 0%
		// Once health reaches 0, it is "dead" and any health increases cannot be applied
		[Test]
		public void TestIncreaseWhenEmpty()
		{
			// Create a health object and empty its health
			Health health = new Health ();
			Assert.AreEqual (1, health.PercentHealth);
			Assert.IsFalse (health.isEmpty ());
			health.decreaseHealth (100);
			Assert.IsTrue (health.isEmpty ());

			// Try increasing health
			health.increaseHealth (100);
			Assert.AreNotEqual (1, health.PercentHealth);
			Assert.AreEqual (0, health.PercentHealth);
			Assert.IsFalse (health.increaseHealth (1));
			Assert.IsTrue (health.isEmpty ());
			health.increaseHealth (1);
			Assert.AreNotEqual (1, health.PercentHealth);
			Assert.AreEqual (0, health.PercentHealth);
			Assert.IsFalse (health.increaseHealth (1));
			Assert.IsTrue (health.isEmpty ());
		}

		#endregion
	}
}
#endif