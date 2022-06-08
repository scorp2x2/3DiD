using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class Test2 : MonoBehaviour
{

    public NavMeshSurface2d navMeshSurface2D;

    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface2D.BuildNavMesh();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
