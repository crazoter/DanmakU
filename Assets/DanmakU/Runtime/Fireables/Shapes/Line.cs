using System;
using UnityEngine;

namespace DanmakU.Fireables {

[Serializable]
public class Line : Fireable {

  public Range Count = 1;
  public Range DeltaSpeed;

  public Line(Range Count, Range DeltaSpeed) {
    this.Count = Count;
    this.DeltaSpeed = DeltaSpeed;
  }

  public override void Fire(DanmakuConfig config) {
    var count = Mathf.RoundToInt(Count.GetValue());
    //Debug.Log("Line Fire:"+count);
    var deltaSpeed = DeltaSpeed.GetValue();
    for (var i = 0; i < count; i++) {
      config.Speed += deltaSpeed;
      Subfire(config);
    }
  }

}

}