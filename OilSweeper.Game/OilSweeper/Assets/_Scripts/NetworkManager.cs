using System;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    public Text StatusText;
    public GameObject User;

    private const string typeName = "OilSweeper";
    private const int ConnectionLimit = 1; // Zero based (2 players)
    private readonly int port = 25000 + (int)(DateTime.Now - DateTime.Today).TotalSeconds % 2000;
    private HostData[] hostList;
    private string gameName = "OilSweeping";

    private void Start()
    {
        ClearLog();
        Log("Chosen port: " + port);
        RefreshHostList();
    }

    /// <summary>
    /// Spawns players and starts the game
    /// </summary>
    [RPC]
    private void StartGame()
    {
        GameObject game = (GameObject)Network.Instantiate(User, new Vector3(), Quaternion.identity, 0);
        Log("Starting the game");

        // Set the player's color
        game.GetComponent<UserController>().Color = Network.isServer ? Color.blue : Color.red;
    }

    private void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Log("\t*msEvent[" + msEvent.ToString() + "]");
        if (msEvent == MasterServerEvent.HostListReceived)
        {
            OnHostListRecieved();         
        }
    }

    /// <summary>
    /// Called on a server after initialization
    /// </summary>
    void OnServerInitialized() {
        Log("Server Initializied");
    }

    /// <summary>
    /// Called on a client when connected on server
    /// </summary>
    void OnConnectedToServer() {
        Log("Server Joined");
        // Start the game on the client
        StartGame();
    }

    /// <summary>
    /// Called on a server when a client connects
    /// </summary>
    void OnPlayerConnected(NetworkPlayer player)
    {
        Log("Player " + player.ipAddress + " connected");
        // Start the game on the server
        StartGame();
    }

    #region ServerConnectionControl

    private void RefreshHostList() {
        MasterServer.ClearHostList();
        MasterServer.RequestHostList(typeName);
        hostList = MasterServer.PollHostList();
    }

    private void StartServer() {
        var error = Network.InitializeServer(ConnectionLimit, port, !Network.HavePublicAddress());

        if (error == NetworkConnectionError.NoError) {
            Network.maxConnections = ConnectionLimit;
            gameName = "OilSweeping_" + DateTime.UtcNow.Ticks.ToString();
            MasterServer.RegisterHost(typeName, gameName);
            Log("Game name: " + gameName);
        } else {
            Log("Server creation failed with error " + error.ToString());
        }
    }

    private NetworkConnectionError JoinServer(HostData hostData) {
        var error = Network.Connect(hostData);
        if (error == NetworkConnectionError.NoError) {
            gameName = hostData.gameName;
            Log("Joining server " + gameName + "...");
        } else {
            Log("Error connecting to server: " + error.ToString());
        }
        return error;
    }

    /// <summary>
    /// Called when the host list gets recieved
    /// </summary>
    private void OnHostListRecieved() {
        hostList = MasterServer.PollHostList();
        ListHosts();
        var connectionSucceeded = false;
        // Connect to the first available host
        foreach (var host in hostList) {
            if (host.AvailableForJoin()) {
                var error = JoinServer(host);
                if (error == NetworkConnectionError.NoError) {
                    connectionSucceeded = true;
                    break;
                } else {
                    Log(error.ToString());
                }
            }
        }
        // Unable to connect to a host, create a new one
        if (!connectionSucceeded) {
            StartServer();
        }
    }

    #endregion

    #region Logging

    public void Log(string text)
    {
        if (StatusText != null)
        {
            StatusText.text += text + "\n";
        }
        else
        {
            Debug.Log(text);
        }
    }

    private void ClearLog()
    {
        if (StatusText != null)
        {
            StatusText.text = "";
        }
    }

    private void ListHosts()
    {
        Log("Available OilSweeper hosts:");
        if (hostList.Length == 0) {
            Log("None");
        } else {
            foreach (var h in hostList) {
                Log(" - " + h.gameName + "; #Players: " + h.connectedPlayers + "; #MaxPlayers: " + h.playerLimit);
            }
        }
    }

    #endregion

}
