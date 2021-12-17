using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class PlayerRecord : MonoBehaviourPunCallbacks
{
    private static PlayerRecord instance;

    public bool EndScene;
    private TextMeshProUGUI[] playerName, scores;
    [SerializeField] Dictionary<string, int> players = new Dictionary<string, int>();
    void Awake()
    {

         SceneManager.activeSceneChanged += DisplayScores;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            AddRecord(player);

        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)//when a player enters go to add scoreboar ditem and add player
    {
        AddRecord(newPlayer);

    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveRecord(otherPlayer);
    }


    void AddRecord(Player player)
    {
        players[player.NickName] = 0;
        Debug.Log("ADDED " + player.NickName + " SCORE " + players[player.NickName] );
    }

    public void UpdateRecord(int score, string name)
    {
        players[name] = score;
        Debug.Log("ADDED " + name+ " SCORE " + players[name] );
    }

    void RemoveRecord(Player player)
    {
        players.Remove(player.NickName);
    }

    void DisplayScores() {
        Debug.Log(players.Values);
    }

    private void DisplayScores(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            int sceneNum = SceneManager.GetActiveScene().buildIndex;
            if(sceneNum == 0) {
                Destroy(gameObject);
            }
            if(sceneNum == 2){
             playerName = GameObject.Find("Player").GetComponentsInChildren<TextMeshProUGUI>();
             scores = GameObject.Find("Scores").GetComponentsInChildren<TextMeshProUGUI>();

             for(int i = 0 ; i < scores.Length ; i++) {
                  KeyValuePair<string,int> record = getMax();
                  playerName[i].text = record.Key;
                  scores[i].text ="Points:" + record.Value + "";
                  players.Remove(record.Key);
             }
            } 

           
        }

    }

    public KeyValuePair<string,int> getMax(){

        if(players.Count == 0) {
            return new KeyValuePair<string,int>("EMPTY", 0);
        }
       KeyValuePair<string,int> topScore =  new KeyValuePair<string,int>("EMPTY", 0);
       int max = 0;
        foreach(KeyValuePair<string,int> kvp in players){
            if (kvp.Value > max){
               topScore = kvp;
            }
        }
        return topScore;
    }

}
