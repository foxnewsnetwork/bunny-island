using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    public GameObject prefab;

    public void Build(Vector3Int position) {
        Instantiate(prefab, position, Quaternion.identity); 
    }
}
