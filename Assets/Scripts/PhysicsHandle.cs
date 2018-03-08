using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHandle : MonoBehaviour {

    public float RayDistance = 3.0f;
    public float Reach = 0.5f;
    public float ObjRotSpeed = 1.0f;
    public LayerMask GrabingLayerMask;
    public Transform ColliderObject;
    private GameObject carriedObject;
    private Dialogue dialogueScript;
    bool carryingObject = false;
    bool inspectingObj = false;

	// Use this for initialization
	void Start ()
    {
        carryingObject = false;
        dialogueScript = GetComponentInParent<Dialogue>();
        dialogueScript.PlayDialog(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // If we aren't carrying an object, try to pick one up
            if (carryingObject == false)
            {

                Collider[] hitColliders = Physics.OverlapBox(ColliderObject.position, new Vector3(1, 1, 1), Quaternion.identity, GrabingLayerMask);
                int i = 0;
                //Check when there is a new collider coming into contact with the box
                while (i < hitColliders.Length)
                {
                    //Output all of the collider names
                    Debug.Log("Hit : " + hitColliders[i].name + i);
                    // detect object hit by collider
                    GameObject hit = hitColliders[i].gameObject;
                    carriedObject = hit.transform.gameObject;
                    Debug.Log("hit physics object");
                    // make camera the parent of the hit object, so that it follows camera around
                    carriedObject.transform.SetParent(transform);
                    // disable rigid body components that will hinder our movement
                    carriedObject.GetComponent<Rigidbody>().useGravity = false;
                    Physics.IgnoreCollision(GetComponentInParent<Collider>(), carriedObject.GetComponent<Collider>());
                    // setup class in the physics object
                    carriedObject.GetComponentInChildren<PhysicsReset>().SetCarrying(true);
                    // reset box transform
                    carriedObject.transform.rotation = Quaternion.identity;
                    ResetCarriedObject();
                    carryingObject = true;
                    // only apply to the first object we hit
                    break;
                }
            }
            else
            {
                if (carryingObject == true)
                {
                    // Drop the carried object
                    carriedObject.GetComponent<Rigidbody>().useGravity = true;
                    carriedObject.GetComponent<Rigidbody>().isKinematic = false;
                    carriedObject.transform.parent = null;
                    carriedObject.GetComponentInChildren<PhysicsReset>().SetCarrying(false);
                    Physics.IgnoreCollision(GetComponentInParent<Collider>(), carriedObject.GetComponent<Collider>(), false);
                    carriedObject = null;
                    carryingObject = false;
                }
            }
        }
        if(carryingObject == true)
        {
            carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (!inspectingObj)
            {
                carriedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
            else
            {
                carriedObject.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * ObjRotSpeed);
                // carriedObject.transform.Rotate((transform.right * Time.deltaTime * ObjRotSpeed * Input.GetAxis("Mouse X")) + (transform.up * Time.deltaTime * ObjRotSpeed * Input.GetAxis("Mouse Y")));
            }
        }
    }

    public void SetInspecting(bool shouldInspect)
    {
        inspectingObj = shouldInspect;
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
