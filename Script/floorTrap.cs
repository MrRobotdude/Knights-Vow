using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorTrap : MonoBehaviour
{
    public Rigidbody floortrap;

    [SerializeField]
    private AudioClip clip;

    private AudioSource audio;

    public GameObject effect;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        floortrap.useGravity = enabled;
        audio.PlayOneShot(clip);
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
