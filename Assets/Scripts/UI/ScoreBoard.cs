using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


public class ScoreBoard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    [SerializeField] Dictionary<int, ScoreboardItem> scoreboardItems = new Dictionary<int, ScoreboardItem>();  //Serialize Field is only here for debugging.
    int playerID;
    public UnityEngine.Events.UnityEvent OnScoreUpdated;
    public Challonge.API.Data.ChallongeMatch challongeMatch;


    void Start()
    {


        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
            Debug.Log("Player Nickname: " + player.NickName);

        }

    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreboardItem(newPlayer);

    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboarditem(otherPlayer);
    }



    void AddScoreboardItem(Player player)
    {
        ScoreboardItem item = Instantiate(scoreboardItemPrefab, container).GetComponent<ScoreboardItem>();
        
        item.Initialize(player);
        item.updateScore(0);

        int id = player.ActorNumber;
        playerID = id;
        scoreboardItems[id] = item;


    }

    public float GetPlayerScore()
    {
        return scoreboardItems[playerID].GetComponent<ScoreboardItem>().score;
    }


    void RemoveScoreboarditem(Player player)
    {
        int id = player.ActorNumber;
        Destroy(scoreboardItems[id].gameObject);
        scoreboardItems.Remove(id);
    }

    public void UpdateScoreboardItem(float score, int id)
    {
        scoreboardItems[id].updateScore(score);

    }

    public void SendScoresToChallonge()
    {
        challongeMatch.participantList[0].matchResult.score = (int)GetPlayerScore();
        Debug.Log(challongeMatch.participantList[0].matchResult.score);
        OnScoreUpdated.Invoke();
    }

}
