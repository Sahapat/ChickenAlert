using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scene_ControllerSelector : MonoBehaviour
{
    private enum SelectSceneState
    {
        FIRST_SELECTED,
        SECOND_SELECTED,
        SHOW_TUTORIAl
    };

    [SerializeField] private Sprite[] showSelecteds;
    [SerializeField] private Image player1ImageRef;
    [SerializeField] private GameObject player1ObjRef;
    [SerializeField] private Image player2ImageRef;
    [SerializeField] private GameObject player2ObjRef;
    [SerializeField] private GameObject tutorialObj;
    [SerializeField] private Animator selectSceneAnim;
    [SerializeField] private Animator fadeAnim;

    private SelectSceneState currentState;

    private void Start()
    {
        selectionState();
    }
    private void Update()
    {
        switch (currentState)
        {
            case SelectSceneState.FIRST_SELECTED:

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Global.player1Selected = ControllerSelector.KEYBOARD_LEFT;
                    player1ObjRef.SetActive(true);
                    player1ImageRef.sprite = showSelecteds[0];
                    currentState = SelectSceneState.SECOND_SELECTED;
                }
                else if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    Global.player1Selected = ControllerSelector.KEYBOARD_RIGHT;
                    player1ObjRef.SetActive(true);
                    player1ImageRef.sprite = showSelecteds[0];
                    currentState = SelectSceneState.SECOND_SELECTED;

                }
                else if (Input.GetKeyDown(KeyCode.Joystick1Button7))
                {
                    Global.player1Selected = ControllerSelector.JOYSTICK_FIRST;
                    player1ObjRef.SetActive(true);
                    player1ImageRef.sprite = showSelecteds[1];
                    currentState = SelectSceneState.SECOND_SELECTED;

                }
                else if (Input.GetKeyDown(KeyCode.Joystick2Button7))
                {
                    Global.player1Selected = ControllerSelector.JOYSTICK_SECOND;
                    player1ObjRef.SetActive(true);
                    player1ImageRef.sprite = showSelecteds[1];
                    currentState = SelectSceneState.SECOND_SELECTED;

                }
                break;
            case SelectSceneState.SECOND_SELECTED:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Global.player2Selected = (Global.player1Selected == ControllerSelector.KEYBOARD_LEFT) ? ControllerSelector.KEYBOARD_RIGHT : ControllerSelector.KEYBOARD_LEFT;
                    player2ObjRef.SetActive(true);
                    player2ImageRef.sprite = showSelecteds[0];
                    currentState = SelectSceneState.SHOW_TUTORIAl;
                }
                else if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    Global.player2Selected = (Global.player1Selected == ControllerSelector.KEYBOARD_RIGHT) ? ControllerSelector.KEYBOARD_LEFT : ControllerSelector.KEYBOARD_RIGHT;
                    player2ObjRef.SetActive(true);
                    player2ImageRef.sprite = showSelecteds[0];
                    currentState = SelectSceneState.SHOW_TUTORIAl;

                }
                else if (Input.GetKeyDown(KeyCode.Joystick1Button7))
                {
                    Global.player2Selected = (Global.player1Selected == ControllerSelector.JOYSTICK_FIRST) ? ControllerSelector.JOYSTICK_SECOND : ControllerSelector.JOYSTICK_FIRST;
                    player2ObjRef.SetActive(true);
                    player2ImageRef.sprite = showSelecteds[1];
                    currentState = SelectSceneState.SHOW_TUTORIAl;
                }
                else if (Input.GetKeyDown(KeyCode.Joystick2Button7))
                {
                    Global.player2Selected = (Global.player1Selected == ControllerSelector.JOYSTICK_SECOND) ? ControllerSelector.JOYSTICK_FIRST : ControllerSelector.JOYSTICK_SECOND;
                    player2ObjRef.SetActive(true);
                    player2ImageRef.sprite = showSelecteds[1];
                    currentState = SelectSceneState.SHOW_TUTORIAl;
                }
                break;
            case SelectSceneState.SHOW_TUTORIAl:
                if (!tutorialObj.activeSelf) tutorialState();
                else if (Input.anyKey) StartCoroutine(LoadScene());
                break;
        }
    }
    private void selectionState()
    {
        player1ObjRef.SetActive(false);
        player2ObjRef.SetActive(false);
        tutorialObj.SetActive(false);
        selectSceneAnim.SetTrigger("Show");
    }
    private void tutorialState()
    {
        tutorialObj.SetActive(true);
        selectSceneAnim.SetTrigger("Close");
    }
    private IEnumerator LoadScene()
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
