using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public PlayerManager player;

    public GoalScript goal;
    public bool isGoalReached = false;

    public bool levelWon = false;
    public bool gameOver = false;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameWonText;


    
    void Start()
    {
        player = GameObject.Find("knight").GetComponent<PlayerManager>();
        goal = GameObject.Find("Goal").GetComponent<GoalScript>();

        goal.OnGoalTrigger += ToggleWin;
    }

    
    void Update()
    {
        UpdateGameManager();
    }

    private void OnDestroy()
    {
        goal.OnGoalTrigger -= ToggleWin;
    }

    public void UpdateGameManager()
    {
        CheckGameStatus();
        UpdateGameUI();
        TryRestartAfterWon();
    }

    public void ToggleWin(bool flag)
    {
        isGoalReached = flag;
    }

    public void CheckGameStatus()
    {
        if(player.isDead)
        {
            gameOver = true;
        }
        else if(isGoalReached)
        {
            levelWon = true;
        }
    }

    public void UpdateGameUI()
    {
        if(gameOver)
        {
            gameOverText.gameObject.SetActive(true);
        }
        else if(isGoalReached) 
        {
            gameWonText.gameObject.SetActive(true);
        }
    }

    public void TryRestartAfterWon()
    {
        if ((gameOver || isGoalReached) && Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
