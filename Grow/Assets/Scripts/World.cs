using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    const float kMinSunAngle = 23.0f;
    const float kMaxSunAngle = 53.0f;
    const float kSunDistance = 10.0f;
    static readonly float[] kSunAngles = {
        23.0f, 29.0f, 35.0f, 41.0f, 47.0f, 53.0f,
        53.0f, 47.0f, 41.0f, 35.0f, 29.0f, 23.0f
    };

    int _month;

    public GameObject _sunlight;
    public GameObject _plant;

    [SerializeField]
    private Season[] _seasons; //these are all the seasons
    private Season _season; //this is the current season

    // Use this for initialization
    void Start () {
        _month = 1;
        _adjustSeason();
    }

    // Update is called once per frame
    void Update () {
    }

    void OnGUI() {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.character == ' ') {
            //  Advance the month when the spacebar is pressed
            _month = (_month % 12) + 1;
            _adjustSeason();
            e.Use();
        }
    }

    private void _adjustSeason() {
        _season = _seasons[(int)Season.typeForMonth(_month)];
        _adjustLight();
    }

    private void  _adjustLight() {
        // Camera is looking towards increasing Z!
        float height = kSunDistance * Mathf.Sin(Mathf.Deg2Rad * kSunAngles[_month-1]);
        Vector3 position = _plant.transform.position + new Vector3(0, height, -kSunDistance);
        Vector3 relativePos = _plant.transform.position - position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        _sunlight.transform.position = position;
        _sunlight.transform.rotation = rotation;
    }

}
