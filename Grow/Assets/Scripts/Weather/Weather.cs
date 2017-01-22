using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather {
    public enum WeatherEnum
    {
        RAIN,
        WIND,
        DROUGHT,
        SNOW
    }

    private WeatherEnum _type;
    private float _temperature;
    private float _moisture;
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
        get { return _type; }
        set { _type = value; }
    }

}
