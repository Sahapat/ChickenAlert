using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceDoor : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch ((int)collision.contacts[0].normal.z)
        {
            case -1:
                anim.SetTrigger("openFront");
                break;

            case 1:
                anim.SetTrigger("openBack");
                break;
        }
    }
}
