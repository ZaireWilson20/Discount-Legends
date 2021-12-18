using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class CharacterSelect : MonoBehaviour
{
    [SerializeField] Sprite[] characterSprites;
    int currentIndex = 0 ;
    Dictionary<Sprite, string> spriteToPrefab;
    [SerializeField] Image characterImage; 

    [SerializeField] Sprite[] weaponSprites;
    int currentWeaponIndex = 0 ;
    Dictionary<Sprite, string> spriteToPrefabWeapon;
    [SerializeField] Image weaponImage; 
    // Start is called before the first frame update
    void Start()
    {
        spriteToPrefab = new Dictionary<Sprite, string>();
        spriteToPrefabWeapon = new Dictionary<Sprite, string>(); 
        spriteToPrefab.Add(characterSprites[0], "Female");   
        spriteToPrefab.Add(characterSprites[1], "Female");   
        spriteToPrefabWeapon.Add(weaponSprites[0], "Cart");   
        spriteToPrefabWeapon.Add(weaponSprites[1], "Backpack");

        characterImage.sprite = characterSprites[0];
        weaponImage.sprite = weaponSprites[0];
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
}
