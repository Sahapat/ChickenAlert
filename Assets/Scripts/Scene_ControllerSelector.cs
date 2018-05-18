using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scene_ControllerSelector : MonoBehaviour
{
    [SerializeField] private Sprite[] showSelected;
    [SerializeField] private Image player1Image;
    [SerializeField] private GameObject player1Obj;
    [SerializeField] private Image player2Image;
    [SerializeField] private GameObject player2Obj;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator anim_guide;

    private bool isPlayer1Select;
    private bool isFinish;

    private void Start()
    {
        player1Obj.SetActive(false);
        player2Obj.SetActive(false);
    }
    private void Update()
    {
        if (isFinish)
        {
            anim_guide.SetTrigger("Close");
            Invoke("toGameScene", 2f);
            return;
        }

        if (!isPlayer1Select)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Global.player1Id = 1;
                Global.player1Isjoy = false;
                isPlayer1Select = true;
                player1Obj.SetActive(true);
                player1Image.sprite = showSelected[0];
            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Global.player1Id = 2;
                Global.player1Isjoy = false;
                isPlayer1Select = true;
                player1Obj.SetActive(true);
                player1Image.sprite = showSelected[0];
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                Global.player1Id = 1;
                Global.player1Isjoy = true;
                isPlayer1Select = true;
                player1Obj.SetActive(true);
                player1Image.sprite = showSelected[1];
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button7))
            {
                Global.player1Id = 2;
                Global.player1Isjoy = true;
                isPlayer1Select = true;
                player1Obj.SetActive(true);
                player1Image.sprite = showSelected[1];
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Global.player2Id = 1;
                Global.player2Isjoy = false;
                player2Obj.SetActive(true);
                player2Image.sprite = showSelected[0];
                isFinish = true;
            }
            else if(Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Global.player2Id = 2;
                Global.player2Isjoy = false;
                player2Obj.SetActive(true);
                player2Image.sprite = showSelected[0];
                isFinish = true;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                Global.player2Id = 1;
                Global.player2Isjoy = true;
                player2Obj.SetActive(true);
                player2Image.sprite = showSelected[1];
                isFinish = true;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick2Button7))
            {
                Global.player2Id = 2;
                Global.player2Isjoy = true;
                player2Obj.SetActive(true);
                player2Image.sprite = showSelected[1];
                isFinish = true;
            }
        }
    }
    private void toGameScene()
    {
        StartCoroutine(LoadScene());
    }
    private IEnumerator LoadScene()
    {
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
