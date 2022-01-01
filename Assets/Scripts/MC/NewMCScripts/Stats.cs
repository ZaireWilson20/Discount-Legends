using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

[CreateAssetMenu(fileName = "Character", menuName = "CharacterCreation")]
public class Stats : ScriptableObject
{
     public string characterName;
     public int damageAmount;
     public int healthAmount;
     public float attackCoolDown;
     public float speed;
     public AudioClip Hit;
     public AudioClip Stun;
     public AudioClip Pickup;
    
}
