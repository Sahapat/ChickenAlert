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
    private bool isHaveChicken;
    private bool isChasing;
    private bool isChaseChicken;
    private bool isFear;
    private bool completeInit;
    private int life = 2;

    private WolfController m_wolfController;
    private GameObject target;
    private Vector3 destination;
    private Vector3 startPoint;
    private bool moveToPositionTrigger;
    private AudioSource audioSource;
    [SerializeField] private AudioClip WolfScream;
    [SerializeField] private AudioClip WolfHit;
    private void Awake()
    {
        agentParent.speed = speed;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.PlayOneShot(WolfScream);
    }
    private void Update()
    {
        if (life <= 0)
        {
            anim.SetBool("isRunning", true);
            if (agentParent.enabled)
            {
                agentParent.isStopped = false;
                agentParent.SetDestination(destination);
                agentParent.speed = 7f;
                if (agentParent.remainingDistance < 0.2f)
                {
                    agentParent.enabled = false;
                }
            }
            else
            {
                agentParent.transform.position = Vector3.MoveTowards(agentParent.transform.position, startPoint, moveTowardDelta * 2);
                agentParent.transform.LookAt(startPoint);
                if (Vector3.Distance(agentParent.transform.position, startPoint) < 1)
                {
                    m_wolfController.DeleteWolf();
                    Destroy(agentParent.gameObject);
                }
            }
            return;
        }
        if (!isFear)
        {
            if (isChaseChicken)
            {
                Chicken chicken = target.GetComponentInChildren<Chicken>();
                if (chicken.isSafe)
                {
                    target = null;
                    isChaseChicken = false;
                    isHaveTarget = false;
                    isChasing = false;
                    agentParent.isStopped = true;
                }
                agentParent.speed = speed;
            }
            else
            {
                agentParent.speed = 5.8f;
            }
            if (isEat)
            {
                if (eatCounter < Time.time)
                {
                    isEat = false;
                }
                else
                {
                    return;
                }
            }
            if (completeInit)
            {
                if (target == null && isHaveChicken)
                {
                    isHaveTarget = false;
                    completeInit = false;
                    anim.SetBool("isRunning", false);
                    Invoke("Scream", eatDuration);
                }
            }

            if (isChasing)
            {
                if (isChaseChicken)
                {
                    Chicken chicken = target.GetComponent<Chicken>();
                    if (chicken.isOnpush)
                    {
                        if (agentParent.remainingDistance < 2.5f)
                        {
                            target = chicken.whoPush;
                            isChaseChicken = false;
                            if (!isEat)
                            {
                                agentParent.transform.LookAt(target.transform);
                                if (isChaseChicken)
                                {
                                    anim.SetBool("isEat", true);
                                }
                                else
                                {
                                    anim.SetBool("isEat", false);
                                }
                                anim.SetTrigger("Jump");
                                isChasing = false;
                                Eat();
                                isEat = true;
                                eatCounter = Time.time + eatDuration;
                            }
                        }
                    }
                    else if (agentParent.remainingDistance < 1.5f)
                    {
                        if (!isEat)
                        {
                            agentParent.transform.LookAt(target.transform);
                            if (isChaseChicken)
                            {
                                anim.SetBool("isEat", true);
                            }
                            else
                            {
                                anim.SetBool("isEat", false);
                            }
                            anim.SetTrigger("Jump");
                            isChasing = false;
                            Eat();
                            isEat = true;
                            eatCounter = Time.time + eatDuration;
                        }
                    }
                }
                else
                {
                    if (agentParent.remainingDistance < 1.2f)
                    {
                        if (!isEat)
                        {
                            agentParent.transform.LookAt(target.transform);
                            if (isChaseChicken)
                            {
                                anim.SetBool("isEat", true);
                            }
                            else
                            {
                                anim.SetBool("isEat", false);
                            }
                            anim.SetTrigger("Jump");
                            isChasing = false;
                            Eat();
                            isEat = true;
                            eatCounter = Time.time + eatDuration;
                        }
                    }
                }
            }
            if (moveToPositionTrigger)
            {
                anim.SetBool("isRunning", true);
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
            else if (completeInit)
            {
                SetDestination();
            }
        }
    }
    public void Hit(GameObject Chase)
    {
        isFear = true;
        agentParent.isStopped = true;
        Scream(Chase);
        life--;
    }
    public void TakeOut()
    {
        life = 0;
    }
    public void InitWolf(Vector3 endPosition, Vector3 startPostion, WolfController controller)
    {
        moveToPositionTrigger = true;
        destination = endPosition;
        startPoint = startPostion;
        this.m_wolfController = controller;
    }
    public void SetEnableAgent(bool status)
    {
        agentParent.enabled = status;
    }
    private void Scream()
    {
        anim.SetTrigger("Scream");
        anim.SetBool("isRunning", false);
        Invoke("AfterScream", screamDuration);
    }
    public void Scream(GameObject chaseObj)
    {
        anim.SetTrigger("Scream");
        anim.SetBool("isRunning", false);
        target = chaseObj;
        Invoke("AfterScreamOnPlayer", screamDuration);
    }
    private void AfterScreamOnPlayer()
    {
        if (target != null)
        {
            isChaseChicken = false;
            isHaveTarget = true;
        }
        completeInit = true;
        isFear = false;
    }
    private void AfterScream()
    {
        target = m_wolfController.getChicken();
        if (target != null)
        {
            isChaseChicken = true;
            isHaveTarget = true;
            isHaveChicken = true;
        }
        else
        {
            isHaveChicken = false;
        }
        completeInit = true;
    }
    private void Eat()
    {
        if (target != null)
        {
            if (isChaseChicken)
            {
                Chicken chicken = target.GetComponentInChildren<Chicken>();
                chicken.Dead();
                m_wolfController.KillChicken(chicken.GetInstanceID());
                target = null;
                isHaveTarget = false;
            }
            else
            {
                Player player = target.GetComponentInChildren<Player>();
                player.Stun();
                target = null;
                isHaveChicken = true;
            }
        }
    }
    private void SetDestination()
    {
        if (isHaveTarget && !isEat)
        {
            agentParent.isStopped = false;
            agentParent.SetDestination(target.transform.position);
            anim.SetBool("isRunning", true);
            isChasing = true;
        }
        else
        {
            agentParent.isStopped = true;
            anim.SetBool("isRunning", false);
            isChasing = false;
            isChaseChicken = false;
        }
    }
}
