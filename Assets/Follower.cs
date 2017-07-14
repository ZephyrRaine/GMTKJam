using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

    [SerializeField]
    Transform _target;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            transform.LookAt(_target);

            transform.position = _target.transform.forward * -10.0f;

        }
    }
}
