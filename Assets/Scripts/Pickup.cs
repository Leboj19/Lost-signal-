using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Pickup : MonoBehaviour
{
    bool canPickup;
    [SerializeField] GameObject target;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canPickup==true)
        {
           this.transform.parent= target.transform;
           this.transform.localEulerAngles = new Vector3(0, 0, 0);//makes the object center of the target
           this.GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && this.transform.parent == target.transform)// droping object!!!
        {
            this.transform.parent = null;
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    

    
    void OnTriggerEnter(Collider other)
    {
        canPickup = true;
        System.Console.WriteLine("can pickup");
    }
    void OnTriggerExit(Collider other)
    {
        canPickup = false;
    }
}
