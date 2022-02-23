using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    public float Health = 50f;
    bool tookDamage = false;
    bool dead = false;

    public void TakeDamage(float ammount)
    {
        Health -= ammount;
        tookDamage = true;
        if (Health <= 0)
        {
            aiState = AIState.dead;
            //Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    NavMeshAgent nm;
    public Transform target;

    public float distanceThreshold = 20f;

    public enum AIState { idle,chasing,dead};

    public AIState aiState = AIState.idle;

    public Animator animator;

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        
    }

    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                case AIState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceThreshold || tookDamage == true && dead == false)
                    {

                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);
                        tookDamage = false;

                    }
                    nm.SetDestination(transform.position);
                    break;


                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > distanceThreshold && dead == false)
                    {

                        aiState = AIState.idle;
                        animator.SetBool("Chasing", false);

                    }
                    nm.SetDestination(target.position);
                    break;


                case AIState.dead:
                    animator.SetBool("Dead", true);
                    dead = true;
                    nm.SetDestination(transform.position);
                    break;


                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }


}
