using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public PlayerMovement playerMovement;
    public StackBouncing stackBouncing;

    public int score;
    public int currentLevel;

    public GameObject currentLevelMap;

    public GameObject[] levelMapReference;

    public static LevelManager Instance { get; private set; }

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

        player = FindObjectOfType<Player>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        stackBouncing = FindObjectOfType<StackBouncing>();
    }

    public void FirstLevel()
    {
        score = 1;
        currentLevel = 1;
        currentLevelMap = Instantiate(levelMapReference[currentLevel - 1]);
    }

    public void EndLevel()
    { 
        UIManager.Instance.EndLevel();
    }

    public void RestartLevel()
    {
        Destroy(currentLevelMap);
        currentLevelMap = Instantiate(levelMapReference[currentLevel - 1]);

        UIManager.Instance.NextLevel();
        player.ResetPlayer();
    }

    public void NextLevel()
    {
        Destroy(currentLevelMap);
        if (currentLevel < levelMapReference.Length)
        {
            currentLevel++;
        }
        currentLevelMap = Instantiate(levelMapReference[currentLevel - 1]);

        UIManager.Instance.NextLevel();
        player.ResetPlayer();
    }
}
