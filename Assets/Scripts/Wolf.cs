using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agentParent;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float moveTowardDelta;
    private GameObject target;
    private Vector3 destination;

    [SerializeField]private bool moveToPositionTrigger;

    private void Awake()
    {
        agentParent.speed = speed;
    }
    private void Update()
    {
        if(moveToPositionTrigger)
        {
            SetEnableAgent(false);
            agentParent.transform.position = Vector3.MoveTowards(agentParent.transform.position, destination, moveTowardDelta);
            transform.LookAt(destination);
            if (Vector3.Distance(agentParent.transform.position, destination) < 0.5f)
            {
                SetEnableAgent(true);
                moveToPositionTrigger = false;
                SetDestination();
            }
        }
    }
    public void MoveToPosition(Vector3 position, GameObject target)
    {
        moveToPositionTrigger = true;
        destination = position;
        this.target = target;
    }
    private void SetEnableAgent(bool status)
    {
        agentParent.enabled = status;
    }
    private void SetDestination()
    {
        agentParent.SetDestination(target.transform.position);
    }
}
