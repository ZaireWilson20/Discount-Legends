using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.RealTime;
//using Photon.Pun;


public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //foreach (Player player in PhotonNetwork.PlayerList)
       // {
        //    AddScoreboardItem(player);

       // }




    }



   // void AddScoreboardItem(Player player)
   // {
       // ScoreBoard item = Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreboardItem>();
       // item.Initialize(player);
       //ScoreboardItem[player] =item;


  //  }


/*
    public override void OnPlayerEnteredRoom(PlayerPrefs newPlayer)
    {
        AddScoreboardItem(newPlayer);

    }

    public override void OnPlayerleftRoom(Player otherPlayer)
    {


    }
    */
    /*
    void RemoveScoreboarditem(Player player)
    {
        Destroy(ScoreboardItem[player].gameObjects)
        ScoreboardItems.Remove(player);
    }

    */
}
