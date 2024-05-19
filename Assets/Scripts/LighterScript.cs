using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterScript : MonoBehaviour
{
    public GameObject lighter;

    // Start is called before the first frame update
    void OnEnable()
    {
        lighter.SetActive(true);
    }

    void OnDisable()
    {
        lighter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
