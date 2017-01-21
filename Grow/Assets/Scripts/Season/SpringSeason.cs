using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringSeason : Season {

	// Use this for initialization
	void Start () {
		Debug.Log("It's now spring");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override WeatherType CurrentWeather() {
		return WeatherType.Rain;
	}

	public override Season StepMonth(int month) {
		if (month > 5) {
			return gameObject.AddComponent( typeof(SummerSeason)) as SummerSeason;
		}
		return this;
	}

}
