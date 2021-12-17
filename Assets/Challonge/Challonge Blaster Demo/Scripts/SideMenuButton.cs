using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Challonge.Examples
{
    public class SideMenuButton : MonoBehaviour
    {
        public string ScreenName;

        public TextMeshProUGUI text;

        public void ShowScreen()
        {
            List<MainContentScreen> mainContentScreens = new List<MainContentScreen>();
            transform.parent.parent.Find("Main Content").GetComponentsInChildren<MainContentScreen>(true, mainContentScreens);

            text.text = ScreenName;

            for (int i = 0; i < mainContentScreens.Count; i++)
                mainContentScreens[i].gameObject.SetActive(false);

            for (int i = 0; i < mainContentScreens.Count; i++)
            {
                if (mainContentScreens[i].gameObject.name == ScreenName)
                {
                    mainContentScreens[i].gameObject.SetActive(true);
                    mainContentScreens[i].loadActions.Invoke();
                }
            }
        }
    }
}
