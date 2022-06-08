using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyController : PersonageController
{
    public NavMeshAgent navMeshAgent;
    public PlayerController target;
    public float rangeAttack = 1;
    public Collider2D Collider2D;

    private void Start()
    {
        target = FindObjectOfType<PlayerController>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        time = Time.time;
        Collider2D.enabled = false;

    }

    float time;
    float timeAttack;

    private void Update()
    {
        if (Time.time - time < .7f) return;

        if (Personage.isDeath || target.Personage.isDeath)
        {
            navMeshAgent.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            AnimationController.Death();
            return;
        }
        float range = (transform.position - target.transform.position).magnitude;
        // Debug.Log(range);

        if (range < rangeAttack)
        {
            if ((Time.time - timeAttack) > Personage.State.DamageSpeed)
            {
                AnimationController.Attack(Vector3.zero, new UnityAction(EndAttack));
                Collider2D.enabled = true;
                timeAttack = Time.time;
            }
        }

        //var vector = transform.position - navMeshAgent.nextPosition;
        //var direction = vector / (vector.magnitude);
        //Debug.Log( navMeshAgent.steeringTarget);
        //if (direction.x != 0)
        AnimationController.Walk(navMeshAgent.velocity.normalized.x > 0);
        //transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        navMeshAgent.SetDestination(target.transform.position);
        if (navMeshAgent.path.corners.Length > 0)
            Navigate.DebugDrawPath(navMeshAgent.path.corners);
    }

    public void EndAttack()
    {
        Collider2D.enabled = false;

    }

    public void AttackPlayer()
    {
        FindObjectOfType<PlayerController>().Damage(Personage.State.Damage);
        Collider2D.enabled = false;

    }
}

