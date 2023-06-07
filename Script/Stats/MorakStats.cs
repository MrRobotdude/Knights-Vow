using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorakStats : MonoBehaviour
{
    public int Level;
    public Stat defaultDamage;
    public Stat defaultHealth;
    public int damage { get; private set; }
    public int maxHealth { get; private set; }
    public int currentHealth;
    public int currentDamage { get; private set; }
    public Animator anim;
    public CharacterStats player;
    public GameObject morak;
    Vector3 startPosition;
    int flag = 0;
    public GameObject spawnParticle;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private AudioClip bloodClip;

    public GameObject bloodSPray;
    private AudioSource source;
    

    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        player = PlayerManager.instance.player.GetComponent<CharacterStats>();
        startPosition = morak.transform.position;
    }

    private void Awake()
    {
        int minLevel;
        int maxLevel = player.Level + 2;
        if(player.Level < 3)
        {
            minLevel = 1;
        }
        else
        {
            minLevel = player.Level - 2;
        }
        Level = Random.Range(minLevel, maxLevel);
        for(int i = 1; i <= Level; i++)
        {
            int temp = defaultHealth.GetValue() * i;
            maxHealth += temp;
        }
        currentHealth = maxHealth;
        for(int i = 1; i <= Level; i++)
        {
            int temp = defaultDamage.GetValue() * i;
            damage += temp;
        }
        
        currentDamage = damage;
        Instantiate(spawnParticle, transform.position, Quaternion.identity);
        source.PlayOneShot(clip);
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
            currentHealth = 0;

        }
    }

    IEnumerator respawnStart()
    {
        Debug.Log("ded");
        Instantiate(spawnParticle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        //source.PlayOneShot(clip);
        yield return new WaitForSeconds(1);
        Debug.Log("mati");
        Destroy(gameObject);
    }

    //public void respawn()
    //{
    //    Instantiate(morak, startPosition, Quaternion.identity);
    //}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("isHit");
        Instantiate(bloodSPray, transform.position, Quaternion.identity);
        source.PlayOneShot(bloodClip);
    }

    public void Die()
    {
        anim.SetBool("Death", true);
        if(flag == 0)
        {
            Spawner.flag = true;
            flag = 1;
            PlayerManager.instance.player.GetComponent<HealthAndExp>().SetExp(PlayerManager.instance.player.GetComponent<CharacterStats>().currentExp += 9 * Level);
        }
        StartCoroutine(respawnStart());
    }
}
