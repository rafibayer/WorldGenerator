using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoCamera : MonoBehaviour {

    private WorldGenerator worldGen;
    private Camera cam;
	void Start () {
        cam = gameObject.GetComponent<Camera>();
        StartCoroutine(delayedCamera());
    }

    IEnumerator delayedCamera()//waits until worldGen finished generation or read
    {
        yield return new WaitForSeconds(0.1f);
        worldGen = GameObject.Find("worldGen").GetComponent<WorldGenerator>();
        Vector3 pos = new Vector3(worldGen.width / 2,
                                 Mathf.Sqrt(worldGen.width * worldGen.height),
                                    worldGen.height / 2) * worldGen.scale;
        if(cam.orthographic)
        {
            cam.orthographicSize = worldGen.scale * Mathf.Max(new int []{ worldGen.width, worldGen.height}) / 2;
        }
        transform.position = pos;
        yield break;
    }
	
}
