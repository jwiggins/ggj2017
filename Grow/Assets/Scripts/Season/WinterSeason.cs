using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterSeason : Season {

	// Use this for initialization
	void Start () {
		Debug.Log("It's now winter");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override WeatherType CurrentWeather() {
		return WeatherType.Snow;
	}

	public override Season StepMonth(int month) {
		if (3 < month && month < 10) {
			return gameObject.AddComponent( typeof(SpringSeason)) as SpringSeason;
		}

		return this;
	}

}
