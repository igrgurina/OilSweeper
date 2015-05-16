using System;
using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{

    private const string typeName = "OilSweeper";
    private const int ConnectionLimit = 1; // Zero based (2 players)
    private int port = 25000 + (int)(DateTime.Now - DateTime.Today).TotalSeconds % 2000;

    private HostData[] hostList;
    private string gameName = "OilSweeping";

    public GameObject Player; // player prefab

    private void Start()
    {
        ClearLog();
        Log("Chosen port: " + port.ToString());
        RefreshHostList();
    }

    #region ServerControl

    private void StartServer()
    {
        var error = Network.InitializeServer(ConnectionLimit, port, !Network.HavePublicAddress());

        if (error == NetworkConnectionError.NoError)
        {
            Network.maxConnections = ConnectionLimit;
            gameName = "OilSweeping_" + DateTime.UtcNow.Ticks.ToString();
            MasterServer.RegisterHost(typeName, gameName);
            Log("Game name: " + gameName);
        }
        else
        {
            Log("Server creation failed with error " + error.ToString());
        }
    }

    private NetworkConnectionError JoinServer(HostData hostData)
    {
        var error = Network.Connect(hostData);
        if (error == NetworkConnectionError.NoError)
        {
            gameName = hostData.gameName;
            Log("Joining server " + gameName + "...");
        }
        else
        {
            Log("Error connecting to server: " + error.ToString());
        }
        return error;
    }

    private void RefreshHostList()
    {
        MasterServer.ClearHostList();
        MasterServer.RequestHostList(typeName);
        hostList = MasterServer.PollHostList();
    }

    private void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Log("\t*msEvent[" + msEvent.ToString() + "]");
        switch (msEvent)
        {
            case MasterServerEvent.HostListReceived:
                OnHostListRecieved();
                break;
            case MasterServerEvent.RegistrationSucceeded:
                OnRegistrationSucceeded();
                break;
        }
    }

    private void OnHostListRecieved() {
        hostList = MasterServer.PollHostList();
        ListHosts();
        var connectionSucceeded = false;
        // Connect to the first available host
        foreach (var host in hostList) {
            if (host.AvailableForJoin()) {
                var error = JoinServer(host);
                if (error == NetworkConnectionError.NoError)
                {
                    connectionSucceeded = true;
                    break;
                }
                else
                {
                    Log(error.ToString());
                }
            }
        }
        // Unable to connect to a host, create a new one
        if (!connectionSucceeded) {
            StartServer();
        }
    }

    private void OnRegistrationSucceeded()
    {
        
    }

    

    void OnServerInitialized() {
        DontDestroyOnLoad(transform.gameObject);
        Debug.Log("Server Initializied");
    }

    void OnConnectedToServer() {
        Debug.Log("Server Joined");
    }

    #endregion

    #region Logging

    private void Log(string text)
    {
        Debug.Log(text);
    }

    private void ClearLog()
    {
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

public static partial class ExtensionMethods
{
    public static bool AvailableForJoin(this HostData host)
    {
        return host.connectedPlayers < host.playerLimit;
    }
}
