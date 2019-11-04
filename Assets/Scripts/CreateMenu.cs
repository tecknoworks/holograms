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
        
        int i = 0;
        var length = prefabs.Length;
        foreach (GameObject prefab in prefabs)
        {
            if(3 - (length % 3) == 2 && i == (length / 3 + 1) * 2 - 1)
            {
                GameObject newButtonSup = Instantiate(button, Vector3.zero, Quaternion.identity) as GameObject;
                newButtonSup.transform.parent = collection.transform;
                Destroy(newButtonSup.GetComponent<AddAnObject>());
            }

            GameObject newButton = Instantiate(button, Vector3.zero, Quaternion.identity) as GameObject;

            AddAnObject script = newButton.GetComponent<AddAnObject>();
            script.prefab = prefab;

            Renderer buttonMaterial = newButton.transform.Find("BackPlate/Quad").gameObject.GetComponent<Renderer>();
            buttonMaterial.material = Array.Find(materials, m => m.name == prefab.name);

            newButton.transform.parent = collection.transform;
            i++;
        }

        
        GridObjectCollection col = collection.GetComponent<GridObjectCollection>();

        if ( length % 3 != 0 )
        {
            GameObject newButtonS = Instantiate(button, Vector3.zero, Quaternion.identity) as GameObject;             
            newButtonS.transform.parent = collection.transform;
            Destroy(newButtonS.GetComponent<AddAnObject>());

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
