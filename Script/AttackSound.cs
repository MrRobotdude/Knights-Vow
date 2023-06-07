using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip Attack1Clip;
    [SerializeField]
    private AudioClip Attack2Clip;
    [SerializeField]
    private AudioClip Attack3Clip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Attack1()
    {
        audioSource.PlayOneShot(Attack1Clip);
    }

    private void Attack2()
    {
        audioSource.PlayOneShot(Attack2Clip);
    }

    private void Attack3()
    {
        audioSource.PlayOneShot(Attack3Clip);
    }
}
