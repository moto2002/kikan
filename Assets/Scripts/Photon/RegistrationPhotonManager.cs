﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class RegistrationPhotonManager : Photon.PunBehaviour {
    void Awake() {
      PhotonNetwork.logLevel = _logLevel;
      PhotonNetwork.autoJoinLobby = false;
      PhotonNetwork.automaticallySyncScene = true;
      PhotonNetwork.sendRate = 30;
      PhotonNetwork.sendRateOnSerialize = 30;

      Screen.SetResolution(1366, 768, false);
    }

    void Start() {
      _controlPanel.SetActive(true);
      _progressLabel.SetActive(false);
    }

    public void Connect() {
      _isConnecting = true;

      _controlPanel.SetActive(false);
      _progressLabel.SetActive(true);

      if (PhotonNetwork.connected)
        PhotonNetwork.JoinRoom("Lobby");
      else
        PhotonNetwork.ConnectUsingSettings(GameData.Instance.GameVersion);
    }

    public override void OnConnectedToMaster() {
      Debug.Log("OnConnectedToMaster() was called");

      if (_isConnecting)
        PhotonNetwork.JoinRoom("Lobby");
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
      Debug.Log("OnPhotonJoinRoomFailed() was called");

      RoomOptions roomOptions = new RoomOptions();
      roomOptions.MaxPlayers = _maxPlayers;

      PhotonNetwork.CreateRoom("Lobby", roomOptions, null);
    }

    public override void OnJoinedRoom() {
      Debug.Log("OnJoinedRoom() was called");
      SceneChanger.Instance.ChangeScene(_nextSceneName);
    }

    [SerializeField] private PhotonLogLevel _logLevel;
    [SerializeField] private GameObject _controlPanel;
    [SerializeField] private GameObject _progressLabel;
    [SerializeField] private byte _maxPlayers;
    [SerializeField] private string _nextSceneName;
    private bool _isConnecting;
  }
}

