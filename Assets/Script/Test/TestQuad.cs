using UnityEngine;
using System.Collections;

public class TestQuad : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
		Quad quad = new Quad();
		quad.Init(Vector3.one);

		quad = new Quad();
		quad.Init(Vector3.zero);
	}


	// Update is called once per frame
	void Update () 
	{

	}
}
