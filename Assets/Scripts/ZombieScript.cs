using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private AnimatorStateInfo animInfo;
    private NavMeshAgent agent;
    public bool randomState = false;
    public float randomTiming = 5f;
    private int newState = 0;
    private int currentState;
    private GameObject[] targets;
    private float[] walkSpeed = { 0.15f, 1.0f, 0.75f };
    private float distanceToTarget;
    private int currentTarget = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("Target");
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
        agent.destination = targets[0].transform.position;
        agent.speed = walkSpeed[(int)zombieStyle];
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, targets[currentTarget].transform.position);
        animInfo = anim.GetCurrentAnimatorStateInfo((int)zombieStyle);
        if (animInfo.IsTag("motion"))
        {
            if (anim.IsInTransition((int)zombieStyle + 1))
            {
                agent.isStopped = true;
            }
        }
        if (chooseState == ZombieState.Walking)
        {
            if (distanceToTarget < 1.5f)
            {
                if (currentTarget < targets.Length - 1)
                {
                    currentTarget = Random.Range(0, targets.Length);
                }
            }
        }
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

    public void WalkOn()
    {
        agent.isStopped = false;
        agent.destination = targets[currentTarget].transform.position;
    }
    public void WalkOff()
    {
        agent.isStopped = true;
    }
}