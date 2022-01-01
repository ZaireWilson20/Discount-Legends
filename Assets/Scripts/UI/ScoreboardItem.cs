using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class ScoreboardItem : MonoBehaviour
{
    public Text usernameText;
    public Text ScoreText;
    public float score = 0; 

    public void Initialize(Player player)
    {
       usernameText.text = player.NickName; 
    }

    public void updateScore(float points) {
        ScoreText.text = "" + points;
        score += points; 
    }

}
