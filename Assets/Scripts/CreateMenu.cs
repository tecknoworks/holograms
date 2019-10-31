 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Microsoft.MixedReality.Toolkit.Utilities;

public class CreateMenu : MonoBehaviour
{
    private GameObject[] prefabs;
    private Material[] materials;
    public GameObject button;
    public GameObject collection;

    // Start is called before the first frame update
    void Start()
    {
        prefabs = Resources.LoadAll<GameObject>("Prefabs");
        materials = Resources.LoadAll<Material>("Materials");


            foreach (GameObject prefab in prefabs)
        {
            GameObject newButton = Instantiate(button, Vector3.zero, Quaternion.identity) as GameObject;

            AddAnObject script = newButton.GetComponent<AddAnObject>();
            script.prefab = prefab;

            Renderer buttonMaterial = newButton.transform.Find("BackPlate/Quad").gameObject.GetComponent<Renderer>();
            buttonMaterial.material = Array.Find(materials, m => m.name == prefab.name);

            newButton.transform.parent = collection.transform;
        }

        var length = prefabs.Length;
        GridObjectCollection col = collection.GetComponent<GridObjectCollection>();

        //if (length != 1 && length != 0)
        //    col.Rows = (int)Math.Log(length, 2);
        //else
        //    col.Rows = 1;

        if ( length % 3 != 0 )
        {
            for(int i = 1; i <= 3 - length % 3; i++)
            {
                GameObject newButton = Instantiate(button, Vector3.zero, Quaternion.identity) as GameObject;             
                newButton.transform.parent = collection.transform;
                Destroy(newButton.GetComponent<AddAnObject>());
            }
            col.Rows = length / 3 + 1;
        }
        else
        {
            col.Rows = length / 3;
        }
        col.UpdateCollection();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
