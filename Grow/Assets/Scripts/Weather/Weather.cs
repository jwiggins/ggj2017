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
    [SerializeField]
    [Range(0,1)]
    private float _probability;

    public float moisture {
        get { return _moisture; }
        set { _moisture = value; }
    }

    public float probability {
        get { return _probability; }
        set { _probability = value; }
    }

    public float temperature {
        get { return _temperature; }
        set { _temperature = value; }
    }

    public WeatherEnum type {
        get { return _weather; }
        set { _weather = value; }
    }

}
