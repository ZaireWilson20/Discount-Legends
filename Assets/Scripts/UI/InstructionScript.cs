using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionScript : MonoBehaviour
{
   public GameObject Menu;

   public void turnoffMenu(){
       Menu.SetActive(false);
   }
   public void turnOnMenu(){
       Menu.SetActive(true);
   }
   
   }
