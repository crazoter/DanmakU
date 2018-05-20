using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using DanmakU;

public enum BattleState {
	NONE,
	ANIMATE_ENEMY_FLYING_IN,
	BATTLING,
	PLAYER_DEFEATED,
	ENEMY_POKEMON_DEFEATED,
	ENEMY_TRAINER_DEFEATED
};

[AddComponentMenu("/Battle Manager")]
public class BattleManager : MonoBehaviour {
	//Todo: manage exit
	protected static BattleManager battleManager;
	public static BattleManager getInstance() {
		return battleManager;
	}
	public PlayerUi PlayerUI;
	public EnemyUi EnemyUI;
	protected TrainerInBattle TrainerInBattle;
	public Trainer TrainerReference {
		get { return TrainerInBattle != null? TrainerInBattle.trainerReference : null; }
		set {
			TrainerInBattle = new TrainerInBattle(value);
		}
	}
	public BattleDialogUi BattleDialogUI;
	public const float BATTLE_TEXT_LINGER = 3.0f;
	public BattleState CurrentBattleState = BattleState.NONE;
	public GameObject PlayerGameObject;
	public GameObject EnemyGameObject;
	
	//Hardcoded values for enemy entering scene and beginning battle
	public const float ENEMY_START_POS_X = 0;
	public const float ENEMY_START_POS_Y = 200;
	public const float ENEMY_BEGIN_BATTLE_POS_X = 0;//This is only used in TrainerInBattle for repositioning after an ability
	public const float ENEMY_BEGIN_BATTLE_POS_Y = 80;

	protected DeltaTimeHelper deltaTimeUi = new DeltaTimeHelper(23);
	protected DeltaTimeHelper deltaTimeGame = new DeltaTimeHelper(30);

	void Start () {
		battleManager = this;
		//BattleDialogUI.close();
		BattleDialogUI.init();
		BattleDialogUI.open("A wild pidgeot appeared!");

		EnemyGameObject.layer = 2;

		//Set trainer programmatically in the start for now
		TrainerReference = GameDataDefinitions.Trainers[0];
		
		//Assert.IsNotNull(TrainerReference);
		Assert.IsNotNull(PlayerGameObject);
		Assert.IsNotNull(EnemyGameObject);

		InitAnimateEnemyFlyingIn();
	}

	void resetEnemy() {

	}

	void InitAnimateEnemyFlyingIn() {
		Vector3 position = EnemyGameObject.transform.position;
		position.x = ENEMY_START_POS_X;
		position.y = ENEMY_START_POS_Y;
		EnemyGameObject.transform.position = position;
		CurrentBattleState = BattleState.ANIMATE_ENEMY_FLYING_IN;
	}
	
	void Update () {
		if(deltaTimeUi.doRun()) {
			if(BattleDialogUI.needsUpdate()) {
				if(BattleDialogUI.updateText() == false) {
					//Invoke("finishedUpdatingBattleText",BATTLE_TEXT_LINGER);
				}
			} else {
				BattleDialogUI.delayedAnimatedClose(20);
			}
		}
		if(deltaTimeGame.doRun()) {
			switch(CurrentBattleState) {
				case BattleState.ANIMATE_ENEMY_FLYING_IN:
					Vector3 pos;
					if((pos = EnemyGameObject.transform.position).y > ENEMY_BEGIN_BATTLE_POS_Y) {
						pos.y -= 2;
						EnemyGameObject.transform.position = pos;
					} else {
						CurrentBattleState = BattleState.BATTLING;
					}
					break;
				case BattleState.BATTLING:
					TrainerInBattle.UpdatePokemon();
					break;
				default:
					break;
			}
		}
	}
	void startNewBattle() {

	}

	void newBattleText(string s) {
		BattleDialogUI.open(s);
	}

	void finishedUpdatingBattleText() {
		BattleDialogUI.close();
	}
}
