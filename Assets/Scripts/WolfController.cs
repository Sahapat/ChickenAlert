using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WolfController : MonoBehaviour
{
    [Serializable]
    private class WolfSpawn
    {
        public WolfSpawn(Transform spawn,Transform destination)
        {
            spawnAndDestination = new Transform[2];
            spawnAndDestination[0] = spawn;
            spawnAndDestination[1] = destination;
        }
        public Transform[] spawnAndDestination;
    }
    [SerializeField] private GameObject WolfObj;
}
