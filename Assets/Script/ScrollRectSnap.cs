using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour
{
    public RectTransform panel;
    public Button[] btn;
    public RectTransform center;
    public int startBtn = 1;
    public int minButtonNum;

    float[] distance;
    float[] distReposition;
    bool dragging;
    int btnDistance;
    int btnLength;
    float lerpSpeed = 5f;
    bool tartgetNearBtn=true;

    public static ScrollRectSnap Instance;

    private void Start()
    {
        Instance=this;
        btnLength = btn.Length;
        distance = new float[btnLength];
        distReposition = new float[btnLength];

        btnDistance = (int)Mathf.Abs(btn[1].GetComponent<RectTransform>().anchoredPosition.x -
            btn[0].GetComponent<RectTransform>().anchoredPosition.x);

        panel.anchoredPosition = new Vector2((startBtn - 1) * 300, 0f);
    }

    private void Update()
    {
        for(int i = 0; i < btn.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - btn[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distReposition[i]);

            if (distReposition[i] > 1000)
            {
                float curX = btn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = btn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchorPos = new Vector2(curX+(btnLength*btnDistance), curY);
                btn[i].GetComponent<RectTransform>().anchoredPosition = newAnchorPos;
            }
            if (distReposition[i] < -1000)
            {
                float curX = btn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = btn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchorPos = new Vector2(curX - (btnLength * btnDistance), curY);
                btn[i].GetComponent<RectTransform>().anchoredPosition = newAnchorPos;
            }
            if (tartgetNearBtn)
            {
                float minDistance = Mathf.Min(distance);
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);

                for (int a = 0; a < btn.Length; a++)
                {
                    if (minDistance == distance[a])
                    {
                        minButtonNum = a;
                        GameController.instance.ChangeMainImage();
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(1.3f, 1.3f), 0.1f);
                        
                    }
                }
            }

        }
        
        if (!dragging)
        {
            LerpToBtn(-btn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
        }

    }

    private void LerpToBtn(float position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * lerpSpeed);
        if (Mathf.Abs(position - newX) < 3f)
            newX = position;


        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        lerpSpeed = 5f;
        dragging = true;
        tartgetNearBtn = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }

    public void GoToBtn(int btnIndex)
    {
        tartgetNearBtn = false;
        minButtonNum = btnIndex - 1;
    }
}
