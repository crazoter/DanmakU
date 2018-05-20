using System.Collections;
using System.Collections.Generic;
using DanmakU.Fireables;
using UnityEngine;

namespace DanmakU {

[AddComponentMenu("DanmakU/Danmaku Emitter")]
public class DanmakuEmitter : DanmakuBehaviour {

  public DanmakuPrefab DanmakuType;

  public Range Speed = 5f;
  public Range AngularSpeed;
  public Color Color = Color.white;
  public Range FireRate = 5;
  public float FrameRate;

  public Ring ring;
  public Circle circle;
  public Arc arc;
  public Line line;

  public float RotationInDegrees = -1;
  
  public int initialLayer = 0;
  protected int layerMask = 0;
  protected int gameObjectLayer = 0;
  public int layer {
    get {return gameObjectLayer;}
    set {
      gameObjectLayer = value;
      layerMask = 1 << gameObjectLayer;
    }
  }

  float timer;
  DanmakuConfig config;
  public IFireable fireable;
  public bool isShooting = false;
  protected Range oldArcCount;
  protected Range oldLineCount;

  private DanmakuSet set;
  // https://stackoverflow.com/questions/2360747/can-i-have-fixed-typed-arraylist-in-c-just-like-c
  private Queue<DanmakuSet> oldSets = new Queue<DanmakuSet>();//DanmakuSet oldSet;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start() {
    if (DanmakuType == null) {
      Debug.LogWarning($"Emitter doesn't have a valid DanmakuPrefab", this);
      return;
    }

    layer = initialLayer;

    //int layerMask = 1 << (layer);
    print("Emitter layer:"+layerMask);
    set = CreateSet(DanmakuType,layerMask);
    set.AddModifiers(GetComponents<IDanmakuModifier>());
    //Arc = new Fireable();
    fireable = arc.Of(line).Of(set);
    //fireable = line.Of(ring).Of(circle).Of(set);
  }

  public void restart(Fireable fireablesChain) {
    //assumes that fireable is already set outside of this method
    //also assumes all necessary changes have been applied
    if(set != null) {
      oldSets.Enqueue(set);
      Invoke("eraseSet",15.0f);
      set = CreateSet(DanmakuType,layerMask);
      set.AddModifiers(GetComponents<IDanmakuModifier>());
    }
    if(fireablesChain != null) {
      fireable = fireablesChain.Of(set);
    }
    timer = 1f / FireRate.GetValue();
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  //int x = 0;
  void Update() {
    if(!isShooting) return;
    if (fireable == null) return;
    var deltaTime = Time.deltaTime;
    if (FrameRate > 0) {
      deltaTime = 1f / FrameRate;
    }
    timer -= deltaTime;
    if (timer < 0) {
      /*
      if(x == 1) {
        oldSets.Enqueue(set);
        AngularSpeed = -0.1f;
        set = CreateSet(DanmakuType,layerMask);
        set.AddModifiers(GetComponents<IDanmakuModifier>());
        fireable = arc.Of(ring).Of(circle).Of(set);
        x++;
        Invoke("eraseSet",2.0f);
        //DanmakuManager.Instance.DestroyDanmakuSet(set);//DestroyDanmakuSet(set);
      } else {
        x++;
        AngularSpeed *= -1;
        if(x == 5) {
          isShooting = false;
          //oldSet = set;
          //Invoke("eraseSet",2.0f);
        }
      }*/
      //set = CreateSet(DanmakuType,layerMask);
      //set.AddModifiers(GetComponents<IDanmakuModifier>());
      //fireable = arc.Of(ring).Of(circle).Of(set);
      config = new DanmakuConfig {
        Position = transform.position,
        Rotation = (RotationInDegrees > 0 ? RotationInDegrees : transform.rotation.eulerAngles.z) * Mathf.Deg2Rad,
        Speed = Speed,
        AngularSpeed = AngularSpeed,
        Color = Color
      };
      fireable.Fire(config);
      timer = 1f / FireRate.GetValue();
    }
  }
  void eraseSet() {
    if(oldSets.Count > 0) {
      //todo: some lock for sync
      DanmakuManager.Instance.DestroyDanmakuSet(oldSets.Dequeue());
    }
  }
}

}