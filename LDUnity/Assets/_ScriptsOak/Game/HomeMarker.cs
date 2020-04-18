﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeMarker : MonoBehaviour, IInteractable
{
    [SerializeField]
    Sprite IconInteract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Player activated Home Marker: " + this);

        CharacterPlayer.instance.DropOffCorpseAtHome();
    }

    public Sprite GetInteractIcon()
    {
        return IconInteract;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
