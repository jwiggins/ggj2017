using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterSeason : Season {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override WeatherType CurrentWeather() {
		return WeatherType.Snow;
	}

	public override Season StepMonth(int month) {
		if (month > 3) {
			return new SpringSeason();
		}

		return this;
	}

}
