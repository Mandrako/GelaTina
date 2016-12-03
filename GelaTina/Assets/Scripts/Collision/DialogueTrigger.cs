using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public string targetTag = "Player";
    public float displayTime = 3.0f;
    public string[] texts;

    private Canvas _targetCanvas;
    private Text _targetTextField;

    void OnTriggerEnter2D(Collider2D target)
    {
        var trigger = GetComponent<Collider2D>();
        trigger.enabled = false;

        _targetCanvas = target.GetComponentInChildren<Canvas>(true);
        _targetTextField = target.GetComponentInChildren<Text>(true);

        if (target.gameObject.tag == targetTag)
        {
            if(_targetCanvas != null && _targetTextField != null)
            {
                StartCoroutine(OnDialogue(target.gameObject));
            }
        }
    }

    protected virtual IEnumerator OnDialogue(GameObject target)
    {
        _targetCanvas.gameObject.SetActive(true);
        int i = 0;

        while(i < texts.Length)
        {
            _targetTextField.text = texts[i];
            i++;
            yield return new WaitForSeconds(displayTime);
        }

        OnDestroy();
    }

    protected virtual void OnDestroy()
    {
        _targetCanvas.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
