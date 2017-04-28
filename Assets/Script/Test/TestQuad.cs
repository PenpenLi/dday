using UnityEngine;
using System.Collections;

public class TestQuad : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
		Quad quad = new Quad();
		quad.Init(Vector3.one);

		quad = new Quad();
		quad.Init(new Vector3(1.0f, 1.01f, 1.0f));
	}


	// Update is called once per frame
	void Update () 
	{

	}
}
