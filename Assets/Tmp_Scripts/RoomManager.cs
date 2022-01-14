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
    CharacterSelect characterSelect; 
    [SerializeField] CharacterSelect tourneySelect; 
    [SerializeField] CharacterSelect regularSelect;
    public GameObject gmananger; 
    public string character; 
    
    PhotonView _pv;

    public TMP_Text itemDebug; 
    private void Awake()
    {
        _pv = GetComponent<PhotonView>(); 
        if (instance)
        {
            Destroy(gameObject);
        }
        gmananger = GameObject.Instantiate(gmananger);
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

    public string SetCharacter()
    {
        return characterSelect.CurrentSelectToSpawn();
    }
    

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (ConnectionManagement.instance.IsTourneyMode()) {
            characterSelect = tourneySelect; 
        }
        else
        {
            characterSelect = regularSelect; 
        }
        if(scene.buildIndex == 1) // Game Scene
        {
            gmananger.GetComponent<GManager>().playType = SetCharacter();

            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManagement"), Vector3.zero, Quaternion.identity);
        }
    }


}
