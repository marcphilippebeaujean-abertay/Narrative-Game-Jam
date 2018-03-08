﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePlay : MonoBehaviour {

    public int DialogueID = 69;
    Text interactionText;
    bool playerColliding;

	// Use this for initialization
	void Start () {
        interactionText = GameObject.FindGameObjectWithTag("Inspect Text").GetComponent<Text>();
        playerColliding = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.E))
        {
            if(playerColliding)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Dialogue>().PlayDialog(DialogueID);
                interactionText.text = "";
            }
        }	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //other.gameObject.GetComponent<Dialogue>().PlayDialog(DialogueID);
            interactionText.text = "Press 'E' to Interact";
            playerColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerColliding = false;
            interactionText.text = "";
        }
    }
}
