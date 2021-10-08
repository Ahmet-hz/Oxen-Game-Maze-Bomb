using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int levelGame;
    public GameObject winPanel;

    AiPlayer aiKarakter;


    //private void Start()
    //{
    //    PlayerPrefs.DeleteAll();
    //}

    private void Start()
    {
        aiKarakter = FindObjectOfType<AiPlayer>();

        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);

            print(111);
        }
        else
        {
            int buildLevel = PlayerPrefs.GetInt("Level");
            levelGame = 1 + buildLevel;
            if (levelGame >= 11)
            {
                levelGame = Random.Range(2, 6);
            }
            PlayerPrefs.SetInt("Level", levelGame);
        }
    }


    public void StartButton()
    {
        PlayerPrefs.GetInt("Level", SceneManager.GetActiveScene().buildIndex);

        SceneManager.LoadScene(1);
    }


    public void Next()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinPanel()
    {
        winPanel.SetActive(true);
    }

    public void GameStart()
    {
        aiKarakter.IsStart();
    }

}
