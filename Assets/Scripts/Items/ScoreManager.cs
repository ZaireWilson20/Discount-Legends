using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    GameObject[] items;
    PhotonView PV;
    //DEBUG
    TextMeshProUGUI itemList;
    private ExitGames.Client.Photon.Hashtable  _myCustomScores = new ExitGames.Client.Photon.Hashtable();
    void Awake() {
    
        PV = GetComponent<PhotonView>();
        items = GameObject.FindGameObjectsWithTag("Item");
        itemList = GameObject.Find("ALL ITEMS").GetComponent<TextMeshProUGUI>();//DEBUG

        if (items == null)
        {
            Debug.Log("NO ITEMS ON SCREEN");
            return;
        }
        foreach (GameObject item in items){
            itemList.text += item.name + ": " + item.GetComponent<Item>().getPoints() + "\n"; //DEBUG
        }

    if( PhotonNetwork.IsMasterClient){
            foreach (GameObject item in items){
             string name = item.name;
            _myCustomScores[name] = item.GetComponent<Item>().getPoints();
            PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomScores);
            }
    
    } 
    }

    void Start(){
     
        foreach (GameObject item in items){ 
            Debug.Log("Item Score : " +  (int)_myCustomScores[item.name]); //DEBUG

            item.GetComponent<Item>().setPoints((int)_myCustomScores[item.name]);
        }
    }
}
