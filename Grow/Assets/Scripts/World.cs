using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	Plant m_plant;
	Season m_season;
	WeatherType m_weather;
	int m_month;

	// Use this for initialization
	void Start () {
		m_month = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// Advance the month
		m_month = (m_month % 12) + 1;
		// ... which might change the season
		m_season = m_season.StepMonth(m_month);
		m_weather = m_season.CurrentWeather();
		m_plant.ExperienceEnvironment(m_season, m_weather);
	}
}
