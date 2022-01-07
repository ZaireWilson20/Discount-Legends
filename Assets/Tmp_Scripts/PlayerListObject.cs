using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;
using Newtonsoft.Json;

public class PlayerListObject : MonoBehaviourPunCallbacks
{
    [SerializeField] public TMP_Text _playerName;
    [SerializeField] public Button leftButton;
    [SerializeField] public Button rightButton;
    CharacterSelect charS; 
    public GameObject parentObject; 
    Player _player;


    private void Awake()
    {
        charS = GameObject.FindGameObjectWithTag("CharacterSelector").GetComponent<CharacterSelect>();
        Transform playerList = GameObject.FindGameObjectWithTag("PlayerList").transform;

        transform.SetParent(playerList);
        transform.localScale = new Vector3(1, 1, 1);
        leftButton.onClick.AddListener(charS.GoLeft);
        rightButton.onClick.AddListener(charS.GoRight);

    }

    public void SetUp(Player player)
    {
        _player = player;
        _playerName.text = player.NickName;
        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(_player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }


}
