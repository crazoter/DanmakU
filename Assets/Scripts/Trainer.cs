using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Trainer {
	public Pokemon[] pokemon;
	public String name;
    //dialog
    //other data like sprites or encoding data
    public Trainer(String name, Pokemon[] pokemon) {
        this.name = name;
        this.pokemon = pokemon;
    }
}
