﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class ManjiSpace : Skill {
    void Awake() {
      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Subscribe(_ => transform.parent = _skillUserObj.transform );
    }

    void Start() {
      if (photonView.isMine) {
        EnhanceStatus();
        InstantiateBuff();
      }
    }

    void OnDestroy() {
      if (photonView.isMine) {
        ResetStatus();
        SkillReference.Instance.Remove(viewID);
      }
    }

    private void EnhanceStatus() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      float statusRatio = 1.3f;
      float powerRatio = 1.5f;
      if (skillUser.Level.Cur.Value >= 11)
        statusRatio = 1.6f;
        powerRatio = 2.0f;

      _spaceFixSpd = new FixSpd(skillUser.Status.Spd * statusRatio, FixSpdType.Buff);
      skillUser.FixSpd.Add(_spaceFixSpd);
      skillUser.Movement.SetJumpForce(skillUser.Status.Jmp * statusRatio);
      skillUser.Movement.SetLadderRatio(statusRatio);

      skillUser.Synchronizer.SyncFixAtk(powerRatio);

      ResetStatus = () => {
        skillUser.FixSpd.Remove(_spaceFixSpd);
        skillUser.Movement.SetJumpForce(skillUser.Status.Jmp);
        skillUser.Movement.SetLadderRatio(1.0f);

        skillUser.Synchronizer.SyncFixAtk(1.0f);
      };

      SkillReference.Instance.Register(viewID, _buffTime, () => { PhotonNetwork.Destroy(gameObject); });
    }

    private void InstantiateBuff() {
      var skillUser = _skillUserObj.GetComponent<Player>();

      var buff = PhotonNetwork.Instantiate("Prefabs/Skill/Manji/SpaceBuff", Vector3.zero, Quaternion.identity, 0).GetComponent<SkillBuff>() as SkillBuff;
      buff.ParentSetter.SetParent(skillUser.PhotonView.viewID);

      SkillReference.Instance.Register(buff.viewID, _buffTime, () => { PhotonNetwork.Destroy(buff.gameObject); });
    }

    private Action ResetStatus;
    private float _buffTime = 20.0f;
    private FixSpd _spaceFixSpd;
  }
}

