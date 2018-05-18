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
    [SerializeField] private WolfSpawn[] wolfSpawns;
    [SerializeField] private ChickenController m_chickenController;
    [SerializeField] private Gamecontroller m_gamecontroller;

    public void KillChicken(int id)
    {
        m_chickenController.RemoveChicken(id);
    }
    public GameObject getChicken()
    {
        return m_chickenController.GetRandomChickenOnLand();
    }
    public GameObject SpawnWolf()
    {
        int spawnIndex = UnityEngine.Random.Range(0, wolfSpawns.Length);
        GameObject temp = Instantiate(WolfObj
                            ,wolfSpawns[spawnIndex].spawnAndDestination[0].position
                            , Quaternion.identity);
        Wolf wolf = temp.GetComponentInChildren<Wolf>();
        wolf.InitWolf(wolfSpawns[spawnIndex].spawnAndDestination[1].position, wolfSpawns[spawnIndex].spawnAndDestination[0].position, this);
        return temp;
    }
    public void DeleteWolf()
    {
        m_gamecontroller.currentWolf--;
    }
}
