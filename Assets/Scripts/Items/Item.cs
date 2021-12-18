using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int basePrice;
    private  int discountPrice;
    public int score;
    //variable for discount percentage
    
   

    // void Awake(){
       
    //     discountPrice = Random.Range(1,10) * 100;
    //     while(basePrice < discountPrice){ // make sure you get a baseprice higher than discountprice
    //         basePrice = Random.Range(1,10) * 300;
    //     }
    //     score = basePrice-discountPrice;
    // }

    public int getPoints() {
        return score;
    }

    public void setPoints(int points){
        this.score = points;
    }

  
}
