using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Challonge.API.Data;

public class LoginHandler : MonoBehaviour
{

    [SerializeField] ChallongeUser _user;
    [SerializeField] GameObject _loginButton; 
    // Start is called before the first frame update
    void Start()
    {
        if(_user.user.accessToken != "")
        {
            _loginButton.SetActive(false); 
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
