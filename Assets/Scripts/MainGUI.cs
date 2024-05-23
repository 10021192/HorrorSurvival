using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGUI : MonoBehaviour
{
    public Text healthAmount;
    public Text staminaAmount;
    public Text infectionAmount;

    // Update is called once per frame
    void Update()
    {
        healthAmount.text = SaveScript.health + "%";
        staminaAmount.text = SaveScript.stamina.ToString("F0") + "%";
        infectionAmount.text = SaveScript.infection.ToString("F0") + "%";
    }
}
