using UnityEngine;

public class JoystickInputController : MonoBehaviour
{
    public static Vector2 GetMovement(ControllerSelector playerData)
    {
        float x = 0;
        float y = 0;
        switch (playerData)
        {
            case ControllerSelector.KEYBOARD_LEFT:
                if (Input.GetKey(KeyCode.A))
                {
                    x = -1;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    x = 1;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    y = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    y = -1;
                }
                break;
            case ControllerSelector.KEYBOARD_RIGHT:
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    x = -1;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    x = 1;
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    y = 1;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    y = -1;
                }
                break;
            case ControllerSelector.JOYSTICK_FIRST:
                x = Input.GetAxisRaw("Horizontal1");
                y = Input.GetAxisRaw("Vertical1");
                break;
            case ControllerSelector.JOYSTICK_SECOND:
                x = Input.GetAxisRaw("Horizontal2");
                y = Input.GetAxisRaw("Vertical2");
                break;
        }
        return new Vector2(x, y);
    }
    public static bool GetUse(ControllerSelector playerData)
    {
        switch (playerData)
        {
            case ControllerSelector.KEYBOARD_LEFT:
                return Input.GetKeyDown(KeyCode.H);
            case ControllerSelector.KEYBOARD_RIGHT:
                return Input.GetKeyDown(KeyCode.Keypad5);
            case ControllerSelector.JOYSTICK_FIRST:
                return Input.GetKeyDown(KeyCode.Joystick1Button2);
            case ControllerSelector.JOYSTICK_SECOND:
                return Input.GetKeyDown(KeyCode.Joystick2Button2);
            default: return false;
        }
    }
    public static bool GetPush(ControllerSelector playerData)
    {
        switch (playerData)
        {
            case ControllerSelector.KEYBOARD_LEFT:
                return Input.GetKeyDown(KeyCode.G);
            case ControllerSelector.KEYBOARD_RIGHT:
                return Input.GetKeyDown(KeyCode.KeypadPlus);
            case ControllerSelector.JOYSTICK_FIRST:
                return Input.GetKeyDown(KeyCode.Joystick1Button1);
            case ControllerSelector.JOYSTICK_SECOND:
                return Input.GetKeyDown(KeyCode.Joystick2Button1);
            default: return false;
        }
    }
}