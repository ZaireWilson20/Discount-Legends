using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class MenuUI : MonoBehaviour
{

    [SerializeField] GameObject _creditsMenu;
    [SerializeField] GameObject _creditsButton;
    [SerializeField] GameObject _loginButton;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] GameObject _createRoomMenu;
    [SerializeField] GameObject _joinRoomMenu;
    [SerializeField] GameObject _titleScreenMenu;
    [SerializeField] GameObject _currentRoomMenu;
    [SerializeField] GameObject _joinOrCreateMenu;
    [SerializeField] GameObject _levelSelectMenu; 
    [SerializeField] TMP_Text _roomNameText;
    [SerializeField] TMP_Text _tourneyNameText;
    [SerializeField] GameObject _modePickScreen; 
    [SerializeField] GameObject _OAuthLogin;
    [SerializeField] GameObject _tourneyJoinOrCreate;
    [SerializeField] GameObject _tourneyRoom;
    [SerializeField] GameObject _tourneyNamer;
    [SerializeField] GameObject _loginNeeded;
    [SerializeField] CharacterSelect characterSelect; 

    private string _currentRoomName;
    private string _currentTourneyName;
    public bool inTourneyMode; 

    private GameObject[] menuList; 
    // Start is called before the first frame update
    void Start()
    {
        menuList = new GameObject[14] { _loadingScreen, _createRoomMenu, _joinRoomMenu, _titleScreenMenu, _currentRoomMenu, _joinOrCreateMenu, _modePickScreen, _tourneyJoinOrCreate, _tourneyRoom, _tourneyNamer, _creditsMenu, _creditsButton, _loginButton,_levelSelectMenu };
    }

    public void ShowLoginNecessary()
    {
        MustLoginCoroutine(); 
    }

    private IEnumerator MustLoginCoroutine()
    {
        _loginNeeded.SetActive(true); 
        yield return new WaitForSeconds(5f);
        _loginNeeded.SetActive(false);
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

    public void OpenCreditsMenu(){
        _creditsMenu.SetActive(true);
        SetOtherMenuInactive(_creditsMenu);
    }

    public void CloseCreditsMenu(){
        _creditsMenu.SetActive(false);
        _loginButton.SetActive(true);
        _creditsButton.SetActive(true);
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

    public void OpenLevelSelect()
    {
        _levelSelectMenu.SetActive(true);
        foreach(GameObject g in characterSelect.allActiveCharacters)
        {
            g.SetActive(false); 
        }
        SetOtherMenuInactive(_levelSelectMenu);
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
