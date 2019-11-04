using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class ButtonsScriptGroup : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject gravityButton;
    public GameObject resetButton;
    public GameObject selectButton;

    private Vector3 offset;
    private bool gravity = false, kinematic = true;
    private bool notSelected = true;
    private float x, y, z;
    private Dictionary<string, Vector3> childsRotation=new Dictionary<string, Vector3>();
    private Dictionary<string, Vector3> childsPosition=new Dictionary<string, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        x = gameObject.transform.eulerAngles.x;
        y = gameObject.transform.eulerAngles.y;
        z = gameObject.transform.eulerAngles.z;
        foreach (Transform child in gameObject.transform)
        {
            childsRotation.Add(child.name,child.transform.eulerAngles);
            childsPosition.Add(child.name,child.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnAdjust()
    {
        if ( notSelected )
        {
            gravity = gameObject.GetComponent<Rigidbody>().useGravity;
            kinematic = gameObject.GetComponent<Rigidbody>().isKinematic;
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
        gravityButton.SetActive(false);
        resetButton.SetActive(false);
        selectButton.SetActive(false);
    }

    public void OnRemove()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void OnHide()
    {
        resetButton.SetActive(false);
        gravityButton.SetActive(false);
        selectButton.SetActive(false);
    }

    public void OnShow()
    {

        gravityButton.SetActive(true);
        resetButton.SetActive(true);         
        selectButton.SetActive(true);
    }

    public void OnDone()
    {
        if ( notSelected )
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = gravity;
            rb.isKinematic = kinematic;           
        }
        gravityButton.SetActive(true);
        resetButton.SetActive(true);
        selectButton.SetActive(true);
    }

    public void OnReset()
    {
        if (notSelected)
        {
            var DistanceToCamera = 3f;
            GameObject objCamera = (GameObject)GameObject.FindWithTag("MainCamera");
            Vector3 SpawnPosition = objCamera.transform.forward * DistanceToCamera + objCamera.transform.position;
            gameObject.transform.position = new Vector3(SpawnPosition.x, -0.5f, SpawnPosition.z);
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            gravity = false;
            kinematic = true;
            rb.useGravity = gravity;
            rb.isKinematic = kinematic;
            gravityButton.GetComponentInChildren<TextMeshPro>().SetText("Gravity" + "\n" + "Turn on");
            gameObject.transform.eulerAngles = new Vector3(x, y, z);
            gameObject.transform.localScale = Vector3.one;
            foreach (Transform child in gameObject.transform)
            {
                child.transform.eulerAngles = childsRotation[child.name];
                child.transform.position = childsPosition[child.name];
            }
        }

    }

    private void changeChildCollider()
    {
        foreach (Transform child in gameObject.transform)
        {
            Collider childCollider = child.gameObject.GetComponent<Collider>();
            if (childCollider)
                childCollider.enabled = !childCollider.enabled;

            CollisionsScriptGroup script = child.gameObject.GetComponent<CollisionsScriptGroup>();
            if (script)
                script.enabled = !script.enabled;

        }
    }

    public void OnGravity()
    {
        if (notSelected)
        {
            changeChildCollider();
            Rigidbody rigid = gameObject.GetComponentInChildren<Rigidbody>();
            rigid.useGravity = !rigid.useGravity;
            rigid.isKinematic = !rigid.isKinematic;
            gravity = rigid.useGravity;
            kinematic = rigid.isKinematic;
            if (gravity == true)
            {
                gravityButton.GetComponentInChildren<TextMeshPro>().SetText("Gravity" + "\n" + "Turn off");
            }
            else
            {
                gravityButton.GetComponentInChildren<TextMeshPro>().SetText("Gravity" + "\n" + "Turn on");
            }
            changeChildCollider();
        }
    }

    public void changeChildGravity()
    {
        foreach (Transform child in gameObject.transform)
        {
            Collider childCollider = child.gameObject.GetComponent<Collider>();
            if (childCollider)
                childCollider.enabled = !childCollider.enabled;

            Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                if(notSelected)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
                else
                {
                    rb.useGravity = gravity;
                    rb.isKinematic = kinematic;
                }
            }

            //BoundingBox childBox = child.gameObject.GetComponent<BoundingBox>();
            //if(childBox)
            //    childBox.Active = !childBox.Active;

        }
    }

    public void OnSelect()
    {
        Collider collider = gameObject.GetComponent<Collider>();
        bool enabled = !collider.enabled;
        collider.enabled = enabled;

        if( gameObject.GetComponent<Rigidbody>() )
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            notSelected = false;
        }
        else
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = gravity;
            rb.isKinematic = kinematic;
            notSelected = true;
        }
        
        changeChildGravity();

        if (enabled == true)
        {
            selectButton.GetComponentInChildren<TextMeshPro>().SetText("Deselect" + "\n" + "All");
        }
        else
        {
            selectButton.GetComponentInChildren<TextMeshPro>().SetText("Select" + "\n" + "All");
        }
    }
}
