using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorMananger  
{
	public static int ACTOR_Y = 1;

	static ActorMananger instance = null;

	List<Actor> actorList = new List<Actor>();

	Dictionary<string, GameObject> prefabList = new Dictionary<string, GameObject>();

	public GameObject GetPrefab(string name)
	{
		if(!prefabList.ContainsKey(name))
		{
			GameObject prefab = Resources.Load<GameObject>(name);
			prefabList.Add(name, prefab);
		}

		return prefabList[name];
	}

	public static ActorMananger Instance()
	{
		if(instance == null)
		{
			instance = new ActorMananger();
		}

		return instance;
	}

	public void Tick(float dt)
	{
		List<Actor>.Enumerator enumarator = actorList.GetEnumerator();

		while(enumarator.MoveNext())
		{
			enumarator.Current.Tick(dt);
		}
	}

	public Actor CreateActor(Vector2 position)
	{
		Actor actor = new Actor();
		actor.Init("Actor/Sprite/Prefab/swordman_r_h", Actor.ActorType.Sprite);
		actorList.Add(actor);

		actor.Position = new Vector3(position.x, ActorMananger.ACTOR_Y, position.y);

		return actor;
	}

	// 测试同屏压力的接口
	public void GenerateTestActor()
	{
		for(int i = 0; i < 400; ++i)
		{
			Actor actor = new Actor();
			actor.Init("Actor/Sprite/Prefab/swordman_r_h", Actor.ActorType.Sprite);
			actorList.Add(actor);

			int xpos = Random.Rand(0, 20);
			int zpos = Random.Rand(10, 30);

			actor.Position = new Vector3(xpos, ActorMananger.ACTOR_Y, zpos);

			actor.RandomMove();
		}

		for(int i = 0; i < 10; ++i)
		{
			Actor actor = new Actor();
			actor.Init("Actor/Skin/Gun/Gun_z", Actor.ActorType.Skin);
			actorList.Add(actor);

			int xpos = Random.Rand(1, 20);
			int zpos = Random.Rand(15, 30);

			actor.Position = new Vector3(xpos, ActorMananger.ACTOR_Y, zpos);

			actor.RandomMove();
		}

	}
}
