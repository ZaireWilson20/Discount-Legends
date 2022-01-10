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
    bool needToCheckItems = true;
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

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("Room property update, is master client: " + PhotonNetwork.IsMasterClient);
        //Eventually need to figure out a way to stop this from happening whenever player properties are changed
        if (needToCheckItems && !PhotonNetwork.IsMasterClient)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            Debug.Log("item check");
            foreach (GameObject item in items)
            {
                Debug.Log(item.name); //DEBUG
                //if(changedProps[item.name + "Base"] == null ) { continue; }
                ItemData d = (ItemData)changedProps[item.name];
                item.GetComponent<Item>().SetUp((float)changedProps[item.name + "Base"], (float)changedProps[item.name + "Discount"]);
            }

            /*
            foreach (GameObject item in items)
            {
                //Debug.Log(item.name + ": " + item.GetComponent<Item>().getPoints() + "\n"); //DEBUG
            }*/
            //itemsChecked = true;
            needToCheckItems = false;
        }

        base.OnRoomPropertiesUpdate(changedProps);
    }


    void Start(){



        //Debug.Log("on scene loaded");
        //PV.RPC("SetItemPoint", RpcTarget.MasterClient);


        if (PhotonNetwork.IsMasterClient)
        {

            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            ExitGames.Client.Photon.Hashtable _myCustomScores = new ExitGames.Client.Photon.Hashtable();
            Debug.Log(gameObject.name);

            foreach (GameObject item in items)
            {

               // Debug.Log(item.name);
                string name = item.name;
                float itemDPrice = UnityEngine.Random.Range(1, 10) * 100;
                float itemBPrice = UnityEngine.Random.Range(3, 10) * 300;
                ItemData itemData = new ItemData();
                itemData.basePrice = itemBPrice;
                itemData.discountPrice = itemDPrice;
                item.GetComponent<Item>().SetUp(itemBPrice, itemDPrice);
                _myCustomScores.Add(item.name + "Discount", itemDPrice);
                _myCustomScores.Add(item.name + "Base", itemBPrice);
                //_myCustomScores[name] = 1;//item.GetComponent<Item>().getPoints();
                //PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomScores);
            }
            Debug.Log("needtocheck: " + needToCheckItems);
            needToCheckItems = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(_myCustomScores);
        }



    }

    private void SetRandomItemValues()
    {

    }

    
    public class ItemData
    {
        public float discountPrice;
        public float basePrice; 
    }

}

