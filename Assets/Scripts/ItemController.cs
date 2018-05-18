using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private GameObject plankObj;
    private float plankCount;
    [SerializeField]private int maxPlank;
    public int currentPlank;
    [SerializeField] private float plankDuration;
    [SerializeField] private GameObject bombObj;
    private float bombCount;
    [SerializeField]private int maxBomb;
    public int currentBomb;
    [SerializeField] private float bombDuration;
    [SerializeField] private GameObject hayObj;
    [SerializeField] private int maxHay;
    private float hayCount;
    public int currentHay;
    [SerializeField] private float hayDuration;
    [SerializeField] private GameObject bootsObj;
    [SerializeField] private int maxBoot;
    public int currentBoot;
    private float bootsCount;
    [SerializeField] private float bootsDuration;
    [Serializable]
    private class SpawnFrame
    {
        public Vector2 max;
        public Vector2 min;
    }

    [SerializeField] private SpawnFrame spawnFrame;
    [SerializeField] private Gamecontroller m_gamecontroller;
    private void Start()
    {
        plankCount = Time.time + plankDuration;
        bombCount = Time.time + bombDuration;
        hayCount = Time.time + hayDuration;
        bootsCount = Time.time + bootsDuration;
    }
    private void Update()
    {
        if (m_gamecontroller.IsGameStart)
        {
            if (plankCount < Time.time)
            {
                if (currentPlank < maxPlank)
                {
                    float spawnX = UnityEngine.Random.Range(spawnFrame.min.x, spawnFrame.max.x);
                    float spawnZ = UnityEngine.Random.Range(spawnFrame.min.y, spawnFrame.max.y);

                    GameObject temp = Instantiate(plankObj, new Vector3(spawnX, 1.5f, spawnZ), Quaternion.identity);
                    currentPlank++;
                    plankCount = Time.time + plankDuration;
                }
            }

            if (bombCount < Time.time)
            {
                if (currentBomb < maxBomb)
                {
                    if (UnityEngine.Random.value < 0.3f)
                    {
                        float spawnX = UnityEngine.Random.Range(spawnFrame.min.x, spawnFrame.max.x);
                        float spawnZ = UnityEngine.Random.Range(spawnFrame.min.y, spawnFrame.max.y);

                        GameObject temp = Instantiate(bombObj, new Vector3(spawnX, 1.5f, spawnZ), Quaternion.identity);
                        currentBomb++;
                        bombCount = Time.time + bombDuration;
                    }
                    else
                    {
                        bombCount = Time.time + UnityEngine.Random.Range(bombDuration / 2, bombDuration);
                    }
                }
            }

            if (hayCount < Time.time)
            {
                if (currentHay < maxHay)
                {
                    if (UnityEngine.Random.value < 0.4f)
                    {
                        float spawnX = UnityEngine.Random.Range(spawnFrame.min.x, spawnFrame.max.x);
                        float spawnZ = UnityEngine.Random.Range(spawnFrame.min.y, spawnFrame.max.y);

                        GameObject temp = Instantiate(hayObj, new Vector3(spawnX, 1.5f, spawnZ), Quaternion.identity);
                        currentHay++;
                        hayCount = Time.time + hayDuration;
                    }
                    else
                    {
                        hayCount = Time.time + UnityEngine.Random.Range(hayDuration / 2, hayDuration);
                    }
                }
            }

            if (bootsCount < Time.time)
            {
                if (currentBoot < maxBoot)
                {
                    if (UnityEngine.Random.value < 0.6f)
                    {
                        float spawnX = UnityEngine.Random.Range(spawnFrame.min.x, spawnFrame.max.x);
                        float spawnZ = UnityEngine.Random.Range(spawnFrame.min.y, spawnFrame.max.y);

                        GameObject temp = Instantiate(bootsObj, new Vector3(spawnX, 1.5f, spawnZ), Quaternion.identity);
                        currentBoot++;
                        bootsCount = Time.time + bootsDuration;
                    }
                    else
                    {
                        bootsCount = Time.time + UnityEngine.Random.Range(bootsDuration / 2, bootsDuration);
                    }
                }
            }
        }
    }
}
