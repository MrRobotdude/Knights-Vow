using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{

    public int Level;
    public Stat defaultDamage;
    public Stat defaultExp;
    public Stat defaultHealth;
    public int damage { get; private set; }
    public int maxHealth { get; private set; }
    public int Exp { get; private set; }
    public static int currentHealth;
    public int currentExp;
    public int currentDamage { get; private set; }
    public Animator anim;
    public int potion;
    PlayerData data;
    public GameObject dead;
    public GameObject blueSpell;
    public GameObject bloodSPray;

    [SerializeField]
    private AudioClip levelUpClip;

    [SerializeField]
    private AudioClip deadClip;

    [SerializeField]
    private AudioClip bloodClip;

    private AudioSource source;

    public HealthAndExp healthAndExp;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        
    }

    private void Awake()
    {
        maxHealth = defaultHealth.GetValue() * Level;
        currentHealth = maxHealth;
        Exp = Exp + defaultExp.GetValue() * Level;
        currentExp = 0;
        damage = defaultDamage.GetValue() + 2 * Level;
        currentDamage = damage;
        potion = 30;

        if (MainMenu.Load)
        {
            data = SaveSystem.LoadPlayer();
            Level = data.level;
            currentHealth = data.health;
            maxHealth = defaultHealth.GetValue() * Level;
            currentExp = data.exp;
            Exp = 0;
            Exp = Exp + defaultExp.GetValue() * Level;
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
            damage += defaultDamage.GetValue() + 2 * Level;
            currentDamage = damage;

        }

    }

    private void Update()
    {
        currentDamage = StaticStat.CurrDamage;
        currentExp = StaticStat.CurrExp;
        Exp = StaticStat.MaxExp;
        maxHealth = StaticStat.MaxHealth;
        Level = StaticStat.Level;

        if (currentHealth <= 0)
        {
            Die();
        }


        if (currentExp >= Exp || Input.GetKeyDown(KeyCode.Z))
        {
            Level++;
            source.PlayOneShot(levelUpClip);
            Exp += (defaultExp.GetValue() * Level);
            currentExp = 0;
            maxHealth += (defaultHealth.GetValue() * Level);
            currentHealth = maxHealth;
            Instantiate(blueSpell, transform.position, Quaternion.identity);

        }
        usePotion();
        healthAndExp.SetMaxHealth(maxHealth);
        healthAndExp.SetMaxExp(Exp);
        healthAndExp.SetHealth(currentHealth);
        healthAndExp.SetExp(currentExp);

        StaticStat.CurrDamage = currentDamage;
        StaticStat.CurrExp = currentExp;
        StaticStat.MaxExp = Exp;
        StaticStat.MaxHealth = maxHealth;
        StaticStat.Level = Level;
        StaticStat.currHealth = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damage;
            anim.SetTrigger("GetHit 0");
            Instantiate(bloodSPray, transform.position, Quaternion.identity);
            source.PlayOneShot(bloodClip);
        }
    }

    void Die()
    {
        anim.SetBool("Death" , true);
        currentHealth = 0;
        StartCoroutine(waitDie());
    }

    IEnumerator waitDie()
    {
        yield return new WaitForSeconds(1);
        dead.SetActive(true);
        source.PlayOneShot(deadClip);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
   
    void usePotion()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (potion <= 0)
            {

            }
            else
            {
                potion -= 1;
                currentHealth += 20;
                if (currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }
    }

}
