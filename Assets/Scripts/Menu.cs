using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject pauseMenu;//pause menu object

    private bool paused = false;//is game paused or not
    private bool isMainMenu = false;// is the game in main menu or not

    public bool IsPaused { get { return paused; } }//access the pause variable from other classes

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Check if the game is in the main menu or not
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainMenu")
        {
            isMainMenu = true;
        }
        else
        {
            isMainMenu = false;
        }

        //Pause the game if the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !isMainMenu && !GameManager.INSTNANCE.IsGameOver)
        {
            //check if the game is not already paused then pause it
            if (!paused)
            {
                pauseMenu.SetActive(true);//show the pause menu
                Time.timeScale = 0f;//freeze the game update
                paused = true;
                Cursor.visible = true;//show the cursor
                Cursor.lockState = CursorLockMode.None;//unlock the cursor preventing it from showing
            }
            else//If the game is already paused then unpause
            {
                pauseMenu.SetActive(false);//hide the pause menu
                Time.timeScale = 1f;
                paused = false;
                Cursor.visible = false;//Hide the cursor
                Cursor.lockState = CursorLockMode.Locked;//lock the cursor preventing it from showing
            }
        }
    }

    //When the start game button is pressed in the main menu
    //Start the game
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Demo");
    }

    //When the resume button is pressed
    public void Resume()
    {
        pauseMenu.SetActive(false);//hide the pause menu
        Time.timeScale = 1f;
        paused = false;
        Cursor.visible = false;//Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;//lock the cursor preventing it from showing
    }

    //When restart button is pressed
    //Reload the Scene
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Load the Main menu
    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Quit the game to the desktop
    public void Quit()
    {
        //if the game is running in the editor then stop the play mode
        //else quit the game
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

}
