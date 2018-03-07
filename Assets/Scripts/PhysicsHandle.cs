using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHandle : MonoBehaviour {

    public float RayDistance = 3.0f;
    public float Reach = 0.5f;
    private GameObject carriedObject;
    bool carryingObject = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // If we aren't carrying an object, try to pick one up
            if (carryingObject == false)
            {
                Debug.Log("attempting to grab object");
                // create ray
                Debug.DrawRay(transform.position, transform.forward, Color.red);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward * RayDistance, out hit))
                {
                    if (hit.transform.tag == "Physics Object")
                    {
                        // detect object hit by ray
                        carriedObject = hit.transform.gameObject;
                        Debug.Log("hit physics object");
                        // make camera the parent of the hit object, so that it follows camera around
                        carriedObject.transform.SetParent(transform);
                        // disable rigid body
                        carriedObject.GetComponent<Rigidbody>().useGravity = false;
                        // reset box transform
                        carriedObject.transform.rotation = Quaternion.identity;
                        ResetCarriedObject();
                        carryingObject = true;
                    }
                }
            }
            else
            {
                if (carryingObject == true)
                {
                    // Drop the carried object
                    carriedObject.GetComponent<Rigidbody>().useGravity = true;
                    carriedObject.transform.parent = null;
                    carriedObject = null;
                    carryingObject = false;
                }
            }
        }
        if(carryingObject == true)
        {
            carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            carriedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    public void ResetCarriedObject()
    {
        if (carriedObject != null)
        {
            // create vector that the object should be set to once hit by ray
            Vector3 fwd = transform.TransformDirection(Vector3.forward) * Reach;
            Vector3 reachPosition = (transform.position + fwd);
            carriedObject.transform.position = reachPosition;
        }
    }
}
