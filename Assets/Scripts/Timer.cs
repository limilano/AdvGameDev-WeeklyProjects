// https://www.youtube.com/watch?v=HLz_k6DSQvU
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{
    bool stopwatchActive = false;
    public float currentTime;
    public TMP_Text currentTimeText;
    [SerializeField] private Collider feetCollider;
    [SerializeField] private Collider startCollider;
    [SerializeField] private Collider stopCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckStart()) StartStopwatch();
        if(CheckStop()) StopStopwatch();

        if (stopwatchActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartStopwatch()
    {
        Start();
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }

    public bool CheckStart()
    {
        if(feetCollider.bounds.Intersects(startCollider.bounds)) return true;
        return false;
    }

    public bool CheckStop()
    {
        if(feetCollider.bounds.Intersects(stopCollider.bounds)) return true;
        return false;
    }
}
