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
		// Keep the EXP tests together for now because they are closely related
		[Test]
		public void TestEXP()
		{
			// Create an experience objecct
			// Should be initialized at 0
			// Level will start at 1
			Experience exp = new Experience ();
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);

			// Increase and decrease the experience
			exp.IncreaseEXP (50);
			Assert.AreEqual (50, exp.CurrentEXP);
			Assert.AreEqual (50f, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);
			exp.IncreaseEXP (500);
			Assert.AreEqual (550, exp.CurrentEXP);
			Assert.AreEqual (50f, exp.PercentEXP);
			Assert.AreEqual (6, exp.Level);
			exp.DecreaseEXP (50);
			Assert.AreEqual (500, exp.CurrentEXP);
			Assert.AreEqual (0f, exp.PercentEXP);
			Assert.AreEqual (6, exp.Level);
			exp.DecreaseEXP (1000);
			Assert.AreEqual (0, exp.CurrentEXP);
			Assert.AreEqual (0, exp.PercentEXP);
			Assert.AreEqual (1, exp.Level);

		}
	}
}
#endif