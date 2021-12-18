using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingDown : MonoBehaviour
{  [SerializeField] Transform spawnDown;
   public void OnTriggerEnter(Collider col) {
       Transform MAIN =  col.gameObject.transform;
       while(MAIN.gameObject.name != "MC"){
           MAIN = MAIN.parent;
       }
      teleport(MAIN);
     
   }

   void teleport(Transform main){
      main.position = spawnDown.position;
      return;
   }
}
