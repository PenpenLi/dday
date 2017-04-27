using UnityEngine;
using System.Collections;

public class Quad 
{
	Mesh mesh;
	Material material;
	MeshFilter meshFilter;
	MeshRenderer meshRenderer;
	GameObject quadObj;

	public void Init(Vector3 worldPos)
	{
		quadObj = new GameObject("Quad");

		meshFilter = quadObj.AddComponent<MeshFilter>();
		meshRenderer = quadObj.AddComponent<MeshRenderer>();

		mesh = new Mesh();
		mesh.name = "QuadMesh";

		meshFilter.mesh = mesh;
		meshRenderer.sharedMaterial = Resources.Load<Material>("Billboard/mat_billboard");



		Vector3[] vertices = new Vector3[4]
		{
			worldPos,
			worldPos,
			worldPos,
			worldPos
		};

		int[] indices = new int[6]
		{
			0, 2, 1, 0, 3, 2
		};

		Vector2[] uvs = new Vector2[4]
		{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 1),
			new Vector2(1, 0),
		};

		Vector2[] uv2s = new Vector2[4]
		{
			new Vector2(-0.5f, -0.5f),
			new Vector2(0.5f, -0.5f),
			new Vector2(0.5f, 0.5f),
			new Vector2(-0.5f, 0.5f),
		};

		meshFilter.mesh.vertices = vertices;
		meshFilter.mesh.triangles = indices;
		meshFilter.mesh.uv = uvs;
		meshFilter.mesh.uv2 = uv2s;


	}

	public void Update()
	{
//		if(meshRenderer)
//		{
//			meshRenderer.sharedMaterial.SetVector("right", Camera.main.transform.right);
//			meshRenderer.sharedMaterial.SetVector("up", Camera.main.transform.up);
//		}
	}
}
