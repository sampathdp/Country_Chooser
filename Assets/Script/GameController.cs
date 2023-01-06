using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CountryImages countyImages;

    [SerializeField] GameObject ParentObjImage;
    [SerializeField] GameObject ParentObjText;
    

    [SerializeField] SwipeMenu swipeMenu_I;
    [SerializeField] SwipeMenu swipeMenu_T;
    [SerializeField] Scrollbar scrollBar;
    [SerializeField] GameObject scrollBarName;
    [SerializeField] GameObject scrollBarImage;
    

    [Header("UI Menu")]
    [SerializeField] TextMeshProUGUI _countryName;
    [SerializeField] TextMeshProUGUI _Msg;
    [SerializeField] Image _countryImage;
    [SerializeField] Sprite _countryImg;
    [SerializeField] GameObject[] bullets;

    bool LevelPass;
    bool QMarkVisible;

    int countryID;
    int lvlPassCount;
    int subLvlCount;

    public static GameController instance;

    private void Awake()
    {
        instance = this;
        QMarkVisible = true;
    }

    private void Start()
    {
        subLvlCount = 0;
        bullets[0].GetComponent<RectTransform>().sizeDelta = new Vector2(45, 45);

        RandomizeCountryID();
        ScrollRandomizeImages();
        ScrollRandomizeCityName();
        SetCountryNameImg();
        _countryImage.sprite = _countryImg;
    }

    private void Update()
    {
        if (QMarkVisible)
            _countryImage.sprite = _countryImg;
    }

    void ScrollRandomizeImages()
    {
        float[] pos = new float[ParentObjImage.transform.childCount];

        for (int i = 0; i < pos.Length; i++)
        {
            if (ParentObjImage.transform.GetChild(i).GetComponent<Image>() != null)
                ParentObjImage.transform.GetChild(i).GetComponent<Image>().sprite = countyImages.countyID[countryID].countyImages[i]._countryImg;
        }

    }

    void ScrollRandomizeCityName()
    {
        float[] pos = new float[ParentObjText.transform.childCount];

        for (int i = 0; i < pos.Length; i++)
        {
            if (ParentObjText.transform.GetChild(i).GetComponent<TextMeshProUGUI>() != null)
                ParentObjText.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = countyImages.countyID[countryID].countyImages[i]._cityName;
        }
        scrollBarName.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }

    void SetCountryNameImg()
    {
        _countryName.text = countyImages.countyID[countryID].countryName;
        _countryImage.sprite = countyImages.countyID[countryID]._countryImg;
    }

    public void ChangeMainImage()
    {
        if (scrollBar.GetComponent<Scrollbar>().value > 0)
        {
            _countryImage.sprite = ParentObjImage.transform.GetChild(swipeMenu_I.position).GetComponent<Image>().sprite;
        }

    }

    public void SelectCountryCityImg()
    {

        Timer.instance.resetTime();
        Timer.instance.timer();
        string countryName = countyImages.countyID[countryID].countryCity;
        Sprite countryImage = _countryImage.GetComponent<Image>().sprite;

        string IcountryName = ParentObjText.transform.GetChild(swipeMenu_T.position).GetComponent<TextMeshProUGUI>().text.ToString();
        Sprite IcountryImage = ParentObjImage.transform.GetChild(swipeMenu_I.position).GetComponent<Image>().sprite;

        if (countryImage != null && countryName != null)
        {
            if (countryName == IcountryName && countryImage == IcountryImage)
            {
                print(countryName +" " +   IcountryName+ " X");
                print(countryImage + "  "+IcountryImage+" X");
                LevelPass = true;
            }
            else
            {
                print(countryName + " " + IcountryName + " Y");
                print(countryImage + "  " + IcountryImage + " Y");
                Lives.Instance.UpdateLives();
                LevelPass = false;
            }


            RandomizeCountryID();
            LoadNextLevel();
            CheckWinTimes();
        }

    }

    void LoadSubNextLevel()
    {
        BulletVisulize();
        ScrollRandomizeImages();
        ScrollRandomizeCityName();
        SetCountryNameImg();
    }

    void CheckWinTimes()
    {
        if (LevelPass)
            lvlPassCount++;

        if (lvlPassCount > 2)
            _Msg.text = "CONGRATULATIONS";
        else if (lvlPassCount > 2)
            _Msg.text = "Good Job, You Are Correctly Answered : " + lvlPassCount + " Questions.";
        else if (lvlPassCount == 0)
            _Msg.text = "Try Next Time.";
        else
            _Msg.text = "Not Bad, You Are Correctly Answered : " + lvlPassCount + " Questions. Try Your Best Next Time";


    }

    void LoadNextLevel()
    {
        subLvlCount++;
        if (subLvlCount == 3)
            Lives.Instance.UpdateLives();
        else
            LoadSubNextLevel();
    }

    void BulletVisulize()
    {
        if (subLvlCount == 1)
        {
            bullets[0].GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            bullets[1].GetComponent<RectTransform>().sizeDelta = new Vector2(45, 45);

        }
        else if (subLvlCount == 2)
        {
            bullets[1].GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
            bullets[2].GetComponent<RectTransform>().sizeDelta = new Vector2(45, 45);

        }
    }

    public void RandomizeCountryID()
    {
        int Rand;
        List<int> list = new List<int>();

        list = new List<int>(new int[countyImages.countyID.Length]);

        for (int j = 1; j < countyImages.countyID.Length; j++)
        {
            Rand = Random.Range(0, countyImages.countyID.Length);

            while (list.Contains(Rand))
            {
                Rand = Random.Range(0, countyImages.countyID.Length);
            }

            countryID = Rand;
        }
        print(countryID);


    }

}
