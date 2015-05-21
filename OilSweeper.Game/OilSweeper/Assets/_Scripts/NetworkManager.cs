using System;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : LoggingMonoBehaviour
{
    public Text MyGoldText;
    public Text NemesisGoldText;

    private const string roomName = "OilSweeperRoom";

	// Use this for initialization
	void Start () {
        Log("Starting Photon...");
        PhotonNetwork.ConnectUsingSettings("0.1");
	}

    /// <summary>
    /// Photon master server connected
    /// </summary>
    void OnJoinedLobby()
    {
        // Connect to the first room available
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        // Unable to connect to a room, create a new one
        PhotonNetwork.CreateRoom(roomName + Guid.NewGuid(), new RoomOptions {
            maxPlayers = 2,
            isOpen = true,
            isVisible = true
        }, TypedLobby.Default);
    }
    
    void OnJoinedRoom() {
        Log("Connected to room " + PhotonNetwork.room);
        if (!PhotonNetwork.isMasterClient)
        {
            StartGame();
        }
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Log("Player " + newPlayer.name + " joined the game");
        StartGame(); // Server
    }

    void StartGame()
    {
        Log("Starting game...");
        // Add player and game board
        var player = PhotonNetwork.Instantiate("Player", new Vector3(), Quaternion.identity, 0);
        var gameBoard = PhotonNetwork.Instantiate("GameBoard", new Vector3(), Quaternion.identity, 0);
        player.GetComponent<UserController>().GameBoard = gameBoard;
        gameBoard.GetComponent<GameBoardController>().User = player;
        gameBoard.GetComponent<GameBoardController>().LogOutputText = LogOutputText;
        gameBoard.GetComponent<OilFieldGenerator>().LogOutputText = LogOutputText;

        // Set the player's color
        Log(PhotonNetwork.isMasterClient.ToString());
        player.GetComponent<UserController>().Color = PhotonNetwork.isMasterClient ? Color.blue : Color.red;
        player.GetComponent<UserController>().MyGoldText = MyGoldText;
        player.GetComponent<UserController>().NemesisGoldText = NemesisGoldText;
    }
}
