using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity.GUISy;



[Serializable]
public class PokemonAbility {
	public String name;
	public int chargeFrames;//Time between previous and this ability
	public PokemonAbilitySequence[] sequences;

	public PokemonAbility(String name, int chargeFrames, PokemonAbilitySequence[] sequences) {
		this.name = name;
		this.chargeFrames = chargeFrames;
		this.sequences = sequences;
	}
}
