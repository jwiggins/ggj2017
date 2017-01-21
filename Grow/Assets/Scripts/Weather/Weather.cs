using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weather {
    private enum WeatherEnum
    {
        RAIN,
        WIND,
        DROUGHT,
        SNOW
    }
    [SerializeField]
    private string _name;
    [SerializeField]
    private WeatherEnum _weather;
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _temperature;
    [SerializeField]
    private float _moisture;
}
