﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class HeavyEffect : MonoBehaviour {
    void Start() {
      _character = transform.parent.gameObject.GetComponent<ICharacter>();
      _character.State.Heavy = true;
      _character.SetMoveForce(0.5f);
    }

    void OnDestroy() {
      _character.State.Heavy = false;
      _character.SetMoveForce(0.5f);
    }

    private ICharacter _character;
  }
}

