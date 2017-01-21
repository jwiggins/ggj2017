using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Season {
    [SerializeField]
    private string _name;
    [SerializeField]
    private float _minTemperature;
    [SerializeField]
    private float _maxTemperature;
    [SerializeField]
    private Weather[] _weathers;
}
