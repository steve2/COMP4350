using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Components;
using Assets.Code.Model;

/// <summary>
/// Game Service
/// </summary>
public class Game
{
    private const string PRODUCTION_URL = "http://54.200.201.50";

    /// <summary>
    /// This is a service that provides communication with a "server" (Whether it's a real server or not)
    /// It can be changed during run-time
    /// Options: ProductionServer, TestServer, DemoServer, ServerStub
    /// </summary>
    private Server server;
    //Currently selected character
    private Character character; //TODO: Keep track of selected character 
    //Cache
    //TODO: We are using threading, we should probably include locks
    private IEnumerable<Mission> missions;
    private IEnumerable<Recipe> purchaseItems;
    private IEnumerable<Recipe> craftItems;

    public static Game Instance { get; private set; }

    public Character Character { get { return character; } }
    public bool CharacterSelected { get { return this.Character != null; } }

    #region Asynchronous Properties
    /****************************************************************************
     *  These properties will start a request for data and immediately return nothing
     *  But eventually they will return data
     *  If empty enumerable is returned, assume data is loading
    ****************************************************************************/

    public IEnumerable<Recipe> PurchasableItems
    {
        get
        {
            if (purchaseItems == null)
            {
                purchaseItems = Enumerable.Empty<Recipe>(); //Empty
                //TODO: lock?
                server.GetPurchasableItems((x) => { purchaseItems = x; });
            }
            return purchaseItems;
        }
    }

    public IEnumerable<Mission> Missions
    {
        get
        {
            if (missions == null)
            {
                missions = Enumerable.Empty<Mission>();
                server.GetMissions(character, (x) => { missions = x; });
            }
            return missions;
        }
    } 

    #endregion

    public Game(Server server)
    {
        this.server = server;
        this.character = null;
        this.purchaseItems = null;
        this.craftItems = null;
    }

    /// <summary>
    /// Returns whether or not the recipe was successful
    /// </summary>
    /// <param name="id">The recipe id we want to use</param>
    /// <returns></returns>
    public bool UseRecipe(Recipe recipe, Character inChar, Character outChar)
    {
        bool success = false;
        //TODO: Perform game-side verification of recipes (In here, or in a "Shop" class)
        //TODO: Store recipe until verified
        //TODO: Perform game-side execution of recipes

        //Assume this is successeful
        server.UseRecipe(recipe, inChar, outChar, (ok) =>
            {
                //if (ok)
                //{
                //    //TODO: Remove recipe from undo list
                //}
                //else
                //{
                //    //TODO: Undo recipe and notify user of error?
                //}
            }); 
        return success;
    }

    #region Initialization
    /// <summary>
    /// One of the first things to be called on startup
    /// </summary>
    public static void Init()
    {
        Instance = new Game(new Server(PRODUCTION_URL));
    }

    /// <summary>
    /// Support for full game dependency injection
    /// </summary>
    /// <param name="customInstance"></param>
    public static void Init(Game customInstance)
    {
        Instance = customInstance;
    }

    /// <summary>
    /// Support for server dependency injection
    /// </summary>
    /// <param name="customServer"></param>
    public static void Init(Server customServer)
    {
        Instance = new Game(customServer);
    } 
    #endregion

}
