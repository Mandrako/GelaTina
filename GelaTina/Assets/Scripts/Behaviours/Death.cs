using System;
using UnityEngine;
using System.Collections;

[AddComponentMenu("Behaviours/Death")]
public class Death : AbstractBehaviour {
    public GameObject deathSplashEffectPrefab;

    public void Die()
    {
        Destroy(gameObject);

        var clone = Instantiate(deathSplashEffectPrefab);
        clone.transform.position = transform.position;
        FadeManager.StartFadeOut();
    }
}
