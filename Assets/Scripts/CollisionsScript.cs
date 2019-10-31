using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject[] appBar = GameObject.FindGameObjectsWithTag("AppBar");
        foreach (GameObject appButton in appBar)
        {
            Physics.IgnoreCollision(appButton.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
    }

}
