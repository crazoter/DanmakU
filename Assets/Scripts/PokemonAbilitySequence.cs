using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanmakU;
using DanmakU.Fireables;

[Serializable]
public class PokemonAbilitySequence {
	public int durationFrames;
	public float movementSpeed;
	public float movementAngularSpeed;
	public float movementRotationInRadians;

	//Emitter Data
	public int emitterIndex;
	public bool isShooting;
	public Range Speed = 5f;
	public Range AngularSpeed;
	public Color Color = Color.white;
	public Range FireRate = 5;
	public float FrameRate;
	public Fireable fireableChain;
	public float RotationInDegrees = -1;
	public int layer = 0;

	public PokemonAbilitySequence(
	int durationFrames,float movementSpeed,float movementAngularSpeed,float movementRotationInDegrees,
	int emitterIndex, 
	bool emitterShooting, Range emitterSpeed,Range emitterAngularSpeed,Color emitterColor,Range emitterFireRate,float emitterFrameRate,
	Fireable chainedFireable,float emitterRotationInDegrees,int emitterlayer) {
		this.durationFrames = durationFrames;
		this.movementSpeed = movementSpeed;
		this.movementAngularSpeed = movementAngularSpeed;
		this.movementRotationInRadians = Mathf.Deg2Rad * movementRotationInDegrees;
		this.emitterIndex = emitterIndex;
		this.isShooting = emitterShooting;
		this.Speed = emitterSpeed;
		this.AngularSpeed = emitterAngularSpeed;
		this.Color = emitterColor;
		this.FireRate = emitterFireRate;
		this.FrameRate = emitterFrameRate;
		//this.fireableChain = chainFireableArray(emitterfireables);
		this.fireableChain = chainedFireable;
		this.RotationInDegrees = emitterRotationInDegrees;
		this.layer = emitterlayer;
	}

	public Fireable chainFireableArray(Fireable[] emitterfireables) {
		return GameDataDefinitions.chainFireableArr(emitterfireables);
	}

	public void applyTo(DanmakuEmitter emitter) {
		//Debug.Log("applyTo "+(fireableChain==null?"NULL":""));
		emitter.isShooting = isShooting;
		emitter.Speed = Speed;
		emitter.AngularSpeed = AngularSpeed;
		emitter.Color = Color;
		emitter.FireRate = FireRate;
		emitter.FrameRate = FrameRate;
		emitter.RotationInDegrees = RotationInDegrees;
		emitter.layer = layer;
		if(fireableChain != null) {
			emitter.restart(fireableChain);
		}
	}
}
