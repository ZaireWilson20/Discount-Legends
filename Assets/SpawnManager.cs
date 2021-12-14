using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;

    SpawnPoints[] _spawnPoints; //Make Sure these are children of this gameobject

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
        instance = this;
        _spawnPoints = GetComponentsInChildren<SpawnPoints>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetSpawnPoint()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform; 
    }
}
