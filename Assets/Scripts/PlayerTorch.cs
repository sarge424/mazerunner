using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTorch : MonoBehaviour
{
    public float brightness;
    public float dischargeRate;
    public float rechargeRate;
    public Light lightSource;
    
    float batteryLife;
    bool on;


    // Start is called before the first frame update
    void Start()
    {
        on = false;
        lightSource.intensity = 0f;
        batteryLife = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)){
            on = !on;
        }

        if(on)
            batteryLife -= Time.deltaTime * dischargeRate;
        else
            batteryLife += Time.deltaTime * rechargeRate;

        batteryLife = Mathf.Clamp(batteryLife, 0f, 1f);

        if(batteryLife == 0f){
            on = false;
        }

        if(on)
            lightSource.intensity = brightness;
        else
            lightSource.intensity = 0f;
        

        Debug.Log(batteryLife);
    }
}
