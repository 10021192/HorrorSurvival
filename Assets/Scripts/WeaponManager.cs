using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum weaponSelect
    {
        knife,
        cleaver,
        bat,
        axe,
        pistol,
        shotgun,
        sprayCan,
        bottle,
        bottleWithCloth
    }

    public weaponSelect chosenWeapon;
    public GameObject[] weapons;
    //private int weaponID = 0;
    private Animator anim;
    private AudioSource audioPlayer;
    public AudioClip[] weaponSounds;
    private int currentWeaponID;
    private bool spraySoundOn = false;
    public GameObject sprayPanel;
    public static bool emptyBottleThrow = false;
    public static bool molotovBottleThrow = false;
    private AnimatorStateInfo animInfo;
    private bool canAttack = true;
    private bool sprayEmpty = false;
    private bool stopSpray = false;

    // Start is called before the first frame update
    void Start()
    {
        SaveScript.weaponID = (int)chosenWeapon;
        anim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        ChangeWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.IsTag("BottleThrown"))
        {
            canAttack = false;
        }
        else
        {
            canAttack = true;
        }
        if (SaveScript.weaponID != currentWeaponID)
        {
            ChangeWeapons();
        }

        if (Input.GetMouseButtonDown(0) && canAttack == true)
        {
            if(SaveScript.inventoryOpen == false)
            {
                if (SaveScript.currentAmmo[SaveScript.weaponID] > 0 && SaveScript.stamina > 20)
                {
                    anim.SetTrigger("Attack");
                    audioPlayer.clip = weaponSounds[SaveScript.weaponID];
                    audioPlayer.Play();

                    if(SaveScript.weaponID == 4 || SaveScript.weaponID == 5)
                    {
                        SaveScript.currentAmmo[SaveScript.weaponID]--;
                    }
                }
                else
                {
                    if (SaveScript.weaponID == 4 || SaveScript.weaponID == 5)
                    {
                        audioPlayer.clip = weaponSounds[9];
                        audioPlayer.Play();
                    }
                }
            }
        }
        if (Input.GetMouseButton(0) && sprayPanel.GetComponent<SprayScript>().sprayAmount > 0.0f)
        {
            sprayEmpty = false;
            stopSpray = false;
            if(SaveScript.weaponID == 6 && SaveScript.inventoryOpen == false)
            {
                if(spraySoundOn == false)
                {
                    spraySoundOn = true;
                    anim.SetTrigger("Attack");
                    StartCoroutine(StartSpraySound());
                }
            }
        }
        if (Input.GetMouseButtonUp(0) || sprayPanel.GetComponent<SprayScript>().sprayAmount <= 0.0f)
        {
            if (SaveScript.weaponID == 6 && SaveScript.inventoryOpen == false && stopSpray == false)
            {
                stopSpray = true;
                anim.SetTrigger("Release");
                spraySoundOn = false;
                audioPlayer.Stop();
                audioPlayer.loop = false;
            }
        }
        if(sprayPanel.GetComponent<SprayScript>().sprayAmount <= 0 && sprayEmpty == false)
        {
            sprayEmpty = true;
            SaveScript.weaponAmounts[6]--;
            if (SaveScript.weaponAmounts[6] == 0)
            {
                SaveScript.weaponsPickedUp[6] = false;
            }
        }
    }

    public void ChangeWeapons()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[SaveScript.weaponID].SetActive(true);
        chosenWeapon = (weaponSelect)SaveScript.weaponID;
        anim.SetInteger("WeaponID", SaveScript.weaponID);
        anim.SetBool("WeaponChanged", true);
        currentWeaponID = SaveScript.weaponID;

        Move();
        StartCoroutine(WeaponReset());
    }

    private void Move()
    {
        switch (chosenWeapon)
        {
            case weaponSelect.knife:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.cleaver:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.bat:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.axe:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.pistol:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.shotgun:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.46f);
                break;
            case weaponSelect.sprayCan:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
            case weaponSelect.bottle:
                transform.localPosition = new Vector3(0.02f, -0.193f, 0.66f);
                break;
        }
    }

    public void BottleThrowEmpty()
    {
        emptyBottleThrow = true;
    }

    public void BottleThrowFire()
    {
        molotovBottleThrow = true;
    }

    public void LoadAnotherBottle()
    {
        if(SaveScript.weaponID == 7)
        {
            ChangeWeapons();
        }
    }

    public void LoadAnotherMolotovBottle()
    {
        if (SaveScript.weaponID == 8)
        {
            ChangeWeapons();
        }
    }

    IEnumerator WeaponReset()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("WeaponChanged", false);
    }
    
    IEnumerator StartSpraySound()
    {
        yield return new WaitForSeconds(0.3f);
        audioPlayer.clip = weaponSounds[SaveScript.weaponID];
        audioPlayer.Play();
        audioPlayer.loop = true;
    }
}