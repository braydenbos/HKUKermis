using System.Collections;
using System.Collections.Generic;
using Games;
using UnityEngine;

public class LightBulb : Target
{
    [SerializeField]
    private GameObject lightBulb, brokenLight;

    public override void OnHit()
    {
        lightBulb.SetActive(false);
        brokenLight.SetActive(true);
    }
}
