using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    public float Health = 50f;
    public float damage = 60f;
    public Transform zombie;
    float nextTimeToAttack = 2f;
    public float damageRate = 40f;
    public CapsuleCollider zombieCollider;
    NavMeshAgent nm;
    public Transform target;
    public float distanceThreshold = 1000000f;
    public enum AIState { idle, chasing, dead };
    public AIState aiState = AIState.chasing;
    public Animator animator;

    //when the player enters the zombie collider then the zombie will attack
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(collision);
        }
    }

    //when the player stays in the zombie collider then it will attack the player
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Damage(collision);
        }
    }

    void Start()
    {
        //this sents the nessary information and starts the corutine Think
        nm = GetComponent<NavMeshAgent>();
        StartCoroutine(Think());
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nm.speed = Random.Range(3f, 6f);
    }

    //runs the ai brains
    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                    //when the ai is close then it will run/walk towards the player
                case AIState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceThreshold)
                    {

                        aiState = AIState.chasing;
                        animator.SetBool("Chasing", true);

                    }
                    nm.SetDestination(transform.position);
                    aiState = AIState.chasing;
                    animator.SetBool("Chasing", true);
                    nm.SetDestination(target.position);
                    break;

                    //when ai is far then they will go into idle
                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > distanceThreshold)
                    {

                        aiState = AIState.idle;
                        animator.SetBool("Chasing", false);

                    }
                    nm.SetDestination(target.position);
                    nm.SetDestination(GameObject.Find("Player").transform.position);
                    break;

                    //when the ai is dead a animation would play before being destoryed
                case AIState.dead:
                    animator.SetBool("Dead", true);
                    nm.SetDestination(transform.position);                    
                    Die();
                    break;


                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    //when called the zombie will take ammount out of their health
    public void TakeDamage(float ammount)
    {
        Health -= ammount;
        if (Health <= 0)
        {
            aiState = AIState.dead;
        }
    }

    //when called the zombie will be destroyed
    void Die()
    {
        Destroy(gameObject, 0.1f);
    }

    //when called if the collision is a player then they will run the recive damage funtion.
    void Damage(Collision collision)
    {
        if (Time.time >= nextTimeToAttack)
        {
            Scr_Movement_player scrPlayer; 
            nextTimeToAttack = Time.time + 2000000f / damageRate;
            scrPlayer = collision.gameObject.GetComponent<Scr_Movement_player>();
            scrPlayer.ReciveDamage(30f);
        }
    }
}
