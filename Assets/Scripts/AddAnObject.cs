using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnObject : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add()
    {
        var DistanceToCamera = 3f;
        GameObject objCamera = (GameObject)GameObject.FindWithTag("MainCamera");
        Vector3 SpawnPosition = objCamera.transform.forward * DistanceToCamera + objCamera.transform.position;
        SpawnPosition.y = -0.5f;
        Instantiate(prefab, SpawnPosition, Quaternion.identity);
    }

}
