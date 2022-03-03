using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    public float Health = 50f;
    //bool tookDamage = false;
    //bool dead = false;
    public float damage = 60f;
    public Transform zombie;
    float nextTimeToAttack = 2f;
    public float damageRate = 40f;
    public CapsuleCollider zombieCollider;
    //Scr_Movement_player scrPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(collision);
        }
    }



    NavMeshAgent nm;
    public Transform target;

    public float distanceThreshold = 0f;

    public enum AIState { idle,chasing,dead};

    public AIState aiState = AIState.chasing;

    public Animator animator;

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
        //scrPlayer = this.GetComponentInParent<Scr_Movement_player>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nm.speed = Random.Range(3f, 6f);
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
                    /*
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceThreshold || tookDamage == true && dead == false)
                    {

                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);
                        tookDamage = false;

                    }
                    nm.SetDestination(transform.position);
                    break;
                    */
                    aiState = AIState.chasing;
                    animator.SetBool("Chasing", true);
                    nm.SetDestination(target.position);
                    break;

                case AIState.chasing:
                    /*
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > distanceThreshold && dead == false)
                    {

                        aiState = AIState.idle;
                        animator.SetBool("Chasing", false);

                    }
                    nm.SetDestination(target.position);
                    break;
                    */
                    nm.SetDestination(GameObject.Find("Player").transform.position);
                    break;

                case AIState.dead:
                    animator.SetBool("Dead", true);
                    //dead = true;
                    nm.SetDestination(transform.position);                    
                    Die();
                    break;


                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void TakeDamage(float ammount)
    {
        Health -= ammount;
        //tookDamage = true;
        if (Health <= 0)
        {
            aiState = AIState.dead;
        }
    }

    void Die()
    {
        Destroy(gameObject, 0.1f);
    }

    void Damage(Collision collision)
    {
        if (Time.time >= nextTimeToAttack)
        {
            Scr_Movement_player scrPlayer; // = this.GetComponentInParent<Scr_Movement_player>();
            nextTimeToAttack = Time.time + 2000000f / damageRate;
            scrPlayer = collision.gameObject.GetComponent<Scr_Movement_player>();
            scrPlayer.ReciveDamage(30f);
        }
    }
}
