using System;
using System.Collections;
using System.Collections.Generic;
using DanmakU;
using UnityEngine;
//using UnityEngine.UI;

public enum TrainerState {
    CHARGING,
    PRE_SKILL_DIALOG,
    USING_ABILITY,
    END_ABILITY,
    PLAYER_TURN,
    END_PLAYER_ABILITY
}

[Serializable]
public class TrainerInBattle {
    public static System.Random rand = new System.Random();
    public const int REPOSITION_DURATION_IN_FRAMES = 60;
	public Pokemon currentPokemonReference;
    public PokemonAbility skillInUse;
    public int currentPokemonHealth;
    public int currentPokemonIndex = -1;
	public Trainer trainerReference;
    public TrainerState currentTrainerState;
    //C# callback / promise thing
    //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/how-to-declare-instantiate-and-use-a-delegate
    //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/using-delegates
    //https://stackoverflow.com/questions/12857508/storing-passing-delegates-as-variables
    public delegate void PokemonDefeated();
    public delegate void TrainerDefeated(bool defeated);
    //Runtime variables
    public int currentChargeFrame = 30;
    //Renamed because the variables were getting too long
    public int skillFrame = 0;
    public int skillIndex = 0;
    public float enemyRotationModifier = 0;

    protected const int DELAY_FRAMES_FOR_DIALOG = 80;

    public TrainerInBattle(Trainer trainerReference) {
        this.trainerReference = trainerReference;
        this.currentPokemonReference = trainerReference.pokemon[0];
        //Set it such that dialog has some time to dissipate
        currentChargeFrame = 30;
        this.currentTrainerState = TrainerState.CHARGING;
    }

    public void UpdatePokemon() {
        switch(this.currentTrainerState) {
            case TrainerState.CHARGING:
                currentChargeFrame--;
                if(currentChargeFrame <= 0) {
                    //Pokemon use ability
                    //for(int i=0;i<20;i++)
                        //Debug.Log(rand.Next(0,currentPokemonReference.abilities.Length));
                    skillInUse = currentPokemonReference.abilities[rand.Next(0,currentPokemonReference.abilities.Length)];
                    currentChargeFrame = DELAY_FRAMES_FOR_DIALOG;
                    currentTrainerState = TrainerState.PRE_SKILL_DIALOG;
                    skillIndex = 0;
                    skillFrame = 0;
                    BattleManager.getInstance().BattleDialogUI.open(currentPokemonReference.name+" used "+skillInUse.name+"!");
                }
                break;
            case TrainerState.PRE_SKILL_DIALOG:
                //Delay while skill dialog appears
                if(currentChargeFrame-- <= 0) {
                    currentChargeFrame = 0;
                    currentTrainerState = TrainerState.USING_ABILITY;
                }
                break;
            case TrainerState.USING_ABILITY:
                if(skillIndex < skillInUse.sequences.Length) {
                    PokemonAbilitySequence seq = skillInUse.sequences[skillIndex];
                    if(skillFrame < seq.durationFrames) {
                        if(skillFrame == 0) {
                            //Debug.Log("Apply Emitter settings");
                            //Apply sequence to emitter
                            DanmakuEmitter emitter = BattleManager.getInstance().EnemyGameObject.GetComponents<DanmakuEmitter>()[seq.emitterIndex];
                            seq.applyTo(emitter);
                        }
                        updateEnemyMovementInBattle(seq);

                        skillFrame++;
                    } else {
                        skillIndex++;
                        enemyRotationModifier = 0;
                        skillFrame = 0;
                    }
                } else {
                    currentChargeFrame = REPOSITION_DURATION_IN_FRAMES;
                    skillFrame = 0;
                    skillIndex = 0;
                    this.currentTrainerState = TrainerState.END_ABILITY;
                }
                break;
            case TrainerState.END_ABILITY:
                //Debug.Log("END_ABILITY");
                if(currentChargeFrame == REPOSITION_DURATION_IN_FRAMES) {
                    DanmakuEmitter[] emitters = BattleManager.getInstance().EnemyGameObject.GetComponents<DanmakuEmitter>();
                    for(int i=0;i<emitters.Length;i++) {
                        emitters[i].isShooting = false;
                    }
                } 
                if(currentChargeFrame > 0) {
                    updateRepositionEnemy();
                    currentChargeFrame--;
                } else {
                    this.currentTrainerState = TrainerState.PLAYER_TURN;
                }
                break;
            case TrainerState.PLAYER_TURN:
                //todo: move enemy around
                this.currentTrainerState = TrainerState.END_PLAYER_ABILITY;
                break;
            case TrainerState.END_PLAYER_ABILITY:
                //animate move enemy back to position
                this.currentChargeFrame = skillInUse.chargeFrames;
                this.currentTrainerState = TrainerState.CHARGING;
                break;

        }
    }
    
    public void updateRepositionEnemy() {
        GameObject enemy = BattleManager.getInstance().EnemyGameObject;
        Vector2 pos = new Vector2(
            enemy.transform.position.x + (BattleManager.ENEMY_BEGIN_BATTLE_POS_X - enemy.transform.position.x)/currentChargeFrame,
            enemy.transform.position.y + (BattleManager.ENEMY_BEGIN_BATTLE_POS_Y - enemy.transform.position.y)/currentChargeFrame);
        enemy.transform.position = pos;
    }
    public void updateEnemyMovementInBattle(PokemonAbilitySequence seq) {
        if(seq.movementSpeed > 0) {
            GameObject enemy = BattleManager.getInstance().EnemyGameObject;
            float modifiedAngle = seq.movementRotationInRadians + enemyRotationModifier;
            enemyRotationModifier += seq.movementAngularSpeed;
            Vector2 pos = new Vector2(enemy.transform.position.x,enemy.transform.position.y);
            //Reminder: it's in radians
            enemy.transform.position = pos + (seq.movementSpeed * RotationUtiliity.ToUnitVector(modifiedAngle));
        }
    }
    
    public void pokemonHurt(int damage, PokemonDefeated callback) {
        currentPokemonHealth -= damage;
        if(currentPokemonHealth < 0) {
            callback();
        }
    }
    public void setNextPokemon(TrainerDefeated callback) {
        currentPokemonIndex++;
        if(currentPokemonIndex < trainerReference.pokemon.Length) {
            this.currentPokemonReference = trainerReference.pokemon[currentPokemonIndex];
            this.currentPokemonHealth = this.currentPokemonReference.health;
            callback(false);
        } else {
            callback(true);
        }
    }
}
