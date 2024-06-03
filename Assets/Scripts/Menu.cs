using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class Menu : MonoBehaviour
{
    Animator animator;
    public GameObject clickPrefab;
    [SerializeField] GameObject panel;
    int nextScene = 0;//So we know what scene to send the player to, game or tutorial
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame(Transform clicked)
    {
        animator.SetTrigger("Active");
        panel.SetActive(true);
        GameObject clickEffect = Instantiate(clickPrefab, clicked);
        clickEffect.transform.position = Input.mousePosition;
        if (clicked.gameObject.name=="Play")
        {
            nextScene = 2;
        }
        else
        {
            nextScene = 1;
        }
    }
    public void EnterGame()
    {
        SceneManager.LoadScene(nextScene);
    }
}