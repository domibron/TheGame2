using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    public float Health = 50f;
    bool tookDamage = false;
    bool dead = false;
    public float damage = 30f;
    public Transform zombie;
    float nextTimeToAttack = 0f;
    public float damageRate = 1000000f;

    public void TakeDamage(float ammount)
    {
        Health -= ammount;
        tookDamage = true;
        if (Health <= 0)
        {
            aiState = AIState.dead;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    RaycastHit hit;
    float range = 0.9f;

    IEnumerator Attack()
    {
        while (true)
        {
            animator.SetBool("Attack", false);

            if (Physics.Raycast(zombie.transform.position, zombie.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Scr_Movement_player target = hit.transform.GetComponent<Scr_Movement_player>();
                if (target != null && Time.time >= nextTimeToAttack)
                {
                    nextTimeToAttack = Time.time + (30f / damageRate);
                    float y = damage / 2f;
                    target.ReciveDamage(y);
                    animator.SetBool("Attack", true);
                }

            }
            yield return new WaitForSeconds(1f);
        }
        
    }

    void Damage()
    {
        if (Physics.Raycast(zombie.transform.position, zombie.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Scr_Movement_player target = hit.transform.GetComponent<Scr_Movement_player>();
            if (target != null)
            {
                StartCoroutine(Attack());
            }
        }
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
        StartCoroutine(Attack());
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
                    Die();
                    break;


                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }


}
