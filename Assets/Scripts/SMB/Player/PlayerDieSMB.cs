﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class PlayerDieSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null) {
        _player     = animator.GetComponent<Player>();
        _respawner  = animator.GetComponent<PlayerRespawner>();
      }

      _player.BodyCollider.enabled = false;

      if (!_player.PhotonView.isMine)
        return;

      _player.AudioEnvironment.PlayOneShot("Die", 0.5f);
      SkillReference.Instance.DeleteAll();

      if (StageReference.Instance.StageData.Name == "Battle") {
        Action action = () => { _player.StateTransfer.TransitTo("Idle", animator); };
        _respawner.Respawn(action);
      }

      if (StageReference.Instance.StageData.Name == "FinalBattle") {
        int[] alivePlayerCount = PhotonNetwork.room.CustomProperties["AlivePlayerCount"] as int[];
        var list = MonoUtility.ToList<int>(alivePlayerCount);

        list[_player.PlayerInfo.Team] -= 1;

        var props = new Hashtable() {{ "AlivePlayerCount", list.ToArray() }};
        PhotonNetwork.room.SetCustomProperties(props);
      }
    }

    private Player _player;
    private PlayerRespawner _respawner;
  }
}
