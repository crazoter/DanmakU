using System.Collections.Generic;
using UnityEngine;

namespace DanmakU.Fireables{

internal class RandomSubemitterFireable : IFireable {

  public List<IFireable> Subemitters { get; set;}

  public RandomSubemitterFireable(IEnumerable<IFireable> subemitters) {
    Debug.Log("test 5");
    Subemitters = new List<IFireable>(subemitters);
  }

  public void Fire(DanmakuConfig state) {
    Debug.Log("test 6");
    var subemitter = Subemitters[Mathf.FloorToInt(Random.value * Subemitters.Count)];
    if (subemitter != null)
      subemitter.Fire(state);
  }

}

}