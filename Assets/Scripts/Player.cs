using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private byte playerId;
    [SerializeField] private GameObject agentParent;
    [SerializeField] private float Maxspeed = 5f;
    [SerializeField] private bool isJoy;
    private bool isPush;
    private float speed;
    [SerializeField]private GameObject pushObject;
    private GameObject currentPush;
    private void Update()
    {
        if (pushObject != null)
        {
            if (JoystickInputController.GetPush(playerId, isJoy))
            {
                pushObject.transform.parent = this.transform;
                currentPush = pushObject;
                pushObject.transform.Find("Chicken").transform.localPosition = new Vector3(0, 0.5f, 0);
                Chicken chicken = pushObject.GetComponentInChildren<Chicken>();
                chicken.isOnpush = true;
                isPush = true;
                speed = Maxspeed * 0.4f;
            }
            else if (currentPush != null)
            {
                currentPush.transform.parent = null;
                Chicken chicken = currentPush.GetComponentInChildren<Chicken>();
                chicken.isOnpush = false;
                isPush = false;
                speed = Maxspeed;
            }
        }
        else if (currentPush != null)
        {
            currentPush.transform.parent = null;
            Chicken chicken = currentPush.GetComponentInChildren<Chicken>();
            chicken.isOnpush = false;
            isPush = false;
            speed = Maxspeed;
        }
        else
        {
            speed = Maxspeed;
        }
    }
    private void FixedUpdate()
    {
        Movement(JoystickInputController.GetMovement(playerId,isJoy));
    }
    private void Movement(Vector2 movement)
    {
        agentParent.transform.Translate(new Vector3(movement.x * speed * Time.deltaTime, 0, movement.y * speed * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Chicken") && !isPush)
        {
            pushObject = other.transform.parent.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chicken"))
        {
            pushObject = null;
        }
    }
}
