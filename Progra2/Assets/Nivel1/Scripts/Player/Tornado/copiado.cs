////Float used to set the speed of the suction of the tornado
//using System.Collections.Generic;
//using UnityEngine;

//public class copiado : MonoBehaviour
//{

//    public float pullInSpeed = 0.1f;

//    //Float used to set the speed of the rotation of the tornado
//    public float rotateSpeed = 1.25f;

//    //Radius the suction of the tornado reaches
//    public float radius = 20;

//    //Holds the objects within the radius
//    public List<GameObject> objectsToPullIn;

//    //Sets whether the objects is being pulled or not
//    public Dictionary<GameObject, bool> objectsPulled;

//    void Start()
//    {
//        //Instantiate Dictionary and List for use
//        objectsToPullIn = new List<GameObject>();
//        objectsPulled = new Dictionary<GameObject, bool>();
//    }

//    void RemoveObjectsFarAway()
//    {
//        //For each of the gameobjects in objectsToPullIn
//        foreach (GameObject thing in objectsToPullIn)
//        {
//            //Check if the distance between the objects position and the tornados position is greater than the suctions radius
//            if (Vector3.Distance(thing.transform.position, transform.position) > radius)
//            {
//                //And if that is true then remove the object from being sucked in
//                objectsToPullIn.Remove(thing);
//            }
//        }
//    }

//    void GetObjectsToPullIn()
//    {
//        Collider[] objects = Physics.OverlapSphere(GetComponent<Collider>().bounds.center, radius);//GetComponent<Collider>().bounds.extents.magnitude);
//                                                                                                   //For each object
//        for (int i = 0; i < objects.Length; i++)
//        {
//            //If that object is not already contained in the objectsToPullIn list
//            //the object does not equal the tornado, and if it contains a 
//            //rigidbody component
//            if (!(objectsToPullIn.Contains(objects *.gameObject)) * && objects *.gameObject != gameObject * && objects *.GetComponent() != null) *
//            {
//                //Then add it to the objects to pull in list
//                objectsToPullIn.Add(objects *.gameObject); *
//                //And make sure to set that the object has not been pulled all the way in yet
//                objectsPulled.Add(objects *.gameObject, false); *
//            }
//        }
//    }

//    void PullObjectsIn()
//    {
//        //For each gameobject in objectsToPullIn
//        foreach (GameObject thing in objectsToPullIn)
//        {
//            //If the object has been pulled in
//            if (objectsPulled[thing] != true)
//            {
//                //Then move it towards the tornado
//                thing.transform.position = Vector3.MoveTowards(thing.transform.position, transform.position, thing.GetComponent().mass * Time.deltaTime * pullInSpeed);
//            }
//        }
//    }

//    void RotateObjects()
//    {
//        //For each of the gameobjects that have been classified as being pulled in or not
//        foreach (GameObject thing in objectsPulled.Keys)
//        {
//            //If they are pulled in
//            if (objectsPulled[thing] == true)
//            {
//                //Then rotate it around the tornado
//                thing.transform.RotateAround(Vector3.zero, transform.up, thing.GetComponent().mass * rotateSpeed * Time.deltaTime);
//            }
//        }
//    }

//    void OnCollisionEnter(Collision other)
//    {
//        //If the object is contained in the object to pull in list
//        if (objectsToPullIn.Contains(other.gameObject))
//        {
//            //Then set the object as being pulled in to the tornado
//            objectsPulled[other.gameObject] = true;
//            //Rotate that shit
//            RotateObjects();
//        }
//    }

//    void OnCollisionStay(Collision other)
//    {
//        //If the object is contained in the object to pull in list
//        if (objectsToPullIn.Contains(other.gameObject))
//        {
//            //Then set the object as being pulled in to the tornado
//            objectsPulled[other.gameObject] = true;
//            //Rotate that shit
//            RotateObjects();
//        }
//    }

//    void OnCollisionExit(Collision other)
//    {
//        //If the object is contained in the object to pull in list
//        if (objectsToPullIn.Contains(other.gameObject))
//        {
//            //Then set the object as not being pulled in to the tornado
//            objectsPulled[other.gameObject] = false;
//        }
//    }

//    void Update()
//    {
//        //Each update:

//        //Run these methods
//        RemoveObjectsFarAway();
//        GetObjectsToPullIn();
//        PullObjectsIn();
//        RotateObjects();
//    }
//}