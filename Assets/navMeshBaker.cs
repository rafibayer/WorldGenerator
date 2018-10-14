using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshBaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LateStart());
	}
	
	IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        foreach(Transform surface in transform)
        {
            surface.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }
}
