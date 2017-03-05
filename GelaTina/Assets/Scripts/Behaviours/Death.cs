using System;
using UnityEngine;

[AddComponentMenu("Behaviours/Death")]
public class Death : AbstractBehaviour {
    public GameObject deathSplashEffectPrefab;
    public Debris debris;
    public int totalDebris = 10;

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Deadly")
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        var t = transform;

        for (int i = 0; i < totalDebris; i++)
        {
            t.TransformPoint(0, -100, 0);
            var debrisClone = Instantiate(debris, t.position, Quaternion.identity) as Debris;
            var body2D = debrisClone.GetComponent<Rigidbody2D>();
            body2D.AddForce(Vector3.right * UnityEngine.Random.Range(-4000, 4000));
            body2D.AddForce(Vector3.up * UnityEngine.Random.Range(4000, 6000));
        }

        Destroy(gameObject);

        var clone = Instantiate(deathSplashEffectPrefab);
        clone.transform.position = transform.position;

        // TODO: this throws an error if the scene lacks a 'fade canvas'
        FadeManager.StartFadeOut();
    }
}
