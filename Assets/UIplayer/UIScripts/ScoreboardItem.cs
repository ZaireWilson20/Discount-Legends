using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class ScoreboardItem : MonoBehaviour
{
    public Text usernameText;
    public Text ScoreText;

    public void Initialize(Player player)
    {
       usernameText.text = player.NickName; 
    }

    /*
    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
