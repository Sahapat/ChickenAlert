using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject agentParent;
    [SerializeField] private float speed = 5f;
    private bool isGround;
    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Movement(moveX,moveZ);
    }
    private void Movement(float moveX,float moveZ)
    {
        agentParent.transform.Translate(new Vector3(moveX * speed * Time.deltaTime, 0, moveZ * speed * Time.deltaTime));
    }
}
