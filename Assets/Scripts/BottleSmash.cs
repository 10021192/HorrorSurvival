using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSmash : MonoBehaviour
{
    private AudioSource audioPlayer;
    private bool playSound = false;
    private Rigidbody rb;
    public GameObject bottleParent;
    public float destroyTime = 3;
    public bool flames = false;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        Destroy(bottleParent, 20);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playSound == false)
        {
            playSound = true;
            audioPlayer.Play();
            rb.isKinematic = true;
            Destroy(bottleParent, destroyTime);
        }
        if(flames == true)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
        }
    }
}