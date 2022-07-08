using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Canvas endLevel;
    public Canvas mainMenu;

    public GameObject endLevelPanel;
    private Animator animator;


    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        animator = endLevelPanel.GetComponent<Animator>();
    }

    public void StartGame()
    {
        mainMenu.gameObject.SetActive(false);
    }

    public void EndLevel()
    {
        scoreText.text = "Score: " + LevelManager.Instance.score.ToString();
        endLevel.gameObject.SetActive(true);
        animator.SetBool("enabled", true);
    }

    public void NextLevel()
    {
        endLevel.gameObject.SetActive(false);
        animator.SetBool("enabled", false);
    }
}
