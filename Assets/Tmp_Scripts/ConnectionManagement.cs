using System.Collections;
using System.Collections.Generic;
using System.IO; 
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Challonge.API.Data;
using UnityEngine.SceneManagement; 
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ConnectionManagement : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_InputField _roomNameInput;
    [SerializeField] MenuUI _menu;
    [SerializeField] Transform _roomListContent;
    [SerializeField] GameObject _roomListPrefab;
    [SerializeField] Transform _playerListContent;
    [SerializeField] Transform _TOURNEYplayerListContent;
    [SerializeField] Transform _REGULARplayerListContent;
    [SerializeField] GameObject _playerListPrefab;
    [SerializeField] GameObject _startGameButton;
    [SerializeField] TMP_InputField _tourneyRoomName;
    [SerializeField] TMP_Text username;
    [SerializeField] GameObject _TOURNEYroomListContent;
    [SerializeField] GameObject _tourneyStartButton;
    Dictionary<string, bool> roomIsTourneyDict; 
    public static ConnectionManagement instance;
    private CharacterSelect characterSelect; 
    public RoomManager roomManager; 
    private bool isTourneyMode;
    public ChallongeUser _user;
    

    private void Awake()
    {
        instance = this;
        roomIsTourneyDict = new Dictionary<string, bool>(); 
    }


    public void StartGame()
    {
        roomManager.SetCharacter();
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        DontDestroyOnLoad(roomManager.gameObject);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = _user.user.username;


    }


    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        //foreach(byte b in propertiesThatChanged.Keys)
        //{
            //roomIsTourneyDict[b.ToString()] = (bool)propertiesThatChanged[b];
        //}
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
    }

    public void ConnectToPhoton()
    {
        if (_user.user.accessToken == "")
        {
            //_menu.ShowLoginNecessary();
            //return; 
        }
        PhotonNetwork.ConnectUsingSettings();
        // _menu.OpenJoinOrCreateScreen(); 
    }

    public void CreateRoom()
    {

        if (string.IsNullOrEmpty(_roomNameInput.text)) { return; }
            _menu.SetRoomName(_roomNameInput.text);

        RoomOptions roomOptions = new RoomOptions();
        Hashtable tourneySetup = new Hashtable();
        tourneySetup.Add("isTourney", false);
        roomOptions.CustomRoomProperties = tourneySetup;
        //Hashtable roomCustomProperties = new Hashtable(); 
        //roomCustomProperties.Add("Scoreboard", )
        PhotonNetwork.CreateRoom(_roomNameInput.text);
        _menu.OpenLoadingScreen();
    }

    public void SetNickName()
    {
        if (username.text != "")
        {
            PhotonNetwork.NickName = username.text;
        }
        else
        {
            PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString();

        }

    }

    public void CreateTourneyRoom()
    {

        if (string.IsNullOrEmpty(_tourneyRoomName.text)) { return; }
        _menu.SetTourneyName(_tourneyRoomName.text);

        RoomOptions roomOptions = new RoomOptions();
        Hashtable tourneySetup = new Hashtable();
        tourneySetup.Add("isTourney", true);
       // Room r = new Room();
          roomOptions.CustomRoomProperties = tourneySetup; 
        //Hashtable roomCustomProperties = new Hashtable(); 
        //roomCustomProperties.Add("Scoreboard", )
        PhotonNetwork.CreateRoom(_tourneyRoomName.text);
        _menu.OpenLoadingScreen();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("player joined from connection management");
        if (username.text != "")
        {
            PhotonNetwork.NickName = username.text;

        }
        if (!_menu.inTourneyMode)
        {
            _menu.OpenCurrentRoomMenu();
            _playerListContent = _REGULARplayerListContent;
            _startGameButton.SetActive(PhotonNetwork.IsMasterClient);
            _menu.SetRoomName(PhotonNetwork.CurrentRoom.Name);


        }
        else
        {
            _menu.OpenTourneyRoom();
            _playerListContent = _TOURNEYplayerListContent;
            _tourneyStartButton.SetActive(PhotonNetwork.IsMasterClient);
            _menu.SetTourneyName(PhotonNetwork.CurrentRoom.Name);


        }
        //  PhotonNetwork.SetPlayerCustomProperties
        //foreach (Player p in PhotonNetwork.PlayerList)
        //{
        GameObject g = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerListObj"),Vector3.zero, Quaternion.identity);
        g.GetComponent<PlayerListObject>().SetUp(PhotonNetwork.LocalPlayer);

        /*
        foreach (GameObject t in _playerListContent)
        {
            if (t.gameObject.GetComponent<PhotonView>().IsMine == false)
            {
                t.gameObject.GetComponent<PlayerListObject>().leftButton.gameObject.SetActive(false);
                t.gameObject.GetComponent<PlayerListObject>().rightButton.gameObject.SetActive(false);

            }
        }*/

        for (int i = 0; i < _playerListContent.transform.childCount; i++)
        {
            Transform Children = _playerListContent.transform.GetChild(i);
            if (Children.GetComponent<PhotonView>().IsMine == false)
            {
                Children.gameObject.GetComponent<PlayerListObject>().leftButton.gameObject.SetActive(false);
                Children.gameObject.GetComponent<PlayerListObject>().rightButton.gameObject.SetActive(false);
            }
        }
        //Instantiate(_playerListPrefab, _playerListContent).GetComponent<PlayerListObject>().SetUp(p);
        //}

        //characterSelect.OnJoinedRoom(); 
    }

    public void SetTourneyModeStatus(bool isT)
    {
        isTourneyMode = isT; 
    }

    public bool IsTourneyMode()
    {
        return isTourneyMode;
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
            //r.
            if (r.RemovedFromList){ continue; }
            //if (!roomIsTourneyDict[r.Name])
           // {
                Instantiate(_roomListPrefab, _roomListContent).GetComponent<RoomListObject>().SetUp(r);
            //}
            //else
            //{
                //Instantiate(_roomListPrefab, _TOURNEYplayerListContent).GetComponent<RoomListObject>().SetUp(r);
            //}
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        Debug.Log("bout to delete them arrows");
        for (int i = 0; i < _playerListContent.transform.childCount; i++)
        {
            Transform Children = _playerListContent.transform.GetChild(i);
            Debug.Log("child " + i + " is mine: " + Children.GetComponent<PhotonView>().IsMine);
            if (Children.GetComponent<PhotonView>().IsMine == false)
            {
                Children.gameObject.GetComponent<PlayerListObject>().leftButton.gameObject.SetActive(false);
                Children.gameObject.GetComponent<PlayerListObject>().rightButton.gameObject.SetActive(false);
            }
        }

        /*
        foreach (GameObject t in _playerListContent)
        {
            if (t.gameObject.GetComponent<PhotonView>().IsMine == false)
            {
                Debug.Log("is mineee");
                t.gameObject.GetComponent<PlayerListObject>().leftButton.gameObject.SetActive(false);
                t.gameObject.GetComponent<PlayerListObject>().rightButton.gameObject.SetActive(false);

            }
        }*/
        //Instantiate(_playerListPrefab, _playerListContent).GetComponent<PlayerListObject>().SetUp(newPlayer);
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

    public void LoadTourneyOAuth()
    {
        PhotonNetwork.LoadLevel("Challonge OAuth");

    }


    public void UpdateLocalUsername(TMPro.TMP_Text nameDisplay)
    {
        username.text = nameDisplay.text; 
    }


}
