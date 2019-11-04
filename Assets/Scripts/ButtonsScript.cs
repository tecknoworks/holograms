using System.Collections;
using UnityEngine;
using TMPro;

public class ButtonsScript : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject gravityButton;
    public GameObject resetButton;

    private Vector3 offset;
    private bool gravity = false, kinematic = true;
    private float xRotation, yRotation, zRotation;
    // Start is called before the first frame update
    void Start()
    {
        xRotation = gameObject.transform.eulerAngles.x;
        yRotation = gameObject.transform.eulerAngles.y;
        zRotation = gameObject.transform.eulerAngles.z;

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnAdjust()
    {
        gravity = gameObject.GetComponent<Rigidbody>().useGravity;
        kinematic = gameObject.GetComponent<Rigidbody>().isKinematic;
        Destroy(gameObject.GetComponent<Rigidbody>());
        gravityButton.SetActive(false);
        resetButton.SetActive(false);
    }

    public void OnRemove()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void OnHide()
    {
        resetButton.SetActive(false);
        gravityButton.SetActive(false);
    }

    public void OnShow()
    {
        gravityButton.SetActive(true);
        resetButton.SetActive(true);
    }

    public void OnDone()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = gravity;
        rb.isKinematic = kinematic;
        gravityButton.SetActive(true);
        resetButton.SetActive(true);
    }

    public void OnReset()
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
        gameObject.transform.eulerAngles = new Vector3(xRotation, yRotation, zRotation);
        gameObject.transform.localScale = Vector3.one;

    }

    public void OnGravity()
    {
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
    }
}
