using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    private float delta = 0.02f;
    ShellState ShellState;
    public void StartAnim(Vector3 target, ShellState shellState)
    {
        StartCoroutine(AnimationPlay(target, shellState));
        ShellState = shellState;
    }

    public IEnumerator AnimationPlay(Vector3 target, ShellState shellState)
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                shellState.Time * shellState.Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < delta)
            {
                transform.position = target;
                Destroy();
                break;
            }
            yield return new WaitForFixedUpdate();
        }

    }

    public void Destroy()
    {
        Destroy(gameObject,0.07f);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter " + collision.tag);
        if (collision.tag == "Obstacle" || collision.tag == "Wall")
        {
            Destroy();
        }

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<PersonageController>().Damage(ShellState.Damage);
        }
    }
}
