using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	GridRenderer gridRenderer;

	GridRenderer terrainRenderer;

	// Use this for initialization
	void Start () {

		terrainRenderer = new GridRenderer();

		terrainRenderer.Init(80, 80, 10, 0, "grid/mat_terrain", "Terrain");

		gridRenderer = new GridRenderer();

		gridRenderer.Init(20, 40, 2, 0.1f, "grid/mat_grid", "Grid");

		InitActor();


//		Sprite[] sprites = Resources.LoadAll<Sprite>("CommonMaterials/battle_anim/human_anim_packer");
//
//		foreach(var sprite in sprites)
//		{
//			Debug.Log(sprite.name);
//		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		ActorMananger.Instance().Tick(Time.deltaTime);
	}

	void InitActor()
	{
		ActorMananger.Instance().GenerateActor();
	}
}
