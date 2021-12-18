using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class MenuUI : MonoBehaviour
{

    [SerializeField] GameObject _loadingScreen;
    [SerializeField] GameObject _createRoomMenu;
    [SerializeField] GameObject _joinRoomMenu;
    [SerializeField] GameObject _titleScreenMenu;
    [SerializeField] GameObject _currentRoomMenu;
    [SerializeField] GameObject _joinOrCreateMenu;
    [SerializeField] TMP_Text _roomNameText;
    [SerializeField] TMP_Text _tourneyNameText;
    [SerializeField] GameObject _modePickScreen; 
    [SerializeField] GameObject _OAuthLogin;
    [SerializeField] GameObject _tourneyJoinOrCreate;
    [SerializeField] GameObject _tourneyRoom;
    [SerializeField] GameObject _tourneyNamer; 

    private string _currentRoomName;
    private string _currentTourneyName;
    public bool inTourneyMode; 

    private GameObject[] menuList; 
    // Start is called before the first frame update
    void Start()
    {
        menuList = new GameObject[10] { _loadingScreen, _createRoomMenu, _joinRoomMenu, _titleScreenMenu, _currentRoomMenu, _joinOrCreateMenu, _modePickScreen, _tourneyJoinOrCreate, _tourneyRoom, _tourneyNamer };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToScene(GameObject g)
    {
        g.SetActive(true);
        //if(g == )
        SetOtherMenuInactive(g);
    }


    public void SetTourneyStatus(bool b)
    {
        inTourneyMode = b; 
    }
    public void OpenTourneyRoom()
    {
        _tourneyRoom.SetActive(true);
        SetOtherMenuInactive(_tourneyRoom);
    }
    public void SetOtherMenuInactive(GameObject g)
    {
        foreach(GameObject o in menuList)
        {
            if(o != g)
            {
                o.SetActive(false);
            }
        }
    }
    public void OpenCurrentRoomMenu()
    {
        _currentRoomMenu.SetActive(true);
        SetOtherMenuInactive(_currentRoomMenu);
    }

    public void OpenTitleScreenMenu()
    {
        _titleScreenMenu.SetActive(true);
        SetOtherMenuInactive(_titleScreenMenu);
    }

    public void OpenJoinRoomMenu()
    {
        _joinRoomMenu.SetActive(true);
        SetOtherMenuInactive(_joinRoomMenu);
    }

    public void OpenCreateRoomMenu()
    {
        _createRoomMenu.SetActive(true);
        SetOtherMenuInactive(_createRoomMenu);
    }

    public void OpenLoadingScreen()
    {
        _loadingScreen.SetActive(true);
        SetOtherMenuInactive(_loadingScreen);
    }

    public void OpenJoinOrCreateScreen()
    {
        _joinOrCreateMenu.SetActive(true);
        SetOtherMenuInactive(_joinOrCreateMenu);
    }

    public void CreateRoomButton()
    {
        OpenCreateRoomMenu(); 
    }

    public void JoinRoomButton()
    {
        OpenJoinRoomMenu(); 
    }

    public void LoginOAuth()
    {
        _OAuthLogin.SetActive(true);
        SetOtherMenuInactive(_OAuthLogin);
    }

    public void SetRoomName(string name)
    {
        _roomNameText.text = name; 
    }

    public void SetTourneyName(string name)
    {
        _tourneyNameText.text = name; 
    }

}
