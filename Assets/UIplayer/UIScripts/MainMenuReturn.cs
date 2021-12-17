using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MainMenuReturn : MonoBehaviourPunCallbacks
{
    public void GoToMain(){
      StartCoroutine(leaveRoom());
    }

    IEnumerator leaveRoom(){
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom){
            yield return null;
        }
          SceneManager.LoadScene(0);
    }
}
