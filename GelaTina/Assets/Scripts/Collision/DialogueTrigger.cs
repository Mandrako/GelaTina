using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    public string targetTag = "Player";

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == targetTag)
        {
            OnDialogue(target.gameObject);
            OnDestroy();
        }
    }

    protected virtual void OnDialogue(GameObject target)
    {

    }

    protected virtual void OnDestroy()
    {
        Destroy(gameObject);
    }
}
