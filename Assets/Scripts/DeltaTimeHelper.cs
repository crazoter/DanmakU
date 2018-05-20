using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DeltaTimeHelper {
    public float FrameRate;
    float timer;

    public DeltaTimeHelper(float FrameRate) {
        this.FrameRate = FrameRate;
    }

    public bool doRun() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            timer = 1f / FrameRate;
            return true;
        }
        return false;
    }
}