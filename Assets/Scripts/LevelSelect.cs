using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class LevelSelect : MonoBehaviour
{

    public string[] levelNames;
    public string[] sceneNames;
    public Sprite[] levelPicArray; 
    private int currentLevel = 0;
    [SerializeField] TMPro.TMP_Text levelNameDisplay;
    [SerializeField] Image levelPicture; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoNext()
    {
        if(currentLevel >= levelNames.Length - 1)
        {
            return; 
        }

        currentLevel++;
        levelNameDisplay.text = levelNames[currentLevel];
        levelPicture.sprite = levelPicArray[currentLevel];

    }

    public void GoPrev()
    {
        if (currentLevel <= 0)
        {
            return;
        }

        currentLevel--;
        levelNameDisplay.text = levelNames[currentLevel];
        levelPicture.sprite = levelPicArray[currentLevel];

    }

    public string GetLevelScene()
    {
        return sceneNames[currentLevel];
    }
}
