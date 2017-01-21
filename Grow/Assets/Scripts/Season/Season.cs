using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Season {
    public enum SeasonType
    {
        SPRING = 0,
        SUMMER,
        FALL,
        WINTER
    }
    [SerializeField]
    private string _name;
    [SerializeField]
    private SeasonType _type;
    [SerializeField]
    private float _minTemperature;
    [SerializeField]
    private float _maxTemperature;
    [SerializeField]
    private Weather[] _weathers;

    public Weather CurrentWeather() {
        return _weathers[0];
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
