using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DanmakU.Fireables;



[Serializable]
public class GameDataDefinitions {
	//Player Abilities
	public static PlayerAbilityEffect[] PlayerAbilityEffects = {
		new PlayerAbilityEffect("Damage"),//0
		new PlayerAbilityEffect("Increased Damage Against Enemy"),//1
		new PlayerAbilityEffect("Increase Enemy Ability Charge Time"),//2	
		new PlayerAbilityEffect("Reduce Enemy Bullet Speed"),	//3
		new PlayerAbilityEffect("Clear Screen"),//4
		new PlayerAbilityEffect("Smaller Hitbox"),//5,
		new PlayerAbilityEffect("Faster Movement")//6
	};
	public static PlayerAbilityEffect Damage = PlayerAbilityEffects[0];
	public static PlayerAbilityEffect IncreasedDamageAgainstEnemy = PlayerAbilityEffects[1];
	public static PlayerAbilityEffect IncreaseEnemyAbilityChargeTime = PlayerAbilityEffects[2];
	public static PlayerAbilityEffect ReduceEnemyBulletSpeed = PlayerAbilityEffects[3];
	public static PlayerAbilityEffect ClearScreen = PlayerAbilityEffects[4];
	public static PlayerAbilityEffect SmallerHitbox = PlayerAbilityEffects[5];
	public static PlayerAbilityEffect FasterMovement = PlayerAbilityEffects[6];

	public static float basicAttackDamageReduction = 0.1f;
	public static float stunBaseDuration = 10f;
	public static PlayerAbilityEffect[] PabWithMags = {
		new PabWithMag(IncreasedDamageAgainstEnemy,0.1f),//Tailwhip 0
		new PabWithMag(Damage,40f),//Thundershock, Quick Attack 1
		new PabWithMag(ReduceEnemyBulletSpeed,0.1f),//Growl 2
		new PabWithMag(Damage,60f),//Electro Ball 3
		new PabWithMag(IncreaseEnemyAbilityChargeTime,stunBaseDuration),//Thunder Wave 4
		new PabWithMag(Damage,50f),//Feint 5
		new PabWithMag(SmallerHitbox,0.1f),//Double Team 6
		new PabWithMag(Damage,65f),//Spark 7
		new PabWithMag(Damage,20f),//Nuzzle 8
		new PabWithMag(IncreaseEnemyAbilityChargeTime,stunBaseDuration / 2),//Nuzzle 9
		new PabWithMag(Damage, 80f),//Discharge / Slam 10
		new PabWithMag(IncreaseEnemyAbilityChargeTime,stunBaseDuration / 2),//Discharge 11
		new PabWithMag(Damage, 90f),//Thunderbolt / Wild Charge 12
		new PabWithMag(FasterMovement,0.1f),//Agility 13
		new PabWithMag(Damage, 110f),//Thunder 14
	};
	public static PlayerAbility[] PlayerAbilities = {
		new PlayerAbility("Tail Whip", 1, 100, new PlayerAbilityEffect[] {PabWithMags[0]}),
		new PlayerAbility("Thunder Shock", 1, -1, new PlayerAbilityEffect[] {PabWithMags[1]}),
		new PlayerAbility("Growl", 5, 100, new PlayerAbilityEffect[] {PabWithMags[2]}),
		new PlayerAbility("Quick Attack", 10, 50, new PlayerAbilityEffect[] {PabWithMags[1]}),
		new PlayerAbility("Electro Ball", 13, 50, new PlayerAbilityEffect[] {PabWithMags[3]}), 
		new PlayerAbility("Thunder Wave", 18, 100, new PlayerAbilityEffect[] {PabWithMags[4]}), 
		new PlayerAbility("Feint", 21, 50, new PlayerAbilityEffect[] {PabWithMags[5]}),        
		new PlayerAbility("Double Team", 23, 100, new PlayerAbilityEffect[] {PabWithMags[6]}),  
		new PlayerAbility("Spark", 26, -1, new PlayerAbilityEffect[] {PabWithMags[7]}),        
		new PlayerAbility("Nuzzle", 29, 50, new PlayerAbilityEffect[] {PabWithMags[8],PabWithMags[9]}),       
		new PlayerAbility("Discharge", 34, 100, new PlayerAbilityEffect[] {PabWithMags[10]}),    
		new PlayerAbility("Slam", 37, 50, new PlayerAbilityEffect[] {PabWithMags[10]}),         
		new PlayerAbility("Thunderbolt", 42, -1, new PlayerAbilityEffect[] {PabWithMags[12]}),  
		new PlayerAbility("Agility", 45, 100, new PlayerAbilityEffect[] {PabWithMags[13]}),      
		new PlayerAbility("Wild Charge", 50, 50, new PlayerAbilityEffect[] {PabWithMags[12]}),  
		new PlayerAbility("Light Screen", 53, 100, new PlayerAbilityEffect[] {ClearScreen}), 
		new PlayerAbility("Thunder", 58, -1, new PlayerAbilityEffect[] {PabWithMags[14]})
	};

    //Enemy Data Definitions
    //https://forum.unity.com/threads/how-do-you-load-a-prefab-with-resources-load.393752/
    //https://forum.unity.com/threads/how-to-change-sprite-image-from-script.212307/
	//Some random sprites
    public static Sprite[] PokemonSprites = {
		Resources.Load("018-pidgeot@3x") as Sprite,
		Resources.Load("065-alakazam@3x") as Sprite,
		Resources.Load("076-golem@3x") as Sprite,
		Resources.Load("094-gengar@3x") as Sprite,
		Resources.Load("184-azumarill@3x") as Sprite,
		Resources.Load("254-sceptille@3x") as Sprite,
		Resources.Load("356-dusclops@3x") as Sprite,
		Resources.Load("359-absol@3x") as Sprite,
		Resources.Load("365-walrein@3x") as Sprite,
		Resources.Load("373-salamence@3x") as Sprite,
		Resources.Load("382-kyogre@3x") as Sprite,
		Resources.Load("383-groudon@3x") as Sprite,
		Resources.Load("384-rayquaza@3x") as Sprite,
    };
    public static readonly int P_LAYER = 1;
    public static readonly int EM_1 = 0, EM_2 = 1, EM_3 = 2;
	public static Fireable[] fireables = {
		//Wing Attack [0,1,2]
		chainFireables(new Arc(1f,2f,1), new Line(12,5)),
		chainFireables(new Arc(4,1,2), new Line(4,10)),
		chainFireables(new Arc(20,1.2f,2), new Line(1,30)),
		//Gust [3]
		chainFireables(new Arc(10,1.2f,2), new Line(1,20)),
		//Twister [4]
		chainFireables(new Arc(6,1.2f,2), new Ring(2,5), new Circle(2,10)),
		//Air Slash [5]
		chainFireables(new Ring(8,5)),
	};
    public static PokemonAbility[] PokemonAbilities = {
		new PokemonAbility("Air Slash",30,new PokemonAbilitySequence[] {
                new PokemonAbilitySequence(30,5f,0,270,EM_1,true,90,0,Color.white,5,0,
                    fireables[5], 270, P_LAYER),
				new PokemonAbilitySequence(10,5f,-0.4f,270,EM_1,false,90,0,Color.white,5,0,
                    null, 270, P_LAYER),
        }),
		new PokemonAbility("Wing Attack",30,new PokemonAbilitySequence[] {
                new PokemonAbilitySequence(25,3.2f,0,0,EM_1,true,40,0,Color.white,5,0,
                    fireables[0], 270, P_LAYER),
				new PokemonAbilitySequence(30,0,0,0,EM_1,true,40,0,Color.white,4,0,
                    fireables[1], 240, P_LAYER),
				new PokemonAbilitySequence(30,0,0,0,EM_1,true,70,0.2f,Color.white,1,0,
                    fireables[2], 200, P_LAYER)
        }),
		new PokemonAbility("Twister",30,new PokemonAbilitySequence[] {
            new PokemonAbilitySequence(100,10,20,0,EM_1,true,50,0,Color.white,5,0,fireables[4], 270, P_LAYER)
        }),
		new PokemonAbility("Gust",30,new PokemonAbilitySequence[] {
                new PokemonAbilitySequence(30,0,0,0,EM_1,true,40,-0.2f,Color.white,5,0,fireables[3], 270, P_LAYER),
				new PokemonAbilitySequence(30,0,0,0,EM_2,true,40,0.2f,Color.white,4,0,fireables[3], 270, P_LAYER),
        }),
	};
    public static Pokemon[] Pokemon = {
        new Pokemon("Pidgeot",PokemonSprites[0], 900, false, false, new PokemonAbility[] {
            PokemonAbilities[0],PokemonAbilities[1],PokemonAbilities[2],PokemonAbilities[3]
        })
    };
    public static Trainer[] Trainers = {
        new Trainer("Rival", new Pokemon[] {Pokemon[0]})
    };

	public static Fireable chainFireables(params Fireable[] fireables) {
		int i = 0;
		Fireable finalFireable = null;
		if(fireables.Length > 0) {
			finalFireable = fireables[0];
			i++;
		}
		while(i < fireables.Length) {
			finalFireable = finalFireable.Of(fireables[i]);
			i++;
		}
		return finalFireable;
	}

	public static Fireable chainFireableArr(Fireable[] fireables) {
		int i = 0;
		Fireable finalFireable = null;
		if(fireables.Length > 0) {
			finalFireable = fireables[0];
			i++;
		}
		while(i < fireables.Length) {
			finalFireable = finalFireable.Of(fireables[i]);
			i++;
		}
		return finalFireable;
	}
}
