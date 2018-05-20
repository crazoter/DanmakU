using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity.GUISy;



[Serializable]
public class PlayerAbility {
	public String name;
	public int levelRequirement;
	public PlayerAbilityEffect[] playerAbilityEffects;

	public int chargeAmount;

	public PlayerAbility(String n, int lv, int chargeAmt, PlayerAbilityEffect[] p) {
		name = n;
		levelRequirement = lv;
		playerAbilityEffects = p;
		chargeAmount = chargeAmt;
	}
}
