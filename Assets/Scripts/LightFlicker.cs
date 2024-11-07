using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private LightScript[] lightsOne, lightsTwo;
    [SerializeField] private Color onColor, offColor;
    [SerializeField] private bool lightOneOn;
    [SerializeField] private float waitTime;

    private void Awake() => StartCoroutine(SwitchLights());
    
    private IEnumerator SwitchLights()
    {
        var on = lightOneOn ? lightsOne : lightsTwo;
        var off = lightOneOn ? lightsTwo : lightsOne;
        foreach (var imageLight in on)
        {
            imageLight.rays.SetActive(true);
            imageLight.lightBulb.color = onColor;
        }
        foreach (var imageLight in off)
        {
            imageLight.rays.SetActive(false);
            imageLight.lightBulb.color = offColor;
        }
        lightOneOn = !lightOneOn;
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(SwitchLights());
    }
}
