using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    [SerializeField] int damageAmount; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1.5f);
        transform.Rotate(player.GetComponent<Movement>().yaw*Vector3.up);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Weapon Colliding");
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<RigidbodyMovement>().TakeDamage(damageAmount); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Cart trigger with " + other.name);
    }
}
