using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morakAttackSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip AttackClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Attack()
    {
        audioSource.PlayOneShot(AttackClip);
    }
}
