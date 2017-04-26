using UnityEngine;
using System.Collections;

public class GridRenderer
{
	private GameObject gridObj;
	private MeshFilter meshFilter;
	private Mesh mesh;
	private MeshRenderer meshRenderer;

	public void Init(int width, int height, int step, float y, string matPath, string name)
	{
		if(width % step != 0 || height % step != 0)
		{
			Debug.LogError("格子的宽度和高度必须被整除width：" + width.ToString() + " , height :" + height.ToString() + " step: " + step.ToString());
		
			return;
		}

		gridObj = new GameObject(name);

		meshFilter = gridObj.AddComponent<MeshFilter>();
		meshRenderer = gridObj.AddComponent<MeshRenderer>();

		mesh = new Mesh();
		mesh.name = name + "Mesh";

		meshFilter.mesh = mesh;

		meshRenderer.sharedMaterial = Resources.Load<Material>(matPath);

		int gridWidth = width / step;
		int gridHeight = height / step;

		Vector3[] vertices = new Vector3[gridWidth * gridHeight * 4];
		int[] indices = new int[gridWidth * gridHeight * 6];
		Vector2[] uvs = new Vector2[gridWidth * gridHeight * 4];

		for(int i = 0; i < gridWidth; ++i)
		{
			for(int j = 0; j < gridHeight; ++j)
			{
				int gridIndex = i + j * gridWidth;
				int vertIndex = gridIndex * 4;

				// vertices
				vertices[vertIndex] = new Vector3(i * step, y, j * step);
				vertices[vertIndex + 1] = new Vector3((i+1) * step, y, j * step);
				vertices[vertIndex + 2] = new Vector3((i+1) * step, y, (j+1) * step);
				vertices[vertIndex + 3] = new Vector3(i * step, y, (j+1) * step);

				int indicesIndex = gridIndex * 6;

				// indices
				indices[indicesIndex] = vertIndex;
				indices[indicesIndex + 1] = vertIndex + 2;
				indices[indicesIndex + 2] = vertIndex + 1;
				indices[indicesIndex + 3] = vertIndex;
				indices[indicesIndex + 4] = vertIndex + 3;
				indices[indicesIndex + 5] = vertIndex + 2;

				// uvs
				uvs[vertIndex] = new Vector2(0, 0);
				uvs[vertIndex + 1] = new Vector2(1, 0);
				uvs[vertIndex + 2] = new Vector2(1, 1);
				uvs[vertIndex + 3] = new Vector2(0, 1);
			}
		}

		mesh.vertices = vertices;
		mesh.triangles = indices;
		mesh.uv = uvs;
	}


}
