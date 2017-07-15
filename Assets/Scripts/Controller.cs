﻿using System.Collections;
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
    bool _switching;

    delegate void InteractDelegate();

    delegate void PersonDelegate(Person p);

    InteractDelegate _dSwitchOnce;
    InteractDelegate _dInteractOnce;
    InteractDelegate _dJumpOnce;
    public Person _target;
    Person _currentHost;
    Person _inATalk;

    List<Person> _personTriggered;
    Rigidbody _hostRigidBody;
    // Use this for initialization
    void Start () 
    {
        _dJumpOnce += PlayerJump;
        _dSwitchOnce += PlayerSwitch;
        _dInteractOnce += PlayerEngage;
        if(_target == null)
        {
            Debug.LogError("Controller must be on a person");
        }
        _personTriggered = new List<Person>();
        ControlPerson(_target);  
    }

    void EngagePerson(Person person)
    {
        for (int i = _personTriggered.Count - 1; i >= 0; i--)
        {
            Person p = _personTriggered[i];
            if (p != person)
            {
                LeavePerson(p);
            }
        }
        _inATalk = person;
        person.Engage();
    }
    void PlayerJump()
    {
        _hostRigidBody.AddForce(_jumpSpeed * Vector3.up, ForceMode.Impulse);   
    }

    void PlayerSwitch()
    {
        ControlPerson(_personTriggered[0]);
    }

    void PlayerEngage()
    {
        if (_inATalk == null)
        {
            foreach (Person p in _personTriggered)
            {
                if (p._isEngageable)
                {
                    EngagePerson(p);
                    p._dFinishedTalking += PlayerDisengage;
                    return;
                }
            }
        }
    }

    void PlayerDisengage()
    {
        _inATalk = null;
    }

    void ControlPerson(Person target)
    {
        if(target == _currentHost)
        {
            Debug.LogError("Target can't be current host");
            Debug.Break();
        }

        Person oldOne = _currentHost;




        _currentHost = target;
        if(oldOne != null)
        {
            TriggerPerson(oldOne);
            oldOne.GetComponent<MeshRenderer>().material = ModelsLibrary.ML.randomMaterial;
        }
        LeavePerson(target); //target.NotNearby(gameObject);
        target.GetComponent<MeshRenderer>().material = ModelsLibrary.ML.controllerMaterial;
        _hostRigidBody = target.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () 	
	{
        if(_currentHost != null)
        {
            transform.position = _currentHost.transform.position;
    
         
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

            if (_personTriggered.Count > 0)
            {
                if (Input.GetAxis("Interact") > 0)
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

                if (Input.GetAxis("Switch") > 0)
                {
                    if (!_switching)
                    {
                        _switching = true;
                        if (_dSwitchOnce != null)
                            _dSwitchOnce();
                    }
                }
                else
                {
                    _switching = false;
                }
            }
        }
        
    }

    void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");
        float lookHorizontalAxis = Input.GetAxis("Mouse X");
        Vector3 dir = (transform.position - Camera.main.transform.position).normalized;
        _hostRigidBody.AddForce(verticalAxis * dir * _speed, ForceMode.Acceleration);
        _hostRigidBody.AddForce(horizontalAxis * Vector3.Cross(Vector3.up, dir) * _speed, ForceMode.Acceleration);
        if (Mathf.Abs(lookHorizontalAxis) >= 0.1f)
            transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, lookHorizontalAxis, 0);
    }

    void TriggerPerson(Person trigger)
    {
        if (trigger != _currentHost)
        {
            trigger.IsNearby(gameObject);
            _personTriggered.Add(trigger);
            trigger.Talk();
        }
    }

    void LeavePerson(Person trigger)
    {
        if(trigger == _inATalk)
        {
            _inATalk = null;
        }
        trigger.NotNearby(gameObject);
        _personTriggered.Remove(trigger);   
    }

    void OnTriggerEnter(Collider other)
    {
        if (_currentHost != null)
        {
            Person trigger = other.GetComponent<Person>();
            if (trigger != null && trigger != _currentHost)
            {
                TriggerPerson(trigger);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_currentHost != null)
        {
            Person trigger = other.GetComponent<Person>();
            if (trigger != null && trigger != _currentHost)
            {
                LeavePerson(trigger);
            }
        }
    }
}