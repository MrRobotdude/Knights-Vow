using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MorakMotor : MonoBehaviour
{
    public Transform target;
    float radius = 50f;
    NavMeshAgent agent;
    bool attacking;
    Animator animator;
    public float dist;
    Vector3 startPosition;
    MorakStats morakStats;
    public CharacterStats playerStats;
    Animator playerAnimator;
    public float roamDist;
    public float attDist;
    bool isHurted;
    Vector3 movementDirection;
    private readonly float directionChangeTime = 3f;
    private float latestDirectionChangeTime;
    private float velocity = 2f;
    private Vector3 movementPerSecond;
    Vector3 Respawn;
    bool hittable = false;
    bool playerHittable = false;
    public GameObject sword;
    bool flag = false;
    public HealthAndExp playerStatus;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        morakStats = GetComponent<MorakStats>();
        playerStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
        playerAnimator = PlayerManager.instance.player.GetComponent<Animator>();
        startPosition = this.transform.position;
        attacking = false;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerStatus = GetComponent<HealthAndExp>();
        latestDirectionChangeTime = 0f;
        Direction();
        Respawn = target.position;
    }


    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(startPosition, target.position);
        roamDist = Vector3.Distance(transform.position, startPosition);
        attDist = Vector3.Distance(transform.position, target.position);
        
        if(morakStats.currentHealth <= 0)
        {
            morakStats.Die();
        }
        else if(morakStats.currentHealth > 0)
        {
            if(dist > radius)
            {
                animator.SetInteger("Move", 2);
                if (Time.time - latestDirectionChangeTime > directionChangeTime)
                {
                    latestDirectionChangeTime = Time.time;
                    Direction();
                }
                transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime), 0,
                transform.position.z + (movementPerSecond.z * Time.deltaTime));
                if(roamDist > radius)
                {
                    MoveToPoint(startPosition);
                    if(transform.position == startPosition)
                    {
                        return;
                    }
                }
                canvas.SetActive(false);
            }
            else if(dist <= radius)
            {
                canvas.SetActive(true);
                if(roamDist > radius)
                {
                    MoveToPoint(startPosition);
                    if (transform.position == startPosition)
                    {
                        return;
                    }
                }
                else
                {
                    if(attDist <= 8f)
                    {
                        animator.SetInteger("Move", 0);
                        attacking = true;
                        AttackPlayer();
                        Debug.Log("Kejar"); 
                        return;
                    }
                    else
                    {
                        
                        animator.SetInteger("Move", 1);
                        MoveToPoint(target.position);
                        if (transform.position == startPosition)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
    void Direction()
    {
        movementDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * velocity;
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius - 7f;
        agent.updateRotation = false;
        
        target = newTarget.transform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0;
        agent.updateRotation = true;
        target = null;

    }

    void AttackPlayer()
    {
        playerHittable = true;
        if (!flag)
        {
            flag = true;
            StartCoroutine(Attacking());
        }
    }

    //void faceTar()
    //{
    //    Vector3 distance = (target.position - transform.position).normalized;
    //    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(distance.x, 0, distance.z));
    //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    //}
    void Activate_attackPoint()
    {
        sword.SetActive(true);
    }

    void DeActivate_attackPoint()
    {
        if (sword.activeInHierarchy)
        {
            sword.SetActive(false);
        }
    }

    IEnumerator Attacking()
    {
        animator.SetBool("Attack", true);
        Activate_attackPoint();
        yield return new WaitForSeconds(1.8f);
        DeActivate_attackPoint();
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(3);
        flag = false;
        //if (attDist <= 7f)
        //{
        //    if(playerStats.currentHealth > 0)
        //    playerAnimator.SetTrigger("GetHit 0");
        //    else PlayerDeath();
        //    if (playerHittable)
        //    {
        //        playerStats.currentHealth -= morakStats.currentDamage;
        //        playerHittable = false;
        //    }
        //    playerAnimator.SetInteger("Move", 0);
        //    playerAnimator.SetInteger("Strafe", 0);
        //    playerAnimator.SetInteger("Jump", 0);
        //    playerAnimator.SetInteger("Attack", 0);
        //    playerStatus.SetHealth(playerStats.currentHealth);
        //    yield return new WaitForSeconds(1);
        //    yield return new WaitForSeconds(1);
        //    animator.SetBool("Attack", false);
        //    attacking = false;
        //}
        //else
        //{
        //    yield return new WaitForSeconds(2);
        //    animator.SetBool("Attack", false);
        //    attacking = false;
        //}
    }

    //void GetHit()
    //{
    //    animator.SetTrigger("isHit");
    //    hittable = true;
    //    StartCoroutine(Hurted());
    //}

    //IEnumerator Hurted()
    //{
    //    if (hittable)
    //    {
    //        morakStats.currentHealth -= playerStats.damage;
    //        hittable = false;
    //    }
    //    yield return new WaitForSeconds(2);
    //}

    //void PlayerDeath()
    //{
    //    StartCoroutine(PDeath());
    //    target.transform.position = Respawn;
    //}

    IEnumerator PDeath()
    {
        playerAnimator.SetBool("Death", true);
        yield return new WaitForSeconds(5);
    }

    void EnemyDeath()
    {
        StartCoroutine(EDeath());
    }

    IEnumerator EDeath()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(5);
    }
}
