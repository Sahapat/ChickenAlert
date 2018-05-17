using System.Collections;
using System;
using UnityEngine.AI;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Serializable]
    private class ClampZone
    {
        public ClampZone(Vector2 max,Vector2 min)
        {
            this.max = max;
            this.min = min;
        }
        public Vector2 max;
        public Vector2 min;
    }
    [SerializeField] private NavMeshAgent agentParent;
    [SerializeField] private Animator anim;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float alertSpeed;
    [SerializeField] private float jumpScale;
    [SerializeField] private int jumpFrame;
    [SerializeField] private float AlertPositionRange;
    [SerializeField] private float idlePositionRange;
    [SerializeField] private ClampZone landZone;
    [SerializeField] private ClampZone safeZone;
    [SerializeField] private float safeRadius;
    [SerializeField] private float landRadius;

    public bool isAlert;
    public bool isSafe;
    public bool isOnpush;
    private bool isDead;

    private float timeCounter;
    private float clamCounter;
    private bool ResetAlertTrigger;
    private WaitForSeconds wait;
    private Vector3 targetPos;

    private void Awake()
    {
        wait = new WaitForSeconds(Time.deltaTime);
        ResetAlertTrigger = true;
    }
    private void Update()
    {
        if(isDead)
        {
            agentParent.isStopped = true;
            return;
        }
        if (!isOnpush)
        {
            agentParent.isStopped = false;
            if (isSafe)
            {
                Safe();
                isAlert = false;
            }
            else if (isAlert)
            {
                Alert();
                if(ResetAlertTrigger)
                {
                    clamCounter = Time.time + 30;
                    ResetAlertTrigger = false;
                }
            }
            else
            {
                Idle();
            }

            if (clamCounter < Time.time)
            {
                isAlert = false;
            }
        }
        else
        {
            StopAllCoroutines();
            isAlert = true;
            agentParent.isStopped = true;
            ResetAlertTrigger = true;
            anim.SetBool("isRunning", true);
        }
    }
    
    public void Dead()
    {
        anim.SetTrigger("Die");
        isDead = true;
        Destroy(this.gameObject, 3f);
    }
    private void Alert()
    {
        agentParent.radius = landRadius;
        agentParent.speed = alertSpeed;
        if (agentParent.remainingDistance < 0.2f)
        {
            anim.SetBool("isRunning", true);
            anim.speed = 0.5f;
            FindPosition(AlertPositionRange);
            agentParent.SetDestination(targetPos);
            StopAllCoroutines();
            StartCoroutine(Jump());
        }
    }
    private void Idle()
    {
        agentParent.radius = landRadius;
        agentParent.speed = normalSpeed;
        if (agentParent.remainingDistance < 0.2f && timeCounter < Time.time)
        {
            anim.SetBool("isRunning", true);
            anim.speed = 1f;
            FindPosition(idlePositionRange);
            agentParent.SetDestination(targetPos);
            timeCounter = Time.time + 2.5f;
        }
        else if(agentParent.remainingDistance < 0.2f)
        {
            anim.SetBool("isRunning", false);
        }
    }
    private void Safe()
    {
        agentParent.radius = safeRadius;
        agentParent.speed = normalSpeed;
        if ((agentParent.remainingDistance < 0.2f && timeCounter < Time.time)) 
        {
            anim.SetBool("isRunning", true);
            anim.speed = 1f;
            float randX = UnityEngine.Random.Range(safeZone.min.x, safeZone.max.x);
            float randZ = UnityEngine.Random.Range(safeZone.min.y, safeZone.max.y);
            
            targetPos = new Vector3(randX,0,randZ);
            agentParent.SetDestination(targetPos);
            timeCounter = Time.time+6;
        }
        else if(agentParent.remainingDistance < 0.2f)
        {
            anim.SetBool("isRunning", false);
        }
    }
    private void FindPosition(float distance)
    {
        Vector3 randomPosition = transform.position + UnityEngine.Random.insideUnitSphere * distance;
        float destinationX = Mathf.Clamp(randomPosition.x, landZone.min.x, landZone.max.x);
        float destinationY = 0;
        float destinationZ = Mathf.Clamp(randomPosition.z, landZone.min.y, landZone.max.y);
        targetPos = new Vector3(destinationX, destinationY, destinationZ);
        agentParent.SetDestination(targetPos);
    }
    private IEnumerator Jump()
    {
        for (int i = 0; i < jumpFrame; i++)
        {
            transform.Translate(new Vector3(0, jumpScale * Time.deltaTime, 0));
            yield return wait;
        }
        for (int i = 0; i < jumpFrame; i++)
        {
            transform.Translate(new Vector3(0, -jumpScale * Time.deltaTime, 0));
            yield return wait;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, 0.5f, transform.localPosition.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SafeZone"))
        {
            isSafe = true;
            agentParent.destination = transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            isSafe = false;
            agentParent.destination = transform.position;
        }
    }
}
