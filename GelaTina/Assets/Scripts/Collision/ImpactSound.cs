using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    public float velocityThreshold = 125;
    public AudioClip[] clips;

    private AudioSource _source;

    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (clips.Length == 0 || coll.relativeVelocity.magnitude < velocityThreshold)
            return;

        if(coll.gameObject.layer == LayerMask.NameToLayer("Solid") && clips.Length > 0)
        {
            var randomIndex = Random.Range(0, clips.Length);

            _source.clip = clips[randomIndex];

            _source.Play();
        }
    }
}
