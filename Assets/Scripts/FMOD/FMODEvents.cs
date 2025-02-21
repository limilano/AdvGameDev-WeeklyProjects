using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    // [field: Header("Ambience")]
    // [field: SerializeField] public EventReference ambience { get; private set; }

    // [field: Header("Music")]
    // [field: SerializeField] public EventReference music { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerWalking { get; private set; }

    [field: Header("Jump SFX")]
    [field: SerializeField] public EventReference jump { get; private set; }

    [field: Header("Bounce SFX")]
    [field: SerializeField] public EventReference bounce { get; private set; }

    [field: Header("Wobble SFX")]
    [field: SerializeField] public EventReference wobble { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}