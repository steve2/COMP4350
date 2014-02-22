using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public static Game Instance { get; private set; }

    //TODO: Keep track of selected character 
    //TODO: Create Mission class

    //public IEnumerable<Mission> GetMissions()
    //{
    //    //TODO: Call server.GetMission(characterID); //We know the character ID
    //}

    /// <summary>
    /// One of the first things to be called on startup
    /// </summary>
    public static void Init()
    {
        Instance = new Game();
        Instance.server = new Server(PRODUCTION_URL); //Default for now
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
        Instance = new Game();
        Instance.server = customServer;
    }
}
