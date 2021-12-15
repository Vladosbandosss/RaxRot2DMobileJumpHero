using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManger : MonoBehaviour
{
    public static GameOverManger instance;

   [SerializeField] private GameObject gameOverPanel;
   [SerializeField] private Text hightScoreText;
   
   private void Awake()
   {
       if (instance == null)
       {
           instance = this;
       }
   }

   public void ShowGameOverPanel()
    {
        
        Sound.instance.PlaySound(SOUNDFX.GO);
        gameOverPanel.SetActive(true);
        Debug.Log("помер");
        ////////
        if (PlayerPrefs.HasKey("hightScore"))
        {
            if (GameManger.instance.Score > PlayerPrefs.GetInt("hightScore"))
            {
                PlayerPrefs.SetInt("hightScore", GameManger.instance.Score);
                hightScoreText.text = GameManger.instance.Score.ToString();
            }
            else
            {
                hightScoreText.text = PlayerPrefs.GetInt("hightScore").ToString();
            }

        }
        else
        {
            PlayerPrefs.SetInt("hightScore", GameManger.instance.Score);
            hightScoreText.text = GameManger.instance.Score.ToString();
        }
        //////////////
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
