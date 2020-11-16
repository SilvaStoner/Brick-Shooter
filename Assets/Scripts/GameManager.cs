using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    /**
     * Only single instance of this object will be created
    */
    public static GameManager INSTNANCE = null;


    public int lives = 3;
    public int score = 0;
    public int bricks;
    public int aliens;

    public Text livesText, scoreText;
    public GameObject winLosePanel, winLoseScore, winLoseTitle;

    private bool gameOver = false;
    public bool IsGameOver { get { return gameOver; } }
    
    //Create a singleton instance of the game manager object to 
    //make sure only one object is instantiated at a time
    private void Awake()
    {
        if(INSTNANCE == null)
        {
            INSTNANCE = this;
        }
        else if(INSTNANCE != this)
        {
            Destroy(gameObject);
        }
       // DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    private void Start()
    {
        Cursor.visible = false;//Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;//lock the cursor preventing it from showing

        livesText.text = "Lives: " + lives;//Set the lives to the UI when the game start
        scoreText.text = "Score: " + score;//Set the score to the UI
    }
    
    //Check if there is lives or bricks available, if not the the game is over
    private void CheckGameOver()
    {
        //check if there are bricks available, if not end game
        if (bricks < 1 && aliens < 1)
        {
            Time.timeScale = .25f;
            Invoke("WinLoseScreen", .5f);
            gameOver = true;
            winLoseTitle.GetComponent<TextMeshProUGUI>().text = "You Won";
            Debug.Log("You Won");
        }

        //Check if there is lives available, if not the end game
        if (lives < 1)
        {
            Time.timeScale = .25f;
            Invoke("WinLoseScreen", .5f);
            gameOver = true;
            winLoseTitle.GetComponent<TextMeshProUGUI>().text = "You Lose";
            Debug.Log("Game Over");
        }
    }

    private void WinLoseScreen()
    {
        livesText.gameObject.SetActive(false);//disable the lives text object
        scoreText.gameObject.SetActive(false);//disable the score text object
        winLoseScore.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        winLosePanel.SetActive(true);//show the game over menu
        Time.timeScale = 0f;//stop the game time

        Cursor.visible = true;//show the cursor
        Cursor.lockState = CursorLockMode.None;//unlock the cursor preventing it from showing
    }

    //Subtract 1 from the lives and display the remaining on the UI
    public void LoseLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        CheckGameOver();
    }

    //Add 1 lives if is less than 3
    public void AddLife()
    {
        if(lives < 3)
        {
            lives++;
            livesText.text = "Lives: " + lives;
        }
    }

    //Add to the score and display it on the UI
    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        bricks--;
        CheckGameOver();
    }

    public void AddScoreAlien(int score)
    {
        this.score += score;
        scoreText.text = "Score: " + score;
        aliens--;
        CheckGameOver();
    }
}
