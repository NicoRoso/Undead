using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pausaMenuUI;
    public int indexLevel;

    // Start is called before the first frame update
    private void Start()
    {
        GameIsPaused = false;
    }

    // Update is called once per frame
    public void Resume()
    {
        pausaMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.visible = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(indexLevel);
    }

    public void Pause_menu()
    {
        pausaMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
