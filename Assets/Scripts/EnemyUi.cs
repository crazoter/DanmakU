using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity.GUISy;


[Serializable]
public class EnemyUi {
	public Sprite pokeballActive;
	public Sprite pokeballInactive;
	public Image[] pokeballs; 
	public Text pokemonName;
	public Text pokemonLv;
	public Slider pokemonHp;

	public EnemyUi(Sprite pokeballActive, Sprite pokeballInactive, Image[] pokeballs, Text pokemonName, Text pokemonLv, Slider pokemonHp) {
		this.pokeballActive = pokeballActive;
		this.pokeballInactive = pokeballInactive;
		this.pokeballs = pokeballs;
		this.pokemonName = pokemonName;
		this.pokemonLv = pokemonLv;
		this.pokemonHp = pokemonHp;
	}
}