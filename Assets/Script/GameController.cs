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
    [SerializeField] ScrollRectSnap scrollRect_I;
    [SerializeField] ScrollRectSnap scrollRect_T;


    [Header("UI Menu")]
    [SerializeField] TextMeshProUGUI _countryName;
    [SerializeField] TextMeshProUGUI _Msg;
    [SerializeField] Image _countryImage;
    [SerializeField] Sprite _countryImg;
    [SerializeField] GameObject[] bullets;
    [SerializeField] GameObject pnlAnswers;

    [SerializeField] TextMeshProUGUI _MsgAnswerPnl;
    [SerializeField] TextMeshProUGUI _countryNameAnswerPnl;
    [SerializeField] TextMeshProUGUI _capitalCityAnswerPnl_A;
    [SerializeField] TextMeshProUGUI _capitalCityAnswerPnl_C;
    [SerializeField] TextMeshProUGUI _scoretxt;
    [SerializeField] TextMeshProUGUI _scoretxtAnswerPn;

    [SerializeField] Image _countryImgAnswerPnl_A;
    [SerializeField] Image _countryImgAnswerPnl_C;
    [SerializeField] Sprite _correctImgAnswerPnl;
    [SerializeField] Sprite _inCorrectImgAnswerPnl;
    [SerializeField] Image _correctImgAnswerPnl_1;
    [SerializeField] Image _correctImgAnswerPnl_2;

    [SerializeField] Button _Btn;


    bool LevelPass;
    public bool isActiveAnswerPnl;
    public bool _GameOver;


    int countryID;
    int lvlPassCount;
    int subLvlCount;
    int score;

    public static GameController instance;

    private void Awake()
    {
        instance = this;
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
    }

    void SetCountryNameImg()
    {
        _countryName.text = countyImages.countyID[countryID].countryName;
        _countryImage.sprite = countyImages.countyID[countryID]._countryImg;
    }

    public void ChangeMainImage()
    {
        _countryImage.sprite = countyImages.countyID[countryID].countyImages[scrollRect_I.minButtonNum]._countryImgSQ;

    }

    public void SelectCountryCityImg()
    {
        if (!_GameOver)
        {
            StartCoroutine(ShowAnswerPanal());

            string countryName = _countryName.GetComponent<TextMeshProUGUI>().text.ToString();
            string cityName = countyImages.countyID[countryID].countryCity;
            Sprite countryImage = countyImages.countyID[countryID]._countryImg;

            string ICapitalCityName = ParentObjText.transform.GetChild(scrollRect_T.minButtonNum).GetComponent<TextMeshProUGUI>().text.ToString();
            Sprite IcountryImage = ParentObjImage.transform.GetChild(scrollRect_I.minButtonNum).GetComponent<Image>().sprite;

            if (countryImage != null && cityName != null)
            {
                if (cityName == ICapitalCityName && countryImage == IcountryImage)
                {
                    LevelPass = true;
                    score++;
                    _scoretxt.text = score.ToString();
                }
                else
                {
                    Lives.Instance.UpdateLives();
                    LevelPass = false;
                }

                //Answer Panal Details
                if (cityName == ICapitalCityName)
                {
                    _correctImgAnswerPnl_1.sprite = _correctImgAnswerPnl;
                }
                if (cityName != ICapitalCityName)
                {
                    _correctImgAnswerPnl_1.sprite = _inCorrectImgAnswerPnl;
                }
                if (countryImage == IcountryImage)
                {
                    _correctImgAnswerPnl_2.sprite = _correctImgAnswerPnl;
                }
                if (countryImage != IcountryImage)
                {
                    _correctImgAnswerPnl_2.sprite = _inCorrectImgAnswerPnl;
                }


                _countryNameAnswerPnl.text = countryName;
                _capitalCityAnswerPnl_A.text = ICapitalCityName;
                _capitalCityAnswerPnl_C.text = cityName;

                _countryImgAnswerPnl_A.sprite = countryImage;
                _countryImgAnswerPnl_C.sprite = IcountryImage;
                _scoretxtAnswerPn.text = "Your Score " + score.ToString();

                //Answer Panal Details

                RandomizeCountryID();
                LoadNextLevel();
                CheckWinTimes();
                ScrollRectSnap.Instance.GoToBtn(1);
            }
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
        {
            Lives.Instance.UpdateLives();
            _GameOver = true;
        }
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
    IEnumerator ShowAnswerPanal()
    {
        _Btn.interactable = false;
        isActiveAnswerPnl = true;
        pnlAnswers.SetActive(true);
        yield return new WaitForSeconds(2f);
        _Btn.interactable = true;
        isActiveAnswerPnl = false;
        pnlAnswers.SetActive(false);
        Timer.instance.resetTime();

    }

}
