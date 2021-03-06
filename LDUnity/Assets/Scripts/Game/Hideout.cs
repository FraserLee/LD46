﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EHideoutType
{
    Barrel,
    Box,
    Bush,
    Closet,
    Sewer,
}

public class Hideout : MonoBehaviour, IInteractable
{
    [SerializeField]
    EHideoutType Type;

    [SerializeField]
    Sprite InteractIcon;
    //public Corpse currentCorpse;
    
    [SerializeField]
    int SpriteVisualID = 0;
    
    [SerializeField]
    Sprite[] SpriteVisuals = new Sprite[4];    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = SpriteVisuals[SpriteVisualID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        CharacterPlayer.instance.ToggleHiding();
    }

    public Sprite GetInteractIcon()
    {
        return InteractIcon;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public EPlayerAction GetPlayerActionType()
    {
        return EPlayerAction.Hide;
    }

    public int GetStreetSpriteSortIndex()
    {
        StreetSpriteSort sort = GetComponent<StreetSpriteSort>();

        if (sort != null)
        {
            return sort.street;
        }
        else
        {
            return 0;
        }
    }
}
