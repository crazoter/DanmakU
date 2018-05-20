using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity.GUISy;



[Serializable]
public class Pokemon {
	public Sprite Sprite;
	public string name;
	public int health;
	public bool immuneAgainstNormal;
	public bool immuneAgainstElectric;

	public PokemonAbility[] abilities;

	public Pokemon(string name, Sprite s, int hp, bool immuneAgainstNormal, bool immuneAgainstElectric, PokemonAbility[] abilities) {
		this.name = name;
		Sprite = s;
		health = hp;
		this.immuneAgainstNormal = immuneAgainstNormal;
		this.immuneAgainstElectric = immuneAgainstElectric;
		this.abilities = abilities;
	}
	public bool isImmuneTo(PokemonType type) {
		switch(type) {
			case PokemonType.Normal: return immuneAgainstNormal;
			case PokemonType.Electric: return immuneAgainstElectric;
		}
		return false;
	}
}
