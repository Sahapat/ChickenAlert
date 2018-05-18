using UnityEngine;

public class JoystickInputController : MonoBehaviour
{
    public static Vector3 GetMovement(int playerId,bool isJoy)
    {
        float x = 0;
        float y = 0;
        Vector3 movement = Vector3.zero;
        if(isJoy)
        {
            x = Input.GetAxisRaw("Horizontal" + playerId);
            y = Input.GetAxisRaw("Vertical" + playerId);
        }
        else
        {
            switch(playerId)
            {
                case 1:
                    if(Input.GetKey(KeyCode.A))
                    {
                        x = -1;
                    }
                    else if(Input.GetKey(KeyCode.D))
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
                case 2:
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
            }
        }
        movement = new Vector3(x, y,0);
        return movement;
    }
    public static bool GetUse(int playerId,bool isJoy)
    {
        bool isPress = false;
        switch(playerId)
        {
            case 1:
                if(isJoy)
                {
                    isPress = Input.GetKeyDown(KeyCode.Joystick1Button2);
                }
                else
                {
                    isPress = Input.GetKeyDown(KeyCode.H);
                }
                break;
            case 2:
                if (isJoy)
                {
                    isPress = Input.GetKeyDown(KeyCode.Joystick2Button2);
                }
                else
                {
                    isPress = Input.GetKeyDown(KeyCode.Keypad5);
                }
                break;
        }
        return isPress;
    }
    public static bool GetPush(int playerId,bool isJoy)
    {
        bool isPress = false;
        switch(playerId)
        {
            case 1:
                if (isJoy)
                {
                    isPress =  Input.GetKey(KeyCode.Joystick1Button1);
                }
                else
                {
                    isPress = Input.GetKey(KeyCode.G);
                }
                break;
            case 2:
                if (isJoy)
                {
                    isPress = Input.GetKey(KeyCode.Joystick2Button1);
                }
                else
                {
                    isPress = Input.GetKey(KeyCode.KeypadPlus);
                }
                break;
        }
        return isPress;
    }
}