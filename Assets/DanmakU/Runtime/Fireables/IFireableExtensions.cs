using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace DanmakU.Fireables {

public static class FireableExtensions {
  
  public delegate void FireAction(Action<DanmakuConfig> fire, DanmakuConfig state);

  class FuncFireable : Fireable {

    readonly FireAction _fireAction;

    public FuncFireable(FireAction fireAction ) {
        Assert.IsNotNull(fireAction);
        _fireAction = fireAction;
    }

    public override void Fire(DanmakuConfig state) {
        _fireAction(Subfire, state);
    }

  }

  static Fireable GetLowestChild(Fireable fireable) {
    var last = fireable;
    while (fireable != null) {
      last = fireable;
      fireable = fireable.Child as Fireable;
    }
    return last;
  }

  public static Fireable Of(this Fireable fireable, IFireable subemitter) {
    Debug.Log("test 1 ");
    if (fireable == null) {
      throw new ArgumentNullException("fireable");
    }
    var lowest = GetLowestChild(fireable);
    lowest.Child = subemitter;
    return fireable;
  }

  public static Fireable Of(this Fireable fireable, FireAction fireAction) {
    Debug.Log("test 2");
    return fireable.Of(new FuncFireable(fireAction));
  }

  public static Fireable Of(this Fireable fireable, IEnumerable<IFireable> subemitters) {
    Debug.Log("test 3 ");
    return fireable.Of(new RandomSubemitterFireable(subemitters));
  }

  public static Fireable Of(this Fireable fireable, params IFireable[] subemitters) {
      Debug.Log("test 4 ");
      return fireable.Of(new RandomSubemitterFireable(subemitters));
  }

}

}
