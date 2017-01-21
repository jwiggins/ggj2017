using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    [SerializeField]
    [Range(1,12)]
    private int _month; //this is the current month (1 month = 1 round)
    [SerializeField]
    private Season[] _seasons; //these are all the seasons
    private Season _season; //this is the current season

	const float kMonthDuration = 5.0f;
	float m_currentMonthTime;
	int m_month;

	// Use this for initialization
	void Start () {
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
		}
	}
}
