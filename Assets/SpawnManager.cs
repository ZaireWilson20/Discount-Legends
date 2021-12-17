using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SpawnManager : MonoBehaviourPunCallbacks
{

    public static SpawnManager instance;
    SpawnPoints[] _spawnPoints; //Make Sure these are children of this gameobject
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        instance = this;
        _spawnPoints = GetComponentsInChildren<SpawnPoints>(); 
    }

    public Transform GetSpawnPoint(int actor)
    {
        if (actor > _spawnPoints.Length-1){
            actor %= _spawnPoints.Length-1;
        } 
        if (actor < 0) {
            actor = 0;
        }
        return _spawnPoints[actor].transform; 
    }
}
