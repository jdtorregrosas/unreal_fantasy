﻿using UnityEngine;
using System.Collections;

public class barraProgreso : MonoBehaviour {
	public GUIStyle barraProgresoStyle;
	public Texture2D barraFondo;
	public Texture2D barraFrente;
	public float progreso= 0f;
	public float velocidadProgreso=0f;

	void Start(){
	}

	void Update(){
		if (progreso < Screen.width / 5 - 2) {
			progreso += Time.deltaTime * velocidadProgreso;
		} else {
			Application.LoadLevel("MainWorld");
		}
	}

	void OnGUI(){
		GUI.BeginGroup (new Rect (Screen.width*2/5,Screen.height*4/5, Screen.width/5, Screen.height/12));
		GUI.Box (new Rect (0, 0, Screen.width/5, Screen.height/10), barraFondo, barraProgresoStyle);
		GUI.EndGroup ();
		GUI.BeginGroup (new Rect (Screen.width*2/5,Screen.height*4/5, progreso, Screen.height/12));
		GUI.Box (new Rect (2, 2, Screen.width/5, Screen.height/12), barraFrente, barraProgresoStyle);
		GUI.EndGroup ();
	}
}
