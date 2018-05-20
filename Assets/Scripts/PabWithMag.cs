using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity.GUISy;



public class PabWithMag : PlayerAbilityEffect {
	public float magnitude;

	public PabWithMag(PlayerAbilityEffect p, float m) : base(p.name) {
		magnitude = m;
	}
}
