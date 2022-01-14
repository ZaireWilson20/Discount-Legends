using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Challonge.API.Data;

public class LoginHandler : MonoBehaviour
{

    [SerializeField] ChallongeUser _user;
    [SerializeField] GameObject _loginButton;
    [SerializeField] GameObject _startButton; 
    [SerializeField] GameObject _creditsButton;
    // Start is called before the first frame update
    void Start()
    {
        if(_user.user.accessToken != "")
        {
            _loginButton.SetActive(false);
            _creditsButton.SetActive(true);
            _startButton.SetActive(true); 
        }
        else
        {
            _loginButton.SetActive(true);
            _startButton.SetActive(false);
            _creditsButton.SetActive(false);
        }


    }
}
