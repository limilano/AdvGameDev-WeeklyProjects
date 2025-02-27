using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class PrintFromMicrophone : MonoBehaviour
{
    public AudioLoudnessDetection detector;

    public float loudnessSensitivity = 100;
    public float threshold = 0.1f;

    public float listeningTime = 10f;
    public float timeTooLoud = 0f;
    public bool listening = false;
    public Button startButton;
    public float loudnessThreshold = 1f;

    void Start()
    {
        startButton.GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(listening) 
        {
            ComputeLoudness(Time.deltaTime);
        }
        

    }


    public void ComputeLoudness(float deltaTime)
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensitivity;
        if (loudness < threshold) 
            loudness = 0;

        if (loudness > loudnessThreshold)
        {
            Debug.Log("Too loud!: " + loudness);
            timeTooLoud += deltaTime;
        }
            
    }

    public void StartListening()
    {
        if(!listening)
        {
            startButton.GetComponent<Image>().color = Color.red;
            listening = true;
            timeTooLoud = 0f;
            Invoke("StopListening", listeningTime);
        }
    }

    void StopListening()
    {
        listening = false;
        Debug.Log("Too loud for: " + timeTooLoud + " seconds");
        startButton.GetComponent<Image>().color = Color.green;
    }
}
