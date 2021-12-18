using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class ScoreManager : MonoBehaviourPunCallbacks
{
    
    GameObject[] items;
    PhotonView PV;
    bool itemsChecked = false;
    bool needToCheckItems = false;
    //DEBUG
    TextMeshProUGUI itemList;
    //Dictionary<string, int> playerScores
    
    void Awake() {
    
        PV = GetComponent<PhotonView>();
        // itemList = GameObject.Find("ALL ITEMS").GetComponent<TextMeshProUGUI>();//DEBUG

        if (items == null)
        {
            Debug.Log("NO ITEMS ON SCREEN");
            return;
        }


    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

        //Eventually need to figure out a way to stop this from happening whenever player properties are changed
        if (!itemsChecked && needToCheckItems)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            foreach (GameObject item in items)
            {
                Debug.Log(changedProps[item.name]); //DEBUG

                item.GetComponent<Item>().setPoints((int)changedProps[item.name]);
            }


            foreach (GameObject item in items)
            {
                Debug.Log(item.name + ": " + item.GetComponent<Item>().getPoints() + "\n"); //DEBUG
            }
            itemsChecked = true;
        }

        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }


    void Start(){

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        ExitGames.Client.Photon.Hashtable _myCustomScores = new ExitGames.Client.Photon.Hashtable();


        //Debug.Log("on scene loaded");
        //PV.RPC("SetItemPoint", RpcTarget.MasterClient);


        if (PhotonNetwork.IsMasterClient)
        {
            foreach (GameObject item in items)
            {

                Debug.Log(item.name);
                string name = item.name;
                _myCustomScores.Add(item.name, item.GetComponent<Item>().getPoints());
                //_myCustomScores[name] = 1;//item.GetComponent<Item>().getPoints();
                //PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomScores);
            }
            needToCheckItems = true;
            PhotonNetwork.SetPlayerCustomProperties(_myCustomScores);
        }



    }

    [PunRPC]
    public void SetItemPoint()
    {


    }
}
