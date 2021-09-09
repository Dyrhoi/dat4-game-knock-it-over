using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public float power = 6;
    
    private float _distToGround;
    private bool _mouseHeldDown;

    private Vector3 _mForce;
    private Vector3 _mStartPosition;
    private Vector3 _mEndPosition;

    // Start is called before the first frame update
    void Start()
    {
        _distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGrounded())
        {
            GetComponent<Renderer>().material.color  = Color.red;
            return;
        }
        GetComponent<Renderer>().material.color = Color.gray;
        
        if (Input.GetMouseButton(0))
        {
            // The mouse button is pressed;
            if (!_mouseHeldDown)
            {
                _mStartPosition = GetMousePosition();
            }
            _mouseHeldDown = true;
        }
        else
        {
            // If the mouse button was released
            if (_mouseHeldDown)
            {
                _mEndPosition = GetMousePosition();
                
                // Normalize force
                _mForce = new Vector3(_mStartPosition.x - _mEndPosition.x, (_mStartPosition.y - _mEndPosition.y) / 2, _mStartPosition.y - _mEndPosition.y) / power;
                
                GetComponent<Rigidbody>().AddForce(_mForce, ForceMode.Impulse);
            }
            _mouseHeldDown = false;
        }
    }
    
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, _distToGround + 0.1f);
    }

    Vector3 GetMousePosition()
    {
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }
}
