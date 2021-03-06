﻿using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	/*Variables publicas para configurar el ataque*/
	public float meleeDamage=70;
	public float attackCoolDown=3;
	public float meleeAttackRange=5;				//El rango de ataque del enemigo cuerp a cuerpo 			
	public float moveSpeed=10;

	private float attackCDTimer=0;
	private bool isTargetSelected=false;		//Dice si en determinado instante, el jugador tiene un objetivo señalado
	private bool isAttacking=false;				//Dice si en determinado instante, el enemigo esta atacando al jugador
	private Animation anim;

	private enemyStatusGUI attackedEnemyScript=null;		//acceso al script de estado del enemigo
	
	void Start () {
		anim = GetComponent<Animation> ();
		meleeAttackRange *= meleeAttackRange;			//Usamos distancia al cuadrado para ahorrarnos la raiz cuadrada
	}
	// Update is called once per frame
	void Update () {
		//Evalua si esta atacando un enemigo
		if (attackedEnemyScript != null && isTargetSelected) {			//Si ha seleccionado a un enemigo, este valor no debe ser nulo
			var distanceToEnemy = Vector3.SqrMagnitude (attackedEnemyScript.getTransform().position - transform.position);
			Debug.Log("Distancia al enemigo "+distanceToEnemy);
			if (meleeAttackRange > distanceToEnemy) {
				isAttacking=true;
			}else{
				approachToTarget();
			}
			if (isAttacking) {
				attack();
			}
		}
	}

	public void setAttackingEnemy(enemyStatusGUI aesIn){
		attackedEnemyScript=aesIn;						//Funcion que recibe el componente "enemyStatusScript", para que el personaje principal pueda atacar al enemigo
	}

	public void approachToTarget(){
		Debug.Log("acercandose al enemigo...");
		//Acerca automaticamente el jugador al enemigo
		if(!anim.IsPlaying ("run"))anim.Play("run");

		transform.position = Vector3.MoveTowards (transform.position,
		                                          new Vector3(attackedEnemyScript.getTransform().position.x,
													            transform.position.y,
													            attackedEnemyScript.getTransform().position.z),
		                                          moveSpeed * Time.deltaTime);
	}
	
	public void setIsTargetSelected(bool itsIn){
		isTargetSelected = itsIn;
	}

	void attack(){
		if(Time.time - attackCDTimer > attackCoolDown) {  // espera entre ataques 
			//ataca
			attackedEnemyScript.curHP -= meleeDamage;
			if(!anim.IsPlaying ("attack"))anim.Play("attack");
			attackCDTimer = Time.time;
		}
	}

	
	public void die(){
		Debug.Log ("jugador: me muero....");
		if(!anim.IsPlaying ("die"))anim.Play("die");
	}

	public enemyStatusGUI getAttackedEnemyScript(){
		return attackedEnemyScript;
	}
}
