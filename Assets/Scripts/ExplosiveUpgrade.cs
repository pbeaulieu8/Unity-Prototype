using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveUpgrade : MonoBehaviour
{
    public AudioClip audioClip;

    void OnTriggerEnter2D(Collider2D other) {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null) {
            controller.GetExplosive();
            controller.PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
