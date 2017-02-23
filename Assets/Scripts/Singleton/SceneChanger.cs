﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour {
  public void ChangeScene(string nextSceneName) {
    PhotonNetwork.isMessageQueueRunning = false;

    SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);

    PhotonNetwork.isMessageQueueRunning = true;
  }
}
