using UnityEngine;
using System.Collections;

public class Launch : MonoBehaviour {

	GridRenderer gridRenderer;

	GridRenderer terrainRenderer;

	public static Battleplayer battleplayer;

	void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 100;

	}

	// Use this for initialization
	void Start () 
	{

		terrainRenderer = new GridRenderer();

		terrainRenderer.Init(Battle.MAX_BATTLE_FILED_X, Battle.MAX_BATTLE_FILED_Y, 10, 0, "grid/mat_terrain", "Terrain");

		gridRenderer = new GridRenderer();

		gridRenderer.Init(Battle.MAX_BATTLE_FILED_X, Battle.MAX_BATTLE_FILED_Y, 2, 0.1f, "grid/mat_grid", "Grid");

		SkillAttackFlyAttributeConfig.Initialize();
		UnitConfig.Initialize();

		battleplayer = new Battleplayer();
		battleplayer.Init();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(battleplayer != null)
		{
			battleplayer.Tick(Time.deltaTime);	
		}

		ActorMananger.Instance().Tick(Time.deltaTime);
	}
}
