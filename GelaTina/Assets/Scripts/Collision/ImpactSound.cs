using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    public AudioClip[] clips;

    private AudioSource _source;

    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("coll: " + coll.gameObject.name);

        if(coll.gameObject.layer == LayerMask.NameToLayer("Solid") && clips.Length > 0)
        {
            Debug.Log("play");

            var randomIndex = Random.Range(0, clips.Length);

            _source.clip = clips[randomIndex];

            _source.Play();
        }
    }
}
