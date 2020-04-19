﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayer : Character
{
    public static CharacterPlayer instance;

    private Corpse currentCorpse;

  	private StreetSpriteSort sss;

    [SerializeField]
    SpriteRenderer TooltipRenderer;


    bool bIsHiding = false;

    protected void Start()
    {
        instance = this;
        sss = GetComponent<StreetSpriteSort>();
        StreetSpriteSort.PlayerStreetSwapp(sss.street);
        base.Start();
    }
    public override void MoveCharacter()
    {
        base.MoveCharacter();
    }

    public override void InitCharacter()
    {

    }
    public void TransitionToStreet(Alley alley)
    {
    	// var delta = transform.position;

        gameObject.transform.SetParent(alley.GetTargetAlley().GetCurrentStreet().gameObject.transform);
        Vector3 temp = alley.GetTargetAlley().gameObject.transform.localPosition;
        temp.y = alley.GetCurrentStreet().StreetYOffset;
        sss.street = alley.GetTargetAlley().GetCurrentStreet().streetID;
        if(currentCorpse!=null)currentCorpse.gameObject.GetComponent<StreetSpriteSort>().street = sss.street;
        gameObject.transform.localPosition = temp;

        StreetSpriteSort.PlayerStreetSwapp(sss.street);

        // delta -= transform.position;
        // SmoothCamera.targetPosition.x-=delta.x;
    }

    IInteractable CurrentClosestInteractable = null;

    public override void Tick()
    {

        IInteractable closestInteractable = EntityManager.Instance.GetClosestInteractableWithinRange(gameObject.transform.position);


        // if (CurrentClosestInteractable != closestInteractable && closestInteractable != null)
        // {
        //     Debug.Log("Player is near interactable: " + closestInteractable);
        // }

        CurrentClosestInteractable = closestInteractable;

        if(CurrentClosestInteractable != null)
        {
            TooltipRenderer.sprite = CurrentClosestInteractable.GetInteractIcon();
        }
        else
        {
            TooltipRenderer.sprite = null;
        }
    }


    public void SetCurrentCorpse(Corpse corpse)
    {
        currentCorpse = corpse;
    }

    public void DropCorpse()
    {
        if(currentCorpse != null)
        {
            currentCorpse.transform.SetParent(gameObject.transform.parent);
            currentCorpse = null;
        }
    }
    public void TryInteract()
    {
        //IInteractable closestInteractable = EntityManager.Instance.GetClosestInteractableWithinRange(gameObject.transform.position);

        if (CurrentClosestInteractable != null)
        {
            CurrentClosestInteractable.Interact();
        }

        //Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1.0f);
        //List<IInteractable> interactables = new List<IInteractable>();
        //Corpse corpse = null;
        //// gather all interactables
        //for (int i = 0; i < colliders.Length; ++i)
        //{
        //    IInteractable interactable = colliders[i].gameObject.GetComponent<IInteractable>();
        //    corpse = colliders[i].gameObject.GetComponent<Corpse>();

        //    //if corpse found, interact with that
        //    if(corpse != null)
        //    {
        //        corpse.Interact();
        //        return;
        //    }
        //    if (interactable != null)
        //    {
        //        interactables.Add(interactable);
        //    }
        //}

        //if(interactables.Count > 0)
        //{
        //    Debug.Log("Player is gonna interact with: " + interactables[0]);
        //    interactables[0].Interact();
        //}
    }

    public Corpse GetCurrentCorpse()
    {
        return currentCorpse;
    }

    //public void TryHandleCorpse()
    //{
    //    if (currentCorpse != null)
    //    {
    //        //find a hideout if available.

    //        Hideout hideout = EntityManager.Instance.GetCorpseHideoutWithinRange(this.transform.position, false);

    //        if (hideout != null)
    //        {
    //            hideout.currentCorpse = currentCorpse;
    //            hideout.currentCorpse.isHidden = true;
    //            currentCorpse.transform.SetParent(hideout.transform);
    //            currentCorpse = null;
    //        }
    //    }
    //    else
    //    {
    //        //check if there is a corpse.
    //        Corpse corpse = EntityManager.Instance.GetCorpseWithinRange(this.transform.position);
    //        if (corpse != null)
    //        {
    //            if (currentCorpse != null)
    //            {
    //                throw new NotImplementedException("Holding already a Corpse");
    //            }

    //            //check the nearest hideout, maybe we picked the corpse just from this hideout.
    //            var corpseHideout = EntityManager.Instance.GetCorpseHideoutWithinRange(this.transform.position, true);
    //            if (corpseHideout != null && corpseHideout.currentCorpse == corpse)
    //            {
    //                corpseHideout.currentCorpse = null;
    //            }

    //            corpse.isHidden = false;
    //            currentCorpse = corpse;

    //            // using parenting here for moving corpse.
    //            // might be suboptimal for animation.
    //            currentCorpse.transform.SetParent(this.transform);
    //        }
    //    }

    //}

    public void DropOffCorpseAtHome()
    {
        if (currentCorpse != null)
        {
            Destroy(currentCorpse.gameObject);
        }
    }

    public bool IsHiding()
    {
        return bIsHiding;
    }

    public void ToggleHiding()
    {
        bIsHiding = !bIsHiding;
        if (bIsHiding)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
