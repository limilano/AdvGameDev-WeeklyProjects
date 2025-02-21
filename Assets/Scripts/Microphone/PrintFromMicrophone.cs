using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class PrintFromMicrophone : MonoBehaviour
{
    public AudioLoudnessDetection detector;

    public float loudnessSensitivity = 100;
    public float threshold = 0.1f;

    

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputeLoudness();
    }

    void ComputeLoudness()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensitivity;
        if (loudness < threshold) 
            loudness = 0;

        if (loudness > 1.5)
            Debug.Log("Too loud!: " + loudness);
    }
}
