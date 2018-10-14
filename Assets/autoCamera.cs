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
        worldGen = GameObject.Find("worldGen").GetComponent<WorldGenerator>();//refrence to the world generator
        //position of the camera (width /2, sqrt(width * height), height /2) * scale
        Vector3 pos = new Vector3(worldGen.width / 2,
                                 Mathf.Sqrt(worldGen.width * worldGen.height),
                                    worldGen.height / 2) * worldGen.scale;
        if(cam.orthographic)//change the camera size to encompase the whole world if the camera is orthographic
        {
            cam.orthographicSize = worldGen.scale * Mathf.Max(new int []{ worldGen.width, worldGen.height}) / 2;
        }
        transform.position = pos;
        yield break;
    }
	
}
