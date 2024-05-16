using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext
{
    public bool _dashPressed;
    public bool _jumpPressed;
    public float _runDirection;

    public PlayerContext(bool dashPressed, bool jumpPressed, float runDirection)
    {
        _dashPressed = dashPressed;
        _jumpPressed = jumpPressed;
        _runDirection = runDirection;
    }
}
