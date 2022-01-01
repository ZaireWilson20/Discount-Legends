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
    TextMeshProUGUI itemList; //Used to print a list of items for DEBUGGING

    //Dictionary<string, int> playerScores;
    
    void Awake() {
    
        //PV = GetComponent<PhotonView>();
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
            Debug.Log("item check");
            foreach (GameObject item in items)
            {
                //Debug.Log(item.name); //DEBUG
                if(changedProps[item.name] == null) { continue; }

                item.GetComponent<Item>().setPoints((int)changedProps[item.name]);
            }


            foreach (GameObject item in items)
            {
                //Debug.Log(item.name + ": " + item.GetComponent<Item>().getPoints() + "\n"); //DEBUG
            }
            itemsChecked = true;
            needToCheckItems = false;
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
            Debug.Log(gameObject.name);

            foreach (GameObject item in items)
            {

               // Debug.Log(item.name);
                string name = item.name;
                _myCustomScores.Add(item.name, item.GetComponent<Item>().getPoints());
                //_myCustomScores[name] = 1;//item.GetComponent<Item>().getPoints();
                //PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomScores);
            }
            Debug.Log("needtocheck: " + needToCheckItems);
            needToCheckItems = true;
            PhotonNetwork.SetPlayerCustomProperties(_myCustomScores);
        }



    }

}
