using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour {
    const float kSunDistance = 10.0f;
    static readonly float[] kSunAngles = {
        23.0f, 29.0f, 35.0f, 41.0f, 47.0f, 53.0f,
        53.0f, 47.0f, 41.0f, 35.0f, 29.0f, 23.0f
    };
    static readonly string[] kMonths = {
        "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Dec"
    };


    int _month;
    int _soilMoisture;

    public GameObject _sunlight;
    public GameObject _plant;
    private Plant _plantInstance;

    private Season[] _seasons; //these are all the seasons
    private Season _season; //this is the current season

    [SerializeField]
    private GameObject[] _weatherEffects; // Prefabs for weather effects
    GameObject _weatherEffect;

    // Use this for initialization
    void Start () {
        _loadWeatherData();
        _month = 4; // Start in the Spring
        _soilMoisture = 50;
        _plantInstance = _plant.GetComponent( typeof(Plant) ) as Plant;
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

        // Compute environmental factors
        float light = _adjustLight();
        _soilMoisture += _season.moistureGain;
        _soilMoisture = (int)Mathf.Repeat(_soilMoisture * _season.moistureLoss, 100);
        _plantInstance.ExperienceEnvironment(_season.temperature, _soilMoisture, light);

        _updateHud();

        if (_weatherEffect != null) {
            Destroy(_weatherEffect);
            _weatherEffect = null;
        }

        switch(_season.weatherType) {
            case Weather.WeatherEnum.SNOW:
                _startWeatherEffect("Snow");
                break;
            case Weather.WeatherEnum.RAIN:
                _startWeatherEffect("Rain");
                break;
            case Weather.WeatherEnum.WIND:
                _startWeatherEffect("Wind");
                break;

        }
    }

    private float _adjustLight() {
        // Camera is looking towards increasing Z!
        float height = kSunDistance * Mathf.Sin(Mathf.Deg2Rad * kSunAngles[_month-1]);
        Vector3 position = _plant.transform.position + new Vector3(5.0f, height, kSunDistance);
        Vector3 relativePos = _plant.transform.position - position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        _sunlight.transform.position = position;
        _sunlight.transform.rotation = rotation;

        float minSunAngle = kSunAngles[0];
        float maxSunAngle = kSunAngles[5];
        float sunAngleRange = maxSunAngle - minSunAngle;
        return (kSunAngles[_month-1] - minSunAngle) / sunAngleRange;
    }

    private void _startWeatherEffect(string name) {
        GameObject camera = GameObject.Find("Camera");
        foreach (GameObject prefab in _weatherEffects) {
            if (prefab.name == name) {
                _weatherEffect = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                _weatherEffect.transform.SetParent(camera.transform);
            }
        }
    }

    private void _updateHud() {
        Text energyDisp = GameObject.Find("Energy").GetComponent<Text>();
        energyDisp.text = "Energy: " + _plantInstance.energy.ToString();
        Text tempDisp = GameObject.Find("Temperature").GetComponent<Text>();
        tempDisp.text = "Temperature: " + _season.temperature.ToString() + "ºC";
        Text monthDisp = GameObject.Find("Month").GetComponent<Text>();
        monthDisp.text = "Date: " + kMonths[_month-1];
    }

    private void _loadWeatherData() {
        _seasons = new Season[4];

        string fileString = File.ReadAllText("Assets/Data/weather.txt");
        string [] fileLines = fileString.Split('\n');
        int line = 0;

        for (int i=0; i < 4; ++i) {
            string [] tempsStr = fileLines[line++].Split(' ');
            _seasons[i] = new Season();
            _seasons[i].minTemperature = float.Parse(tempsStr[0]);
            _seasons[i].maxTemperature = float.Parse(tempsStr[1]);

            Weather[] weathers = new Weather[4];
            for (int j=0; j < 4; ++j) {
                string [] weatherStr = fileLines[line++].Split(' ');
                weathers[j] = new Weather();
                weathers[j].type = Weather.WeatherEnum.RAIN + j;
                weathers[j].temperature = float.Parse(weatherStr[0]);
                weathers[j].moisture = float.Parse(weatherStr[1]);
                weathers[j].probability = float.Parse(weatherStr[2]);
            }
            _seasons[i].weathers = weathers;
        }
    }

}
