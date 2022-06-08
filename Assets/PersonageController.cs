using Assets.Scripts;
using System;
using UnityEngine;

public class PersonageController : MonoBehaviour
{
    public Transform centerPosition;
    public AnimationController AnimationController;
    public PersonageSave Personage;
    public Transform startShell;


    public void DamageTrap(Vector3 position, int damage)
    {
        var heading = centerPosition.position - position;
        var direction = heading / heading.magnitude;

        Debug.Log(position + " => " + centerPosition.position + " => " + direction);
        AnimationController.Damage();
        if (!Personage.Damage(damage))
        {
            AnimationController.Death();
            return;
        }
        MoveVertical(direction.x);
        MoveHorizontal(direction.y);
    }

    public virtual void MoveHorizontal(float y)
    {
    }

    public virtual void MoveVertical(float x)
    {
    }

    public virtual void Attack()
    {

    }

    public void Damage(int damage)
    {
        if (!Personage.Damage(damage))
            AnimationController.Death();
        else
            AnimationController.Damage();
    }
}
