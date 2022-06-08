using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDataScript
{
    public bool isUse;
}

public class TrapController : MonoBehaviour
{
    public SkeletonAnimation skeletonGraphic;
    public MeshRenderer MeshRenderer;
    bool isUse;
    public int Damage = 50;


    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer.enabled = false;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    int countContacts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUse) return;

        countContacts++;
        if(countContacts==1)
        {
            MeshRenderer.enabled = true;

        }
        if (countContacts == 2)
        {
            StartTrap(collision.gameObject);
        }
    }

    private void StartTrap(GameObject gameObject)
    {
        skeletonGraphic.AnimationState.SetAnimation(1, "animation", false);
        gameObject.GetComponent<PersonageController>()?.DamageTrap(transform.position, Damage);
        enabled = false;
        isUse = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isUse) return;

        countContacts--;

        if(countContacts==0)
            MeshRenderer.enabled = false;

    }
}
