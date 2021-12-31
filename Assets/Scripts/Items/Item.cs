using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class Item : MonoBehaviour
{
    public float basePrice;
    public float discountPrice;
    private float score; //variable for discount percentage
    [SerializeField] private TextMeshProUGUI priceTag;

    void Awake(){
       
        // discountPrice = Random.Range(1,10) * 100;
        // while(basePrice < discountPrice){ // make sure you get a baseprice higher than discountprice
        //     basePrice = Random.Range(1,10) * 300;
        // }

        float percent = (float)Math.Floor(((basePrice-discountPrice)/basePrice) * 100);
        score =  percent * 100;
        if(priceTag == null){
            Debug.LogError("No pricetag UI element", priceTag);
            return;
        }
        priceTag.text = percent + "% Off!";  
    }

    public float getPoints() {
        return score;
    }

    public void setPoints(float points){
        this.score = points;
    }

    public GameObject getText() {
        return priceTag.gameObject;
    }
  
}
