using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public LayerMask layer;
    float radius = 1f;
    public MorakStats morakStats;
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        morakStats = GetComponentInParent<MorakStats>();
    }

    // Update is called once per frame
    void Update()
    {
        damage = morakStats.currentDamage;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);
        Debug.Log("hit");

        if (hits.Length > 0)
        {
            Debug.Log("kena paladin");
            //hits[0].GetComponent<MorakStats>().TakeDamage(damage);
            hits[0].GetComponent<CharacterStats>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
