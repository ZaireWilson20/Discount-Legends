using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable; 

public class ConnectionManagement : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_InputField _roomNameInput;
    [SerializeField] MenuUI _menu;
    [SerializeField] Transform _roomListContent;
    [SerializeField] GameObject _roomListPrefab;     
    [SerializeField] Transform _playerListContent;
    [SerializeField] GameObject _playerListPrefab;
    [SerializeField] GameObject _startGameButton; 

    public static ConnectionManagement instance;

    private void Awake()
    {
        instance = this; 
    }


    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        

    }


    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
        _menu.OpenJoinOrCreateScreen(); 
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomNameInput.text)) { return;  }

        _menu.SetRoomName(_roomNameInput.text);
        //Hashtable roomCustomProperties = new Hashtable(); 
        //roomCustomProperties.Add("Scoreboard", )
        PhotonNetwork.CreateRoom(_roomNameInput.text);
        _menu.OpenLoadingScreen(); 
    }

    public override void OnJoinedRoom()
    {
        _menu.OpenCurrentRoomMenu();
        _menu.SetRoomName(PhotonNetwork.CurrentRoom.Name);
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            Instantiate(_playerListPrefab, _playerListContent).GetComponent<PlayerListObject>().SetUp(p);

        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        _menu.OpenLoadingScreen(); 
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        foreach(Transform t in _roomListContent)
        {
            Destroy(t.gameObject);
        }

        foreach(RoomInfo r in roomList)
        {
            if (r.RemovedFromList){ continue; }
            Instantiate(_roomListPrefab, _roomListContent).GetComponent<RoomListObject>().SetUp(r);
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(_playerListPrefab, _playerListContent).GetComponent<PlayerListObject>().SetUp(newPlayer);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        //PhotonNetwork.Room
        _menu.OpenLoadingScreen(); 
    }

    public override void OnLeftRoom()
    {
        foreach(Transform t in _playerListContent)
        {
            Destroy(t.gameObject);
        }
        _menu.OpenJoinOrCreateScreen(); 
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    

    
}
