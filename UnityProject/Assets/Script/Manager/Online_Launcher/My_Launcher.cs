using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class My_Launcher : MonoBehaviourPunCallbacks
{
    #region initialisation 
    public Button btn;
    public Text feedbackText;
    private byte maxPlayersPerRoom = 2;
    bool isConnecting;
    string gameVersion = "Alpha";
    
    #endregion
    // for adding animation to load in game,add a LoaderAnime
    
    // Start is called before the first frame update
    
    void Awake() // to synchronize the player in the actual scene in game 
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    public void Connect()
    {
        // clear the feedback text
        feedbackText.text = "";

        // trying to connect
        isConnecting = true;

        // the button isn't usable now
        btn.interactable = false;

        // trying to connect, if not we get connect to the serv, if so we try to join a Room 
        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("Joining Room...");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            LogFeedback("Connecting...");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.gameVersion;
        }
    }
    
    void LogFeedback(string message)
    {
        // case feeback text isn't initialise
        if (feedbackText == null) 
        {
            return;
        }

        // add our text in the log
        feedbackText.text += System.Environment.NewLine+message;
    }
    
    public override void OnConnectedToMaster()
    {
        // if not trying to connect, do nothing
        if (isConnecting)
        {
            // Log info added
            LogFeedback("OnConnectedToMaster: Next -> try to Join Random Room");
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");
		    // CallBack to Join a Room
            PhotonNetwork.JoinRandomRoom();
        }
    }
    
    public override void OnJoinedRoom()
    {
        LogFeedback("<Color=Green>OnJoinedRoom</Color> with "+PhotonNetwork.CurrentRoom.PlayerCount+" Player(s)");
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.\nFrom here on, your game would be running.");
		
        // Load a game when we are a host, if not Awake will be used to get connect
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1");

            PhotonNetwork.LoadLevel("Main_Online");
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        LogFeedback("<Color=Red>OnJoinRandomFailed</Color>: Next -> Create a new Room");
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // Join fail, we create a room
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom});
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        LogFeedback("<Color=Red>OnDisconnected</Color> "+cause);
        Debug.LogError("PUN Basics Tutorial/Launcher:Disconnected");

        isConnecting = false;
        btn.interactable = true;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
