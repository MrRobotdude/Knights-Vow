using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordDamage : MonoBehaviour
{
    public LayerMask layer;
    float radius = 1f;
    int damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        damage = PlayerManager.instance.player.GetComponent<PaladinController>().damage;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);
        if(hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<MorakStats>().TakeDamage(damage);
                gameObject.SetActive(false);

            }
        }
    }
}
