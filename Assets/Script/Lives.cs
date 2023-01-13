using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] int maxLives;
    public int _lives;

    [SerializeField] GameObject GameOverPnl;

    public static Lives Instance;

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite EmptyHeart;
    [SerializeField] Button Next_Btn;

    private void Awake()
    {
        Instance = this;
        _lives = maxLives;
    }


    private void Update()
    {
        if (_lives > maxLives)
            _lives = maxLives;


        if (Timer.instance.timesUp && !GameController.instance._GameOver)
            _lives--;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < _lives)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = EmptyHeart;

            if (i < maxLives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
        if (GameController.instance._GameOver)
            Invoke("loadPanal", 1f);
    }


    public void UpdateLives()
    {
        if (!GameController.instance._GameOver)
            _lives--;
        if (_lives <= 0)
        {
            GameController.instance._GameOver = true;
            Next_Btn.interactable = false;
            Invoke("loadPanal", 1f);

        }
    }

    public void loadPanal()
    {
        Next_Btn.interactable = false;
        GameOverPnl.SetActive(true);
    }

}
