using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyLight : MonoBehaviour
{
    private Light light;

    [SerializeField]
    private float secondsToChange = 5;

    private void Awake()
    {
        light = GetComponent<Light>();
    }
    void Start()
    {
        StartCoroutine(ChangeLights());
    }

    private IEnumerator ChangeLights()
    {
        float hue, saturation, value;

        Color.RGBToHSV(light.color, out hue, out saturation, out value);
        while (true)
        {
            yield return new WaitForSeconds(secondsToChange);
            hue += 0.15f;
            if (hue > 1)
            {
                hue = hue - 1;
            }
            light.color = Color.HSVToRGB(hue, saturation, value);

        }
    }
}
