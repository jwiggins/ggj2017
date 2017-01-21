using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerSeason : Season {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override WeatherType CurrentWeather() {
		return WeatherType.Drought;
	}

	public override Season StepMonth(int month) {
		if (month > 9) {
			return new FallSeason();
		}

		return this;
	}

}
