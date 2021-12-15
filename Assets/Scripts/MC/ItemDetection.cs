using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Item")
        {
            Item points = other.gameObject.GetComponent<Item>();
            if (points == null)
            {
                return;
            }
            else
            {
                Destroy(other.gameObject);
            }

        }
    }
}
