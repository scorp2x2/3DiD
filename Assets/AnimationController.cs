using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationController : MonoBehaviour
{
    public SkeletonAnimation skeletonGraphic;
    public StatePersonage State;
    public void Idle()
    {
        if (State == StatePersonage.death) return;
        //if(skeletonGraphic.ani.)
        if (State != StatePersonage.idle && State != StatePersonage.damage)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "Покой", true);
            State = StatePersonage.idle;
        }
    }

    public void Walk(bool direction)
    {
        if (State == StatePersonage.death) return;

        var scale = skeletonGraphic.transform.localScale;
        if (direction)
            skeletonGraphic.transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        else
            skeletonGraphic.transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);

        if (State != StatePersonage.walk && State != StatePersonage.damage)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "Ходьба", true);
            State = StatePersonage.walk;
        }

    }

    public void Attack(Vector3 direction, UnityAction unityEvent = null)
    {
        if (State == StatePersonage.damage || State == StatePersonage.death) return;

        Debug.Log("Attack");
        skeletonGraphic.AnimationState.SetAnimation(0, "Атака", false);

        if (direction == Vector3.zero)
        {
            if (transform.localScale.x < 0)
                direction.x = -1;
            else
                direction.x = 1;

        }

        skeletonGraphic.AnimationState.Complete += entry =>
        {
            if (State == StatePersonage.idle)
                skeletonGraphic.AnimationState.SetAnimation(0, "Покой", true);

            else
                skeletonGraphic.AnimationState.SetAnimation(0, "Ходьба", true);

            unityEvent?.Invoke();
        };
    }

    public void Damage()
    {
        if (State == StatePersonage.death) return;

        State = StatePersonage.damage;

        skeletonGraphic.AnimationState.SetAnimation(0, "Урон", false);

        skeletonGraphic.AnimationState.Complete += entry =>
        {
            if (State == StatePersonage.death) return;

            skeletonGraphic.AnimationState.SetAnimation(0, "Покой", true);
            State = StatePersonage.idle;

        };
    }

    public void Death()
    {
        if (State != StatePersonage.death)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "Смерть", false);
            State = StatePersonage.death;
            Debug.Log("Death");
        }

    }
}

public enum StatePersonage
{
    idle,
    walk,
    damage,
    death
}