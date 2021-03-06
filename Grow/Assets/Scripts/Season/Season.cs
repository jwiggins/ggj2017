﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Season {
    public enum SeasonType
    {
        SPRING = 0,
        SUMMER,
        FALL,
        WINTER
    }

    private SeasonType _type;
    private float _minTemperature;
    private float _maxTemperature;
    private Weather[] _weathers;
    private Weather _weather;

    public float minTemperature {
        set { this._minTemperature = value; }
    }

    public float maxTemperature {
        set { this._maxTemperature = value; }
    }

    public Weather[] weathers {
        set { this._weathers = value; }
    }

    public int moistureGain {
        get {
            switch (_weather.type) {
                case Weather.WeatherEnum.RAIN:
                    return 20;
                case Weather.WeatherEnum.SNOW:
                    return 10;
                default:
                    return 0;
            }
        }
    }

    public float moistureLoss {
        get {
            return _weather.moisture;
        }
    }

    public float temperature {
        get {
            float range = (_maxTemperature - _minTemperature) / 2.0f;
            float mid = (_minTemperature + _maxTemperature) / 2.0f;
            return mid + _weather.temperature * range;
        }
    }

    public Weather.WeatherEnum weatherType {
        get {
            return _weather.type;
        }
    }

    public void ChooseWeather() {
        float randval = Random.value;
        foreach (Weather w in _weathers)
        {
            randval -= w.probability;
            if (randval <= 0.0f) {
                _weather = w;
                return;
            }
        }
        _weather = _weathers[_weathers.Length-1];
    }

    static public SeasonType typeForMonth(int month) {
        if (3 <= month && month < 6) {
            return SeasonType.SPRING;
        }
        else if (6 <= month && month < 9) {
            return SeasonType.SUMMER;
        }
        else if (9 <= month && month < 11) {
            return SeasonType.FALL;
        }
        else if (11 <= month || month < 3) {
            return SeasonType.WINTER;
        }
        // We probably shouldn't get here. Right?
        return SeasonType.SPRING;
    }
}
