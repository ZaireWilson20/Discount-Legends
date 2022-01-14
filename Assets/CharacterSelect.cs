using System.Collections;
using System.Collections.Generic;
using System.IO; 
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; 
public class CharacterSelect : MonoBehaviourPunCallbacks
{

    public Transform[] characterSelectSpawn;
    [SerializeField] private int currentSpawnIndex = 0; 
    [SerializeField] Sprite[] characterSprites;
    [SerializeField] string[] characterChoices;

    int currentCharacterChoice = 0; 
    int currentIndex = 0 ;
    Dictionary<Sprite, string> spriteToPrefab;
    [SerializeField] Image characterImage;
    public GameObject activeCharacter;

    private Dictionary<string, string> selectToPlayable; 
    [SerializeField] Sprite[] weaponSprites;
    [SerializeField] public List<GameObject> allActiveCharacters; 
    int currentWeaponIndex = 0 ;
    Dictionary<Sprite, string> spriteToPrefabWeapon;
    [SerializeField] Image weaponImage; 
    // Start is called before the first frame update
    void Start()
    {
        spriteToPrefab = new Dictionary<Sprite, string>();
        spriteToPrefabWeapon = new Dictionary<Sprite, string>(); 
        spriteToPrefab.Add(characterSprites[0], "Fem");   
        spriteToPrefab.Add(characterSprites[1], "Fem");   
        spriteToPrefabWeapon.Add(weaponSprites[0], "Cart");   
        spriteToPrefabWeapon.Add(weaponSprites[1], "Back");

        selectToPlayable = new Dictionary<string, string>(); 

        selectToPlayable.Add("SCharacterMcgee", "ProfFox Cart");
        selectToPlayable.Add("SCharacterFootball", "Football Cart");
        selectToPlayable.Add("SCharacterNeo", "Neo Cart");
        selectToPlayable.Add("SCharacterBee", "Beekeeper Cart");
        selectToPlayable.Add("SCharacterBoxer", "Boxer Cart"); 
        selectToPlayable.Add("SCharacterBabyMech", "BabyMech Cart");



        characterImage.sprite = characterSprites[0];
        weaponImage.sprite = weaponSprites[0];
    }

    public string CurrentSelectToSpawn()
    {
        return selectToPlayable[characterChoices[currentCharacterChoice]];
    }
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WeaponForward()
    {
        if(currentWeaponIndex < weaponSprites.Length - 1)
        {
            currentWeaponIndex++;
            weaponImage.sprite = weaponSprites[currentWeaponIndex];
        }
    }

    public void WeaponBackward()
    {
        if (currentWeaponIndex > 0)
        {
            currentWeaponIndex--;
            weaponImage.sprite = weaponSprites[currentWeaponIndex];
        }
    }

    public void CharacterForward()
    {
        if (currentIndex < characterSprites.Length - 1)
        {
            currentIndex++;
            characterImage.sprite = characterSprites[currentIndex];
        }
    }

    public void CharacterBackward()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            characterImage.sprite = characterSprites[currentIndex];
        }
    }

    public string GetCharcterPrefab()
    {
        return spriteToPrefab[characterSprites[currentIndex]]; 
    }

    public string GetWeaponPrefab()
    {
        return spriteToPrefabWeapon[weaponSprites[currentWeaponIndex]];

    }

    public override void OnJoinedRoom()
    {
        currentSpawnIndex = PhotonNetwork.PlayerList.Length - 1; 
        activeCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SelectableCharacters", "SCharacterMcgee"), characterSelectSpawn[currentSpawnIndex].position, characterSelectSpawn[currentSpawnIndex].rotation);

        //base.OnJoinedRoom();
    }
    
    public void CharacterSelectOnRoomJoin()
    {

    }


    public void GoRight()
    {
        Debug.Log("Go Right");
        if (currentCharacterChoice < characterChoices.Length - 1)
        {
            currentCharacterChoice++;
            PhotonNetwork.Destroy(activeCharacter);
            activeCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SelectableCharacters", characterChoices[currentCharacterChoice]), characterSelectSpawn[currentSpawnIndex].position, characterSelectSpawn[currentSpawnIndex].rotation);
        }
    }



    public void GoLeft()
    {
        Debug.Log("Go Left");

        if (currentCharacterChoice > 0)
        {
            currentCharacterChoice--;
            PhotonNetwork.Destroy(activeCharacter);
            activeCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SelectableCharacters", characterChoices[currentCharacterChoice]), characterSelectSpawn[currentSpawnIndex].position, characterSelectSpawn[currentSpawnIndex].rotation);
            allActiveCharacters.Add(activeCharacter);
        }
    }
}
