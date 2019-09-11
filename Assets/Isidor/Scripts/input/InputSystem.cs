using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private static InputSystem input;

    public KeyCode defaultLeft;
    public KeyCode defaultRight;
    public KeyCode defaultJump;
    public KeyCode defaultAttack;

    public static KeyCode leftKey;
    public static KeyCode rightKey;
    public static KeyCode jumpKey;
    public static KeyCode attackKey;

    private void OnEnable()
    {
        input = this;
        ResetKeys();

        DontDestroyOnLoad(this.gameObject);
    }

    public static void ResetKeys()
    {
        SetLeft(input.defaultLeft);
        SetRight(input.defaultRight);
        SetJump(input.defaultJump);
        SetAttack(input.defaultAttack);
    }

    public static void SetLeft(KeyCode key)
    {
        leftKey = key;
    }
    public static void SetRight(KeyCode key)
    {
        rightKey = key;
    }
    public static void SetJump(KeyCode key)
    {
        jumpKey = key;
    }
    public static void SetAttack(KeyCode key)
    {
        attackKey = key;
    }

    public static bool AttackDown()
    {
        return Input.GetKeyDown(attackKey);
    }

    public static bool Attack()
    {
        return Input.GetKey(attackKey);
    }

    public static bool AttackUp()
    {
        return Input.GetKeyUp(attackKey);
    }

    public static bool LeftDown()
    {
        return Input.GetKeyDown(leftKey);
    }

    public static bool Left()
    {
        return Input.GetKey(leftKey);
    }

    public static bool LeftUp()
    {
        return Input.GetKeyUp(leftKey);
    }

    public static bool RightDown()
    {
        return Input.GetKeyDown(rightKey);
    }

    public static bool Right()
    {
        return Input.GetKey(rightKey);
    }

    public static bool RightUp()
    {
        return Input.GetKeyUp(rightKey);
    }

    public static bool JumpDown()
    {
        return Input.GetKeyDown(jumpKey);
    }

    public static bool Jump()
    {
        return Input.GetKey(jumpKey);
    }

    public static bool JumpUp()
    {
        return Input.GetKeyUp(jumpKey);
    }

    public float Horizontal()
    {
        return Right() ? 1 : Left() ? -1 : 0;
    }
}
