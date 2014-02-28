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
	class TestExperience
	{
		[Test]
		public void TestLevelOne()
		{
			// Create an experience objecct
			// Should be initialized at 0
			Experience exp = new Experience ();
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);

			// Increase and decrease the experience 
			exp.IncreaseEXP (1);
			Assert.AreEqual (1, exp.CurrentEXP);
			Assert.AreEqual (0.01f, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.IncreaseEXP (50);
			Assert.AreEqual (51, exp.CurrentEXP);
			Assert.AreEqual (0.51f, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.DecreaseEXP (10);
			Assert.AreEqual (41, exp.CurrentEXP);
			Assert.AreEqual (0.41f, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.IncreaseEXP (58);
			Assert.AreEqual (99, exp.CurrentEXP);
			Assert.AreEqual (0.99f, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);

			// Level won't fall below 1 and experience won't fall below 0
			exp.DecreaseEXP (99);
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.DecreaseEXP (1);
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.DecreaseEXP (100);
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.DecreaseEXP (1000);
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
		}

		[Test]
		public void TestLevelUp()
		{
			// Create an experience objecct
			// Should be initialized at 0
			// Level will start at 1
			Experience exp = new Experience ();
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);

			// Level up
			exp.IncreaseEXP (99);
			Assert.AreEqual (99, exp.CurrentEXP);
			Assert.AreEqual (0.99f, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.IncreaseEXP (1);
			Assert.AreEqual (100, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (2, exp.Level);
			exp.IncreaseEXP (1);
			Assert.AreEqual (101, exp.CurrentEXP);
			Assert.AreEqual (0.01f, exp.PercentEXP);
			Assert.AreEqual (2, exp.Level);
			exp.IncreaseEXP (500);
			Assert.AreEqual (601, exp.CurrentEXP);
			Assert.AreEqual (0.01f, exp.PercentEXP);
			Assert.AreEqual (7, exp.Level);

		}

		[Test]
		public void TestLevelDown()
		{
			// Create an experience objecct and level it up to 10
			Experience exp = new Experience ();
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.IncreaseEXP (999);
			Assert.AreEqual (999, exp.CurrentEXP);
			Assert.AreEqual (0.99f, exp.PercentEXP);
			Assert.AreEqual (10, exp.Level);
			
			// Level down
			exp.DecreaseEXP (99);
			Assert.AreEqual (900, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (10, exp.Level);
			exp.DecreaseEXP (1);
			Assert.AreEqual (899, exp.CurrentEXP);
			Assert.AreEqual (0.99f, exp.PercentEXP);
			Assert.AreEqual (9, exp.Level);
			exp.DecreaseEXP (100);
			Assert.AreEqual (799, exp.CurrentEXP);
			Assert.AreEqual (0.99f, exp.PercentEXP);
			Assert.AreEqual (8, exp.Level);
			exp.DecreaseEXP (1000);
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
		}
	}
}
#endif