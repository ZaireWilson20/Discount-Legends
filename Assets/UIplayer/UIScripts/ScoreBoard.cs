using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


public class ScoreBoard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    Dictionary<Player, ScoreboardItem> scoreboardItems = new Dictionary<Player, ScoreboardItem>();

   
    void Start()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
       {
         AddScoreboardItem(player);

        }

    }



    public override void OnPlayerEnteredRoom(Player newPlayer)//when a player enters go to add scoreboar ditem and add player
    {
        AddScoreboardItem(newPlayer);

    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboarditem(otherPlayer);
    }



    void AddScoreboardItem(Player player)//adds players
   {
       ScoreboardItem item= Instantiate (scoreboardItemPrefab, container).GetComponent<ScoreboardItem>();
       item.Initialize(player);
       scoreboardItems[player] =item;


  }



   

    void RemoveScoreboarditem(Player player)//removes players
    {
        Destroy(scoreboardItems[player].gameObject);
        scoreboardItems.Remove(player);
    }

}
