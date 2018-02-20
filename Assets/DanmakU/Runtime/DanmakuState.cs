using System;
using UnityEngine;

namespace DanmakU {

/// <summary>
/// A config for creating <see cref="DanmakU.DanmakuState"/>.
/// </summary>
[Serializable]
public struct DanamkuConfig {

  public Vector2 Position;
  public Range Rotation;
  public Range Speed;
  public Range AngularSpeed;
  public Color Color;

  /// <summary>
  /// Creates an state based on the config.
  /// </summary>
  /// <returns>a sampled state from the config's state space.</returns>
  public DanmakuState CreateState() {
    return new DanmakuState {
      Position = Position,
      Rotation = Rotation.GetValue(),
      Speed = Speed.GetValue(),
      AngularSpeed = AngularSpeed.GetValue(),
      Color = Color
    };
  }

}

/// <summary>
/// A snapshot of a <see cref="DanmakU.Danmaku"/>'s state.
/// </summary>
[Serializable]
public struct DanmakuState {
  public Vector2 Position;
  public float Rotation;
  public float Speed;
  public float AngularSpeed;
  public Color Color;
}

}