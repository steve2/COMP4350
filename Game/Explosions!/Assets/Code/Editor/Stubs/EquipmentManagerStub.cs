using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Components;

namespace Assets.Code.Editor.Stubs
{
    /// <summary>
    /// Excludes file system prefab access from unit tests
    /// </summary>
    class EquipmentManagerStub : EquipmentManager
    {
        protected override Item InitPrefab(Item prefab)
        {
            return prefab;
        }

        protected override void DestroyPrefab(Item instance)
        {
            //Do nothing
        }
    }
}
