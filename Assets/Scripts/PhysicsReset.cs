using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsReset : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if(player)
        {
            if (other.GetComponent<Rigidbody>() == null)
            {
                player.GetComponent<PhysicsHandle>().ResetCarriedObject();
            }
        }
    }
}
