﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StunSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_character == null) {
        _character = animator.transform.parent.GetComponent<ICharacter>();
        _stateEffect = animator.GetComponent<StateEffect>();
        _transform = animator.GetComponent<Transform>();
      }

      _transform.Translate(Vector2.up * 0.5f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (!_character.State.Stun) { _stateEffect.StateTransfer.TransitTo("None", animator); }
    }

    private ICharacter _character;
    private StateEffect _stateEffect;
    private Transform _transform;
  }
}
