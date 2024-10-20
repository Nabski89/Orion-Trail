using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePitch : MonoBehaviour
{
    public float pitchRange = 0.1f; // The range of pitch variation

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float randomPitch = Random.Range(1f - pitchRange, 1f + pitchRange);
        audioSource.pitch = randomPitch;
    }

}
