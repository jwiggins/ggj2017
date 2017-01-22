using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour {

    float countdownTimer;

    void Start() {
        countdownTimer = 5f;
    }

    void Update() {
        countdownTimer -= Time.deltaTime;
        if (countdownTimer <= 0f) {
            SceneManager.LoadScene("WorldDev");
        }
    }
}
