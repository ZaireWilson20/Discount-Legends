using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class Item : MonoBehaviour
{
    [Header("Discount")]
    public float basePrice;
    public float discountPrice; //TODO: Editor script that hides this variable depending if isDiscount or not
    private float score;
    [SerializeField] private TextMeshProUGUI priceTag;

    [Header("Item Type")]
    public bool isDiscount; // In the future, have a base class and subclasses for different types of items.

    void Awake()
    {

        // UNCOMMENT WHEN RANDOMIZATION IS IMPLEMENTED

        // discountPrice = Random.Range(1,10) * 100;
        // while(basePrice < discountPrice){ // make sure you get a baseprice higher than discountprice
        //     basePrice = Random.Range(1,10) * 300;
        // }


        if (isDiscount)
        {
            float percent = (float)Math.Floor(((basePrice - discountPrice) / basePrice) * 100);
            score = percent * 10;
            if (priceTag == null)
            {
                Debug.LogError("No pricetag UI element", gameObject);
                return;
            }
            if (discountPrice == 0) {
                Debug.LogError("DiscountPrice is set to 0. Please change this.", gameObject);
            }

            priceTag.text = percent + "% OFF!";

        }
        else
        {
            score = ((basePrice) * 10f) % 100f;
            if (score == 0)
            {
                score = 100f; // Lucky
            }

            if (priceTag == null)
            {
                Debug.LogError("No pricetag UI element", priceTag);
                return;
            }

            priceTag.text = ""; // In case text isn't empty

            // Guarantees the score is less than a discounted item due to Modulo rules unless rare chance of 0
            // Multiplied by 10 to avoid getting points that are in the 1-10
        }
    }

    public float getPoints()
    {
        return score;
    }

    public void setPoints(float points)
    {
        this.score = points;
    }

    public GameObject getText()
    {
        return priceTag.gameObject;
    }

}
