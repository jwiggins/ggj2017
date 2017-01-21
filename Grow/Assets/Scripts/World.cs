﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class World : MonoBehaviour {
    const float kSunDistance = 10.0f;
    static readonly float[] kSunAngles = {
        23.0f, 29.0f, 35.0f, 41.0f, 47.0f, 53.0f,
        53.0f, 47.0f, 41.0f, 35.0f, 29.0f, 23.0f
    };

    int _month;

    public GameObject _sunlight;
    public GameObject _plant;
    
    [SerializeField]
    private GameObject[] _weatherEffects; // Prefabs for weather effects
    GameObject _weatherEffect;

    [SerializeField]
    private Season[] _seasons; //these are all the seasons
    private Season _season; //this is the current season

    // Use this for initialization
    void Start () {
        _month = 1;
        _weatherEffect = null;
        _adjustToMonth();
    }

    // Update is called once per frame
    void Update () {
    }

    void OnGUI() {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.character == ' ') {
            //  Advance the month when the spacebar is pressed
            _month = (_month % 12) + 1;
            _adjustToMonth();
            e.Use();
        }
    }

    private void _adjustToMonth() {
        _season = _seasons[(int)Season.typeForMonth(_month)];
        _season.ChooseWeather();
        _adjustLight();


        if (_weatherEffect != null) {
            Destroy(_weatherEffect);
            _weatherEffect = null;
        }

        switch(_season.weatherType) {
            case Weather.WeatherEnum.SNOW: {
                GameObject effect = _getWeatherPrefab("Snow");
                _weatherEffect = PrefabUtility.InstantiatePrefab(effect) as GameObject;
                break;
            }
            case Weather.WeatherEnum.RAIN: {
                GameObject effect = _getWeatherPrefab("Rain");
                _weatherEffect = PrefabUtility.InstantiatePrefab(effect) as GameObject;
                break;
            }
        }
    }

    private void _adjustLight() {
        // Camera is looking towards increasing Z!
        float height = kSunDistance * Mathf.Sin(Mathf.Deg2Rad * kSunAngles[_month-1]);
        Vector3 position = _plant.transform.position + new Vector3(0, height, -kSunDistance);
        Vector3 relativePos = _plant.transform.position - position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        _sunlight.transform.position = position;
        _sunlight.transform.rotation = rotation;
    }

    private GameObject _getWeatherPrefab(string name) {
        foreach (GameObject prefab in _weatherEffects)
        {
            if (prefab.name == name) return prefab;
        }
        return null;
    }

}
