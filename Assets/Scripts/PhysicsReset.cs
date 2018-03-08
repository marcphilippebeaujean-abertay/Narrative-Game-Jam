using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsReset : MonoBehaviour {

    GameObject player;
    Rigidbody rigidBody;
    bool beingCarried = false;
    public int DialogueID = 69;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        rigidBody = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if(beingCarried)
        {
            Debug.Log("collider being carried!");
            player.GetComponent<PhysicsHandle>().ResetCarriedObject();
            rigidBody.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(beingCarried)
        {
           rigidBody.isKinematic = false;
        }
    }

    public void SetCarrying(bool currentCarriedObject)
    {
        beingCarried = currentCarriedObject;
    }

    public int GetDialogueID()
    {
        return DialogueID;
    }
}
