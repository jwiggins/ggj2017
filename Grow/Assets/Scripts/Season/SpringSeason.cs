using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringSeason : Season {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override WeatherType CurrentWeather() {
		return WeatherType.Rain;
	}

	public override Season StepMonth(int month) {
		if (month > 5) {
			return new SummerSeason();
		}
		return this;
	}

}
