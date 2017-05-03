using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorMananger  
{
	public static int ACTOR_Y = 1;

	static ActorMananger instance = null;

	List<Actor> actorList = new List<Actor>();

	Dictionary<string, GameObject> prefabList = new Dictionary<string, GameObject>();

	Dictionary<GameObject, Actor> monoBehaviourMap = new Dictionary<GameObject, Actor>();

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

	public Actor CreateActor(Vector2 position, int id)
	{
		Actor actor = new Actor();
		actor.Init("Actor/Sprite/Prefab/swordman_r_h", Actor.ActorType.Sprite, id);
		actorList.Add(actor);

		actor.Position = new Vector3(position.x, ActorMananger.ACTOR_Y, position.y);

		return actor;
	}

	// 由于动作才用统一的回调处理，所以需要一个mono和actor的映射关系
	public void RegisterMonoBehaviour(GameObject obj, Actor actor)
	{
		monoBehaviourMap[obj] = actor;
	}

	public void UnRgisterMonoBehaviour(GameObject obj, Actor actor)
	{
		monoBehaviourMap.Remove(obj);
	}

	public Actor GetActorMonoBehaviour(GameObject obj)
	{
		return monoBehaviourMap[obj];
	}

	// 测试同屏压力的接口
	public void GenerateTestActor()
	{
		for(int i = 0; i < 400; ++i)
		{
			Actor actor = new Actor();
			actor.Init("Actor/Sprite/Prefab/swordman_r_h", Actor.ActorType.Sprite, i);
			actorList.Add(actor);

			int xpos = Random.Rand(0, 20);
			int zpos = Random.Rand(10, 30);

			actor.Position = new Vector3(xpos, ActorMananger.ACTOR_Y, zpos);

			actor.RandomMove();
		}

		for(int i = 0; i < 10; ++i)
		{
			Actor actor = new Actor();
			actor.Init("Actor/Skin/Gun/Gun_z", Actor.ActorType.Skin, i);
			actorList.Add(actor);

			int xpos = Random.Rand(1, 20);
			int zpos = Random.Rand(15, 30);

			actor.Position = new Vector3(xpos, ActorMananger.ACTOR_Y, zpos);

			actor.RandomMove();
		}

	}
}
