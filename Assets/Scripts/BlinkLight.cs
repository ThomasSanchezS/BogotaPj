using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    //how to make a light bl   
    public Light light;
    public float maxIntensity;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        maxIntensity = light.intensity;    
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        light.intensity = maxIntensity * Mathf.Abs(Mathf.Sin(timer));
        
    }
}
