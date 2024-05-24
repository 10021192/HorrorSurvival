using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public enum ZombieType
    {
        shuffle,
        dizzy,
        alert
    }
    public enum ZombieState
    {
        Idle,
        Walking,
        Eating
    }

    public ZombieType zombieStyle;
    public ZombieState chooseState;
    public float yAdjustment = 0.0f;
    private Animator anim;
    public bool randomState = false;
    public float randomTiming = 5f;
    private int newState = 0;
    private int currentState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(((int)zombieStyle + 1), 1);
        if(zombieStyle == ZombieType.shuffle)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + yAdjustment, transform.position.z);
        }
        anim.SetTrigger(chooseState.ToString());
        currentState = (int)chooseState;
        if (randomState == true)
        {
            InvokeRepeating("SetAnimState", randomTiming, randomTiming);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAnimState()
    {
        newState = Random.Range(0, 3);
        if (newState != currentState)
        {
            chooseState = (ZombieState)newState;
            currentState = (int)chooseState;
            anim.SetTrigger(chooseState.ToString());
        }
    }
}