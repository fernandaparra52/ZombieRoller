using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public GameManager gameManager;
    public AudioClip hit;
    AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        gameManager.AddPoints();
        source.PlayOneShot(hit);
    }
}
