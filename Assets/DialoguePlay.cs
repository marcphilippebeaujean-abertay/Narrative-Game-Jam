using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlay : MonoBehaviour {

    public int DialogueID = 69;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<Dialogue>().PlayDialog(DialogueID);
        }
    }
}
