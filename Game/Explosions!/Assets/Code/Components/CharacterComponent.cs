using UnityEngine;
using System.Collections;
using Assets.Code.Model;

namespace Assets.Code.Components
{
    /// <summary>
    /// The component for Character
    /// Other components will use this component to speak with the character
    /// </summary>
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(AttributeManager))]
    //TODO: Do we need CharacterComponent for anything other than autopopulating required components?
    public class CharacterComponent : GameComponent
    {
        //TODO: REMOVE ALL OF THIS
        //[SerializeField]
        //private Character stats; //Initialized by inspector by default (Demo character)

        //#region Properties
        //#region PercentHealth
        //public float PercentHealth
        //{
        //    get
        //    {
        //        return (float)stats.Health / (float)stats.MaxHealth;
        //    }
        //}
        //#endregion

        //public int Exp { get { return stats.Exp; } }

        //public int Level { get { return stats.Level; } }
        //#endregion

        //// Use this for initialization
        //public override void Start()
        //{
        //    base.Start();
        //    if (Game.CharacterSelected)
        //    {
        //        stats = Game.Character;
        //    }
        //    stats.ResetHealth();
        //}
    }
}