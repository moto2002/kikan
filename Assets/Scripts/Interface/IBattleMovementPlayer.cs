﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IBattleMovementPlayer {
    GameObject  gameObject { get; }
    Transform   Transform  { get; }
    Rigidbody2D Rigid      { get; }
    Core        Core       { get; }
  }
}
