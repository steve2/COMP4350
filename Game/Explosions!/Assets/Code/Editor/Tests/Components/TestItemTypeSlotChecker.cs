#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Assets.Code.Components;
using Assets.Code.Model;
using System.Collections.Generic;
using Assets.Code.Controller;

namespace Assets.Code.Editor.Tests.Components
{
    [ExecuteInEditMode]
    class TestItemTypeSlotChecker
    {
		ItemTypeSlotChecker slotPermissions;

		public void Setup()
		{
			slotPermissions = new ItemTypeSlotChecker();
		}

		[Test]
		public void TestAddingPermissions()
		{
			Setup ();
		
			Assert.True (slotPermissions.AddSlotToType (ItemType.Weapon, Slot.RightHand));
			Assert.True (slotPermissions.AddSlotToType (ItemType.Weapon, Slot.LeftHand));
			Assert.True (slotPermissions.AddSlotToType (ItemType.Legs, Slot.Legs));
			
			Assert.False (slotPermissions.AddSlotToType(ItemType.Weapon, Slot.RightHand));
			Assert.False (slotPermissions.AddSlotToType(ItemType.Weapon, Slot.LeftHand));
			Assert.False (slotPermissions.AddSlotToType(ItemType.Legs, Slot.Legs));
		}

		[Test]
		public void TestCheckingPermissions()
		{
			Setup ();

			slotPermissions.AddSlotToType(ItemType.Weapon, Slot.RightHand);
			slotPermissions.AddSlotToType(ItemType.Weapon, Slot.LeftHand);
			slotPermissions.AddSlotToType(ItemType.Legs, Slot.Legs);

			Assert.True (slotPermissions.CheckSlotPermission(ItemType.Weapon, Slot.RightHand));
			Assert.True (slotPermissions.CheckSlotPermission(ItemType.Weapon, Slot.LeftHand));
			Assert.True (slotPermissions.CheckSlotPermission(ItemType.Legs,   Slot.Legs));

			Assert.False (slotPermissions.CheckSlotPermission(ItemType.Weapon, Slot.Chest));
			Assert.False (slotPermissions.CheckSlotPermission(ItemType.Weapon, Slot.Legs));

			Assert.False (slotPermissions.CheckSlotPermission(ItemType.Legs, Slot.RightHand));
			Assert.False (slotPermissions.CheckSlotPermission(ItemType.Legs, Slot.Chest));
		}
	}
}
#endif