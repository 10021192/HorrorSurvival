using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SaveScript : MonoBehaviour
{
    public static bool inventoryOpen = false;
    public static int weaponID = 0;
    public static bool[] weaponsPickedUp = new bool[9];
    public static int itemID = 0;
    public static bool[] itemsPickedUp = new bool[13];
    public static int[] weaponAmounts= new int[9];
    public static int[] itemAmounts = new int[13];
    public static bool change = false;
    public static int[] ammoAmounts = new int[2];
    public static int[] currentAmmo = new int[9];
    public static float stamina;
    public static float infection;
    public static int health;
    public static GameObject doorObject;
    public static List<GameObject> zombiesChasing = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        stamina = FirstPersonController.FPSstamina;
        health = 99;
        weaponsPickedUp[0] = true;
        weaponAmounts[0] = 1;
        itemsPickedUp[0] = true;
        itemsPickedUp[1] = true;
        itemAmounts[0] = 1;
        itemAmounts[1] = 1;
        ammoAmounts[0] = 12;
        ammoAmounts[1] = 2;

        for (int i = 0; i < currentAmmo.Length; i++)
        {
            currentAmmo[i] = 2;
        }
        currentAmmo[4] = 12;
        currentAmmo[6] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(FirstPersonController.inventorySwitchedOn == true)
        {
            inventoryOpen = true;
        }
        if (FirstPersonController.inventorySwitchedOn == false)
        {
            inventoryOpen = false;
        }

        if(Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.LeftShift) && FirstPersonController.FPSstamina > 0.0f)
        {
            FirstPersonController.FPSstamina -= 10 * Time.deltaTime;
            stamina = FirstPersonController.FPSstamina;
        }
        if(stamina < 100)
        {
            FirstPersonController.FPSstamina += 3.35f * Time.deltaTime;
            stamina = FirstPersonController.FPSstamina;
        }
        if(stamina >= 100)
        {
            FirstPersonController.FPSstamina = stamina;
        }
        if(infection < 50)
        {
            infection += 0.1f * Time.deltaTime;
        }
        if (infection > 49 && infection < 100)
        {
            infection += 0.4f * Time.deltaTime;
        }

        if (change  == true)
        {
            change = false;
            for(int i = 1; i < weaponAmounts.Length; i++)
            {
                if (weaponAmounts[i] > 0)
                {
                    weaponsPickedUp[i] = true;
                }
                else if (weaponAmounts[i] == 0)
                {
                    weaponsPickedUp[i] = false;
                }
            }
            for (int i = 2; i < itemAmounts.Length; i++)
            {
                if (itemAmounts[i] > 0)
                {
                    itemsPickedUp[i] = true;
                }
                else if (itemAmounts[i] == 0)
                {
                    itemsPickedUp[i] = false;
                }
            }
        }
    }
}
