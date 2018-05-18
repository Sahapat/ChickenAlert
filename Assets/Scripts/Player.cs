using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerId;
    [SerializeField] private GameObject agentParent;
    [SerializeField] private float Maxspeed = 5f;
    [SerializeField] private float stunDuration = 6f;
    [SerializeField] private bool isJoy;
    [SerializeField] private Animator anim;
    [SerializeField] private Gamecontroller m_gamecontroller;
    [SerializeField] private ChickenController m_chickenController;
    [SerializeField] private ItemController m_itemcontroller;
    [SerializeField] private GameObject plank;

    private bool isPush;
    private bool isStun;
    private float stunCount;
    private float speedUpCount;
    private float speed;
    private float currentSpeed;
    private GameObject pushObject;
    private Item collectObject;
    public Item inventory;
    private GameObject currentPush;
    [SerializeField] private AudioClip hitClip;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        plank.SetActive(false);
    }
    private void Update()
    {
        if (m_gamecontroller.IsGameStart)
        {
            if(speedUpCount < Time.time)
            {
                currentSpeed = Maxspeed;
            }
            if (!isStun)
            {
                if (JoystickInputController.GetUse(playerId, isJoy))
                {
                    if (inventory != null)
                    {
                        switch (inventory.itemType)
                        {
                            case ItemType.Plank:
                                inventory.Use(agentParent.transform.position + agentParent.transform.forward, agentParent.gameObject);
                                StartCoroutine(PlankVisible());
                                m_itemcontroller.currentPlank--;
                                audioSource.PlayOneShot(hitClip);
                                Destroy(inventory.gameObject);
                                break;
                            case ItemType.Bomb:
                                inventory.Use(agentParent.transform.forward);
                                break;
                            case ItemType.Boots:
                                inventory.Use(ref currentSpeed, out speedUpCount);
                                Destroy(inventory.gameObject);
                                break;
                            case ItemType.Hay:
                                inventory.Use(m_chickenController);
                                Destroy(inventory.gameObject);
                                break;
                        }

                        anim.SetTrigger("Hit");
                        inventory = null;
                    }
                    else
                    {
                        if(collectObject != null)
                        {
                            anim.SetTrigger("Get");
                            collectObject.Collect(agentParent.gameObject);
                            inventory = collectObject;
                        }
                    }
                }
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
                        anim.SetBool("isSheepHed", true);
                        anim.SetBool("isRunning", false);
                        chicken.whoPush = this.gameObject;
                        speed = currentSpeed * 0.4f;
                    }
                    else if (currentPush != null)
                    {
                        try
                        {
                            currentPush.transform.parent = null;
                            Chicken chicken = currentPush.GetComponentInChildren<Chicken>();
                            chicken.isOnpush = false;
                            isPush = false;
                            anim.SetBool("isSheepHed", false);
                            chicken.whoPush = null;
                            speed = currentSpeed;
                        }
                        catch (System.NullReferenceException)
                        {
                            isPush = false;
                            anim.SetBool("isSheepHed", false);
                            speed = currentSpeed;
                        }
                    }
                }
                else if (currentPush != null)
                {
                    try
                    {
                        currentPush.transform.parent = null;
                        Chicken chicken = currentPush.GetComponentInChildren<Chicken>();
                        chicken.isOnpush = false;
                        isPush = false;
                        anim.SetBool("isSheepHed", false);
                        chicken.whoPush = null;
                        speed = currentSpeed;
                    }
                    catch (System.NullReferenceException)
                    {
                        isPush = false;
                        anim.SetBool("isSheepHed", false);
                        speed = currentSpeed;
                    }
                }
                else
                {
                    speed = currentSpeed;
                }
            }
            else
            {
                try
                {
                    currentPush.transform.parent = null;
                    Chicken chicken = currentPush.GetComponentInChildren<Chicken>();
                    chicken.isOnpush = false;
                    isPush = false;
                    anim.SetBool("isSheepHed", false);
                    chicken.whoPush = null;
                    speed = currentSpeed;
                }
                catch (System.NullReferenceException)
                {
                    isPush = false;
                    anim.SetBool("isSheepHed", false);
                    speed = currentSpeed;
                }
                if (stunCount < Time.time)
                {
                    isStun = false;
                    anim.SetBool("isStun", false);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (m_gamecontroller.IsGameStart)
        {
            if (!isStun)
            {
                Movement(JoystickInputController.GetMovement(playerId, isJoy));
            }
        }
    }
    public void setProperty(int playerId, bool isJoy)
    {
        this.playerId = playerId;
        this.isJoy = isJoy;
    }
    public void Stun()
    {
        isStun = true;
        anim.SetBool("isStun", true);
        anim.SetTrigger("Stun");
        stunCount = Time.time + stunDuration;
    }
    private void Movement(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            agentParent.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (!isPush)
            {
                anim.SetBool("isRunning", true);
            }
        }
        else
        {
            if (!isPush)
            {
                anim.SetBool("isRunning", false);
            }
        }

        var target = agentParent.transform.position + movement;
        var relativeVector = (target - agentParent.transform.position).normalized;
        var radian = Mathf.Atan2(relativeVector.x, relativeVector.y);
        var degree = (radian * 180) / Mathf.PI;

        if (movement != Vector3.zero)
        {
            agentParent.transform.eulerAngles = new Vector3(0, degree, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chicken") && !isPush)
        {
            pushObject = other.transform.parent.gameObject;
        }
        else if(other.CompareTag("Item"))
        {
            collectObject = other.gameObject.GetComponent<Item>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chicken"))
        {
            pushObject = null;
        }
        else if (other.CompareTag("Item"))
        {
            collectObject = null;
        }
    }
    private IEnumerator PlankVisible()
    {
        plank.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        plank.SetActive(false);
    }
}
