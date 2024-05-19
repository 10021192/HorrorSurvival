using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLightScript : MonoBehaviour
{
    private Image batteryBars;
    public float batteryPower = 1.0f;
    public float drainTime = 2;

    // Start is called before the first frame update
    void OnEnable()
    {
        batteryBars = GameObject.Find("FLBatteryBars").GetComponent<Image>();
        InvokeRepeating("FLBatteryDrain", drainTime, drainTime);
    }

    // Update is called once per frame
    void Update()
    {
        batteryBars.fillAmount = batteryPower;
    }

    private void FLBatteryDrain()
    {
        if (batteryPower > 0.0f)
            batteryPower -= 0.25f;
    }

    public void StopDrain()
    {
        CancelInvoke("FLBatteryDrain");
    }
}
