using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class TorchFlicker : MonoBehaviour
{
    private Transform mainLight;
    private Transform flickerLight;
    private UnityEngine.Rendering.Universal.Light2D mainLightComponent;
    private UnityEngine.Rendering.Universal.Light2D flickerLightComponent;


    // Start is called before the first frame update
    void Start()
    {
        mainLight = this.transform.GetChild(0);
        flickerLight = this.transform.GetChild(1);
        mainLightComponent = mainLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        flickerLightComponent = flickerLight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        for (; ; )
        {
            float randomIntensity = Random.Range(0.5f, 0.8f);
            flickerLightComponent.intensity = randomIntensity;


            float randomTime = Random.Range(0f, 0.3f);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
