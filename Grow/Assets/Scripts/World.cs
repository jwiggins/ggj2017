using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	const float kMonthDuration = 5.0f;
	float m_currentMonthTime;

	Plant m_plant;
	Season m_season;
	WeatherType m_weather;
	int m_month;

	// Use this for initialization
	void Start () {
		m_plant = gameObject.AddComponent( typeof(Plant) ) as Plant;
		m_season = gameObject.AddComponent( typeof(WinterSeason) ) as WinterSeason;
		m_weather = m_season.CurrentWeather();
		m_month = 1;
		m_currentMonthTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		m_currentMonthTime += Time.deltaTime;

		// Maybe advance the month
		if (m_currentMonthTime >= kMonthDuration) {
			m_month = (m_month % 12) + 1;
			m_currentMonthTime = 0.0f;

			Debug.Log("The month is now: " + m_month.ToString());

			// ... which might change the season
			m_season = m_season.StepMonth(m_month);
		}

		m_weather = m_season.CurrentWeather();
		m_plant.ExperienceEnvironment(m_season, m_weather);
	}
}
