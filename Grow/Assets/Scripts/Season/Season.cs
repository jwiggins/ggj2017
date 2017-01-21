using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Season {
    private enum SeasonType
    {
        SPRING,
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
}
