using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Bomb,
    Plank,
    Boots,
    Hay,
    None
};

public class Item : MonoBehaviour
{
    public ItemType itemType;
    [SerializeField] private float BootEffectDuration;
    [SerializeField] private float HayEffectDuration;
    [SerializeField] private float BombExpoDuration;
    [SerializeField] private float BombShootSpeed;
    [SerializeField] private LayerMask wolfMask;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject parentObject;

    private bool isUseBomb;
    private Vector3 shootDirection;
    private float bombExpoCount;

    private void Update()
    {
        if(isUseBomb)
        {
            var hit = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1), Quaternion.identity, wolfMask);
            if (hit.Length > 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    Wolf wolf = hit[i].gameObject.GetComponent<Wolf>();
                    wolf.TakeOut();
                }
                Destroy(this.gameObject);
            }
        }
        if(isUseBomb && !(bombExpoCount<Time.time))
        {
            transform.Translate(shootDirection * Time.deltaTime * BombShootSpeed);
        }
        else if(isUseBomb)
        {
            var hit = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1), Quaternion.identity, wolfMask);
            if (hit.Length > 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    Wolf wolf = hit[i].gameObject.GetComponent<Wolf>();
                    wolf.TakeOut();
                }
            }
            Destroy(this.gameObject);
        }
    }
    public void Use(Vector3 forwardDirection)
    {
        isUseBomb = true;
        shootDirection = forwardDirection;
        transform.parent = null;
        transform.eulerAngles = Vector3.zero;
        bombExpoCount = Time.time + BombExpoDuration;
        meshRenderer.enabled = true;
    }
    public void Use(Vector3 forwardDirection,GameObject whoSend)
    {
        var hit = Physics.OverlapBox(forwardDirection, new Vector3(1,1,1)*0.5f,Quaternion.identity,wolfMask);
        if (hit.Length > 0)
        {
            GameObject temp = Instantiate(hitParticle, transform.position, Quaternion.identity);
            temp.GetComponent<ParticleSystem>().Play();
            Wolf wolf = hit[0].gameObject.GetComponent<Wolf>();
            wolf.Hit(whoSend);
            Destroy(temp, 5f);
        }
    }
    public void Use(ref float speed,out float endTime)
    {
        speed *= 1.5f;
        endTime = Time.time + BootEffectDuration;
    }
    public void Use(ChickenController chicken)
    {
        chicken.SetClam(Time.time + HayEffectDuration);
    }
    public void Collect(GameObject parent)
    {
        meshRenderer.enabled = false;
        transform.parent = parent.transform;
        Destroy(parentObject);
    }
}
