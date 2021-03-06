﻿using UnityEngine;
using System.Collections;

public class playerStatusGUI : MonoBehaviour {

	
	// texturas
	public Texture2D healthBackground; 
	public Texture2D healthForeground; 
	public Texture2D healthDamage; 
	public Texture2D closeBtn;
	public Texture2D statusLayoutBg; // fondo del layout para estatus
	
	//interfaz
	public int layoutWidth=300;
	public int layoutHeight=100;

	public string playerName="test";

	public GUIStyle style;
	
	private int anchorX = 0;
	private int anchorY = 0;
	public bool showStatus=true;
	  
	private float previousHealth; 
	private float healthBar; 
	private float myFloat; 

	//Valores de estado del personaje
	public static float maxHP=5000; 
	private float curHP=0; 
	private int experience=0;
	private int level=1;
	
	
	void Awake () {
		curHP=maxHP;
		previousHealth = maxHP; // assign the empty value to store the value of max health
		healthBar = layoutWidth*0.8f; // create the health bar width
		anchorX = (int)(layoutWidth * 0.1);
		anchorY = (int)(layoutWidth * 0.05);
		myFloat = (maxHP / 100) * 10; // affects the health drainage

	}
	
	void Update(){
		adjustCurrentHealth();
	}
	
	public void adjustCurrentHealth(){
		if(previousHealth > curHP){
			previousHealth -= ((maxHP / curHP) * (myFloat)) * Time.deltaTime; // deducts health damage
		} else {
			previousHealth = curHP;
		}
		
		if(previousHealth < 0){
			previousHealth = 0;
		}
		
		if(curHP > maxHP){
			curHP = maxHP;
			previousHealth = maxHP;
		}
		if(curHP < 0){
			curHP = 0;
		}
	}
	
	void OnGUI () {
		
		if (showStatus) {

			float previousAdjustValue = (previousHealth * healthBar) / maxHP;
			float percentage = healthBar * (curHP / maxHP);

			//Area de HUD para estado del personaje
			GUILayout.BeginArea (new Rect(10, 10, layoutWidth, layoutHeight),"");
			//Fondo del layout
			GUI.DrawTexture (new Rect (0, 0,layoutWidth, layoutHeight), statusLayoutBg);  
			//Nombre del personaje
			GUI.Label (new Rect (anchorX,anchorY,2*layoutWidth/3,layoutHeight-anchorY),playerName,style);

			//Nivel del personaje
			GUI.Label (new Rect (2*layoutWidth/3,anchorY,layoutWidth/3,layoutHeight-anchorY),"Nivel: "+GetComponent<playerInfoGUI>().level,style);

			//Boton de cerrado
			if(GUI.Button(new Rect(layoutWidth-anchorX,anchorY, 15, 15),closeBtn)){
				hideStatus();

			}
			//Etiqueta de salud
			GUI.Label (new Rect (anchorX,3*anchorY, (healthBar * 2), 20),"Salud:",style);

			//Barra de salud
			GUI.DrawTexture (new Rect (anchorX, 4*anchorY, healthBar, 20), healthBackground);       
			GUI.DrawTexture (new Rect (anchorX, 4*anchorY, previousAdjustValue, 20), healthDamage);
			GUI.DrawTexture (new Rect (anchorX, 4*anchorY, percentage, 20), healthForeground);
			//Cantidad de salud
			GUI.Label (new Rect (anchorX, 4*anchorY, healthBar, 20),(int)(previousHealth) + "/" + maxHP.ToString (),style);
			GUILayout.EndArea ();
		} 
	}
	//Oculta el estado del jugador
	public void hideStatus(){
		showStatus = false;
	}
	//setters y getters (evitan el exceso de variables publicas)
	public float getCurrentHP(){
		return curHP;
	}
	public void setCurrentHP(float cHPIn){
		curHP=cHPIn;
	}
}
