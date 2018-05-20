using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Unity.GUISy;


[Serializable]
public class PlayerUi {
	public string PlayerName;
    protected string PlayerLv;
    protected int Lv;
    public Text pokemonName;
	public Text pokemonLv;
    public int Level {
    get {return Lv;}
    set {
      Lv = value;
      PlayerLv = "Lv"+Lv;
    }
  }

	public PlayerUi(string PlayerName, int PlayerLv) {
        this.PlayerName = PlayerName;
        this.Level = PlayerLv;
        this.pokemonName.text = this.PlayerName;
        this.pokemonLv.text = this.PlayerLv;
	}
}