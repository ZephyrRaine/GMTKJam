using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour 
{
    [SerializeField]
    float _speed;
	[SerializeField]
    float _jumpSpeed;

    bool _jumping;
    bool _interacting;

    public delegate void InteractDelegate();
    public InteractDelegate _dSwitch;
    public InteractDelegate _dInteractOnce;
    public InteractDelegate _dJumpOnce;
    // Use this for initialization
    void Start () 
    {
        _dJumpOnce += PlayerJump;
    }

    void PlayerJump()
    {
            GetComponent<Rigidbody>().AddForce(_jumpSpeed * Vector3.up, ForceMode.Impulse);   
    }

    void InteractOnce()
    {
        if(!_interacting)
        {
            _interacting = true;
        }
    }
	
	// Update is called once per frame
	void Update () 	
	{
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().AddForce(verticalAxis * Vector3.forward*_speed, ForceMode.Acceleration);
        GetComponent<Rigidbody>().AddForce(horizontalAxis * Vector3.right*_speed, ForceMode.Acceleration);
        Debug.Log(Input.GetAxis("Jump"));
        if (Input.GetAxis("Jump") > 0)
        {
            if (!_jumping)
            {
                _jumping = true;
                if (_dJumpOnce != null)
                    _dJumpOnce();
            }
        }
        else
        {
            _jumping = false;
        }


        if(Input.GetAxis("Interact") > 0)
        {
            if (!_interacting)
            {
                _interacting = true;
                if (_dInteractOnce != null)
                    _dInteractOnce();
            }
        }
        else
        {
            _interacting = false;
        }
    }
}
