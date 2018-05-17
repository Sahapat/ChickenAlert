using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agentParent;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float moveTowardDelta;
    [SerializeField] private Transform lookAtPos;
    [SerializeField] private float eatDuration;
    [SerializeField] private float stunDuration;
    [SerializeField] private float screamDuration;

    private bool isEat;
    private float eatCounter;
    private float stunCounter;
    private bool isHaveTarget;

    private WolfController m_wolfController;
    private GameObject target;
    private Vector3 destination;
    private bool moveToPositionTrigger;

    private void Awake()
    {
        agentParent.speed = speed;
    }
    private void Update()
    {
        if (moveToPositionTrigger)
        {
            agentParent.transform.position = Vector3.MoveTowards(agentParent.transform.position, destination, moveTowardDelta);
            agentParent.transform.LookAt(destination);
            if (Vector3.Distance(agentParent.transform.position, destination) < 0.5f)
            {
                agentParent.transform.LookAt(lookAtPos);
                SetEnableAgent(true);
                moveToPositionTrigger = false;
                Scream();
            }
        }
        else
        {
            SetDestination();
        }
        /*else if (agentParent.remainingDistance < 0.5f && !isEat)
        {
            agentParent.speed = speed * 2;
        }
        else if (agentParent.remainingDistance < 0.1f && !isEat)
        {
            agentParent.isStopped = true;
            Eat();
            eatCounter = Time.time + eatDuration;
            isEat = true;
        }
        else if (eatCounter < Time.time && stunCounter < Time.time)
        {
            SetDestination();
            agentParent.speed = speed;
        }*/
        //Remaindistance bug after reach start pos it's 0
    }
    public void InitWolf(Vector3 position,WolfController controller)
    {
        moveToPositionTrigger = true;
        destination = position;
        this.m_wolfController = controller;
    }
    public void SetEnableAgent(bool status)
    {
        agentParent.enabled = status;
    }
    private void Scream()
    {
        print("scream");
        Invoke("afterScream", screamDuration);
    }
    private void afterScream()
    {
        print("afterScream");
        target = m_wolfController.getChicken();
        if (target != null)
        {
            isHaveTarget = true;
            print("haveChicken");
        }
        else
        {
            print("noChicken");
        }
    }
    private void Eat()
    {
        if(target.GetComponentInChildren<Chicken>() != null)
        {
            Chicken chicken = target.GetComponentInChildren<Chicken>();
            chicken.Dead();
            m_wolfController.KillChicken(chicken.GetInstanceID());
        }
    }
    private void SetDestination()
    {
        if (isHaveTarget)
        {
            agentParent.isStopped = false;
            agentParent.SetDestination(target.transform.position);
        }
        else
        {
            agentParent.isStopped = true;
        }
    }
}
