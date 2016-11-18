using UnityEngine;

public class DeathSplashEffect : MonoBehaviour
{
    void OnDestroy()
    {
        Destroy(gameObject);
    }
}
