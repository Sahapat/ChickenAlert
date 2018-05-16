using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunlightController : MonoBehaviour
{
    [SerializeField] private Color[] sunlights;
    [SerializeField] private float changingTime;
    private Light m_light;

    private void Awake()
    {
        m_light = GetComponent<Light>();
    }
    private void Update()
    {
    }
}
