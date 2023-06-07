using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morakDeath : MonoBehaviour
{
    [SerializeField]
    private AudioClip clips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void MorakDeath()
    {
        audioSource.PlayOneShot(clips);
    }

}