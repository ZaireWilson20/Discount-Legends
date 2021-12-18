using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingUp : MonoBehaviour
{
    [SerializeField] Transform spawnUp;
   public void OnTriggerEnter(Collider col) {
       Transform MAIN =  col.gameObject.transform;
       while(MAIN.gameObject.name != "MC"){
           if(MAIN.parent == null){
               return;
           }
           MAIN = MAIN.parent;
       }
      teleport(MAIN);
     
   }
    void teleport(Transform main){
      main.position = spawnUp.position;
      return;
   }
}
