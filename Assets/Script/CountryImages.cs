using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CountryImages", menuName = "Assets/Country Images", order = 0)]
public class CountryImages : ScriptableObject
{
    public CountyID[] countyID;
}
[System.Serializable]
public class CountyID
{
    public string countryName;
    public string countryCity;
    public Sprite _countryImg;
    public CountyImages[] countyImages;
}
[System.Serializable]
public class CountyImages
{
    public string _countryName;
    public string _cityName;
    public Sprite _countryImg;
}

