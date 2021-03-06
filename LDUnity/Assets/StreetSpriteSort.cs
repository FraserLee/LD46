﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetSpriteSort : MonoBehaviour {
	protected SpriteRenderer sr;

	private static List<StreetSpriteSort> SSSs = new List<StreetSpriteSort>();

	public bool requireExactLayer = false;

	public Color spriteColour;
	protected bool isGrey = false;
	public float transitionFrac = 0;
	private const float transitionLength = .5f;

	private int _street;
	public int street{  
		get { return _street; }
		set { 
			_street = value; 
			Recalc();
		}
	}

	public SortLayer initLayer;
	private SortLayer _layer;
	public SortLayer layer{  
		get { return _layer; }
		set { 
			_layer = value; 
			Recalc();
		}
	}

	void Awake() { 
		sr = GetComponent<SpriteRenderer>();
		spriteColour = sr.color;
		_street = GetComponentInParent<Street>().streetID;
		_layer = initLayer;
		Recalc();
		SSSs.Add(this);
	}
	private void Recalc() {
		sr.sortingLayerID = 0;
		sr.sortingOrder = 2000*_street+100*(int)_layer+Random.Range(0,100);
	}
	private static Color grey = new Color(0,0,0,0.3f), trans = new Color(0,0,0,0);
	public static void PlayerStreetSwapp(int playerStreet){
		//garbage doesn't matter in a jam game
		foreach (var sss in SSSs) if(sss.isGrey!=(sss.isGrey = (sss.requireExactLayer?sss.street!=playerStreet:sss.street>playerStreet))) sss.transitionFrac = 1;
	}
	void Update(){
		if(transitionFrac>0){
			transitionFrac-=Time.deltaTime/transitionLength;
			if(isGrey)	sr.color = (requireExactLayer?trans:grey)*(1-transitionFrac)+spriteColour*transitionFrac;
			else		sr.color = (requireExactLayer?trans:grey)*transitionFrac+spriteColour*(1-transitionFrac);
		}
	}
}
public enum SortLayer {
	BACKGROUND, BUILDING_INTERNAL, BUILDING_MID, BUILDING_FRONT, ENVIRONMENT, OBJECTS, PLAYER, GROUND, FOREGROUND_1, FOREGROUND_2, FOREGROUND_3
}