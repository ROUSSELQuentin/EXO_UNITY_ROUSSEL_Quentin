using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool _jumpInput;
    public bool _dashInput;
    public float _moveInput;
    public bool _runInput;

    void Start()
    {
    }

    public void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        _jumpInput = Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space);
        _dashInput = Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.Z);
        _runInput = Input.GetKeyDown(KeyCode.Joystick1Button11) || Input.GetKeyDown(KeyCode.LeftShift);

        if (_jumpInput)
        {
            Debug.Log("Jump");
        }

        if (_dashInput)
        {
            Debug.Log("Dash");
        }

        if (_moveInput != 0)
        {
            Debug.Log("Deplacement " + _moveInput);
        }

        if (_runInput)
        {
            Debug.Log("Run");
        }

    }
}



