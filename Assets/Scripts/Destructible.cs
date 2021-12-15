using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    void OnCollisionEnter2D(Collision2D other) {
        //layer 14 is explosive projectile layer
        if(other.gameObject.layer == 14) {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
