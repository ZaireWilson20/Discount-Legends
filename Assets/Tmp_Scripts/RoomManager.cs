using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement; 

public class RoomManager : MonoBehaviourPunCallbacks
{
    [HideInInspector]public RoomManager instance;
    PhotonView _pv;

    public TMP_Text itemDebug; 
    private void Awake()
    {
        _pv = GetComponent<PhotonView>(); 
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
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1) // Game Scene
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManagement"), Vector3.zero, Quaternion.identity);
        }

    }


}
