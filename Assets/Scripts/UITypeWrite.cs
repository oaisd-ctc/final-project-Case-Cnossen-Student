using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


[RequireComponent(typeof(TMP_Text))]
public class UITypeWrite : MonoBehaviour {
    public string text;
    public float waitTime;
    public UnityEvent doneTyping;
    public TMP_Text displayText;
    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<TMP_Text>();
        StartCoroutine(writeText(waitTime));
    }

    public IEnumerator writeText(float t) {
        foreach (char x in text)
        {
            displayText.text = displayText.text + x;
            yield return new WaitForSeconds(t);
        }
        doneTyping.Invoke();
    }
}