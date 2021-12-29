using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBounds : MonoBehaviour
{
    [SerializeField] private Transform respawn;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"){
            if(respawn == null){
                Debug.LogError("Respawn transform is missing");
            }
            other.gameObject.transform.position = respawn.position;
        }
    }
}
