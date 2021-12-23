using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

[CreateAssetMenu(fileName = "Character", menuName = "CharacterCreation")]
public class CharacterStats : ScriptableObject
{
    public string characterName;
     public int damageAmount;
     public int healthAmount;
     public const float camRotateSpeed = 100f;
     public float speed;
     public AudioClip Hit;
     public AudioClip Stun;


    
}
