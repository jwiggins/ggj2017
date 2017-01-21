using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSeason : Season {

	// Use this for initialization
	void Start () {
		Debug.Log("It's now fall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override WeatherType CurrentWeather() {
		return WeatherType.Wind;
	}

	public override Season StepMonth(int month) {
		if (month > 11) {
			return gameObject.AddComponent( typeof(WinterSeason) ) as WinterSeason;
		}
		return this;
	}

}
