using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class FollowScript : MonoBehaviour
{
    public GameObject collection;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeFollow()
    {
        RadialView rv = collection.GetComponent<RadialView>();
        rv.enabled = !rv.enabled;

        if(rv.enabled)
        {
            button.GetComponentInChildren<TextMeshPro>().SetText("Unfollow me");
        }
        else
        {
            button.GetComponentInChildren<TextMeshPro>().SetText("Follow me");
        }

        Billboard bb = collection.GetComponent<Billboard>();
        bb.enabled = !bb.enabled;
    }
}
