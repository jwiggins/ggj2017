using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weather {
    public enum WeatherEnum
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
    [Range(0,1)]
    private float _gravity;
    [SerializeField]
    [Range(-1,1)]
    private float _temperature;
    [SerializeField]
    [Range(-1,1)]
    private float _moisture;

    public float gravity {
        get {
            return _gravity;
        }
    }

    public float moisture {
        get {
            return _moisture;
        }
    }

    public float temperature {
        get {
            return _temperature;
        }
    }

    public WeatherEnum type {
        get {
            return _weather;
        }
    }

}
