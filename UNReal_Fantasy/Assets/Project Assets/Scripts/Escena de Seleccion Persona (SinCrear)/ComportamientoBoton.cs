﻿using UnityEngine;
using System.Collections;

public class ComportamientoBoton : MonoBehaviour {
	public string irA;
	public Material inactivo;
	public Material activo;
	public Material presionado;
	
	void OnMouseEnter(){
		gameObject.GetComponent<Renderer>().material = activo; 
	}
	void OnMouseExit(){
		gameObject.GetComponent<Renderer>().material = inactivo; 
	}
	void OnMouseDown(){
		gameObject.GetComponent<Renderer>().material = presionado; 
	}
	void OnMouseUp(){
		gameObject.GetComponent<Renderer>().material = activo; 
		Application.LoadLevel (irA);
	}
}
