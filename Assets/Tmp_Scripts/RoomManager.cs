using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using Photon.Pun;
using UnityEngine.SceneManagement; 

public class RoomManager : MonoBehaviourPunCallbacks
{
    [HideInInspector]public RoomManager instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnEnable()
    {
        base.OnEnable(); 
    }

    public override void OnDisable()
    {
        base.OnDisable(); 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1) // Game Scene
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManagement"), Vector3.zero, Quaternion.identity);
        }
    }
}
