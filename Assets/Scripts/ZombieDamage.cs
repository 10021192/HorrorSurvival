using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour
{
    private bool damaging = true;
    private int zombieHealth = 100;
    private Animator zombieAnim;
    private AudioSource damagePlayer;
    private bool death = false;
    public GameObject bloodSplat;
    public string[] weaponTag;
    public int[] damageAmounts;
    public AudioClip[] damageSounds;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnim = GetComponentInParent<Animator>();
        damagePlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            damaging = true;
        }

        if(zombieHealth <= 0)
        {
            if(death == false)
            {
                death = true;
                zombieAnim.SetTrigger("dead");
                zombieAnim.SetBool("isDead", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < weaponTag.Length; i++)
        {
            if (other.CompareTag(weaponTag[i]))
            {
                if(damaging == true)
                {
                    damaging = false;
                    zombieHealth -= damageAmounts[i];
                    Vector3 pos = other.ClosestPoint(transform.position);
                    Instantiate(bloodSplat, pos, other.transform.rotation);
                    damagePlayer.clip = damageSounds[i];
                    damagePlayer.Play();
                    if (weaponTag[i] == "bat")
                    {
                        zombieAnim.SetTrigger("react");
                    }
                    if (weaponTag[i] == "axe")
                    {
                        zombieAnim.SetTrigger("axeReact");
                    }
                }
            }
        }
    }
}
