using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Image timerBar;
    [SerializeField] float maxTime;

    float timeLeft;
    public bool timesUp;

    public static Timer instance;

    private void Start()
    {
        timeLeft = maxTime;
        instance = this;
        Invoke("timer", 1.2f);
    }
    private void Update()
    {
        if(!GameController.instance.isActiveAnswerPnl)
            Invoke("timer",1.2f);
    }
    public void resetTime()
    {
        timeLeft = maxTime;
    }

    public void timer()
    {
        if (timeLeft > 0 && !GameController.instance._GameOver)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
            timesUp = false;
        }
        else
        {
            timeLeft = maxTime;
            timesUp = true;
            GameController.instance.SelectCountryCityImg();
        }
    }

}

