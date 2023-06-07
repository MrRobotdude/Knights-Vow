using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : MonoBehaviour
{
    float speed = 10;
    float gravity = 10;
    float jumpForce = 7;
    float strafeSpeed = 5;
    float radius = 10f;
    private readonly float waitRespawn = 5f;
    private float deadInterval;
    public GameObject sword;
    public int damage;

    CharacterController controller;
    Animator animator;
    public LayerMask groundLayer;
    CapsuleCollider col;
    Rigidbody rb;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    CharacterStats characterStats;
    public Transform cube;
    public GameObject dead;
    bool moving = false;
    bool jumping = false;

    float dist;
    bool attacking;
    int load;

    float time;


    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        characterStats = PlayerManager.instance.player.GetComponent<CharacterStats>();
        cube = GetComponent<Transform>();
    }
    
    void Update()
    {
        dist = Vector3.Distance(transform.position, cube.position);
        damage = characterStats.currentDamage;
        if (PauseMenuScript.GameIsPaused)
        {
            animator.SetFloat("DirX", 0.0f);
            animator.SetFloat("DirZ", 0.0f);
            animator.SetInteger("Jump", 0);
        }
        if (!attacking && !isDead() && !PauseMenuScript.GameIsPaused)
        {
            
            jump();
            move();
            if(dist < 7f)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    SaveData();
                }
            }
            GetInput();
        }
        if (isDead())
        {
            StartCoroutine(youDied());
        }
        if(!Input.anyKey && !animator.GetBool("GetHit 0"))
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                animator.SetBool("Idle", true);
            }
            else
            {
                animator.SetBool("Idle", false);
            }
        }
        else
        {
            time = 5f;
            animator.SetBool("Idle", false);
        }
    }

    IEnumerator youDied()
    {
        yield return new WaitForSeconds(1.17f);
        dead.SetActive(true);
    }

   
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
    void Activate_AttackPoint3()
    {
        
        damage *= 2;
        sword.SetActive(true);
    }

    void Deactive_AttackPoint3()
    {
        damage /= 2;
        if (sword.activeInHierarchy)
        {
            sword.SetActive(false);

        }
    }

    void GetInput()
    {
        //if (controller.isGrounded){
            if (Input.GetMouseButtonDown(0))
            {
                Activate_attackPoint();
                Attack();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                animator.SetInteger("Attack", 0);
            }
        //}
    }

    public void getLoadStatus(int load)
    {
        this.load = load;
    }

    public void SaveData()
    {
        SaveSystem.SavePlayer(characterStats);
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        characterStats.Level = data.level;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }

    void Attack()
    {
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking()
    {
        attacking = true;
        animator.SetInteger("Attack", 1);
        yield return new WaitForSeconds(1.033f);
        DeActivate_attackPoint();
        if (Input.GetMouseButtonDown(0))
        {
            Activate_attackPoint();
            yield return new WaitForSeconds(0.433f);
            DeActivate_attackPoint();
            if (Input.GetMouseButtonDown(0))
            {
                Activate_AttackPoint3();
                yield return new WaitForSeconds(2.133f);
            }

        }
        animator.SetInteger("Attack", 0);
        attacking = false;
        Deactive_AttackPoint3();
    }

    void move()
    {
        if (!attacking)
        {
            animator.SetFloat("DirX", Input.GetAxis("Horizontal"));
            animator.SetFloat("DirZ", Input.GetAxis("Vertical"));
            moving = true;
        }
    }

    bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.center.y, col.bounds.center.z), col.radius * .9f, groundLayer);
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            StartCoroutine(Jumping());
        }
    }
    
    IEnumerator Jumping()
    {
        jumping = true;
        animator.SetInteger("Jump", 1);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.53f);
        animator.SetInteger("Jump", 0);
        jumping = false;
    }

    bool isDead()
    {
        if (animator.GetBool("Death") == true)
            return true;
        else return false;
    }




    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
