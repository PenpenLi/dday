using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorMananger  
{
	public static float ACTOR_Y = 0.5f;

	static ActorMananger instance = null;

	List<Actor> actorList = new List<Actor>();

	Dictionary<string, GameObject> prefabList = new Dictionary<string, GameObject>();

	Dictionary<GameObject, Actor> monoBehaviourMap = new Dictionary<GameObject, Actor>();

	Dictionary<string, Queue<GameObject>> gameobject_pool = new Dictionary<string, Queue<GameObject>>();

	public GameObject GetPrefab(string name)
	{
		if(!prefabList.ContainsKey(name))
		{
			GameObject prefab = Resources.Load<GameObject>(name);
			prefabList.Add(name, prefab);
		}

		return prefabList[name];
	}

	public GameObject GetGameObjectInstance(string name)
	{
		if(gameobject_pool.ContainsKey(name))
		{
			if(gameobject_pool[name].Count > 0)
			{
				GameObject instance = gameobject_pool[name].Dequeue();
				//instance.SetActive(true);
				return instance;
			}
			else
			{
				GameObject prefab = GetPrefab(name);

				return GameObject.Instantiate(prefab);
			}
		}
		else
		{
			gameobject_pool.Add(name, new Queue<GameObject>());

			GameObject prefab = GetPrefab(name);

			return GameObject.Instantiate(prefab);
		}
	}

	public void RecycleGameObjectInstance(string name, GameObject gameobject)
	{
		gameobject.transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

		gameobject_pool[name].Enqueue(gameobject);
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

	public Actor CreateActor(Unit unit)
	{
		Actor actor = new Actor();
		actor.AttackSkillAttackId = unit.AttackSkillAttackID;
		actor.Init("Actor/Sprite/Prefab/swordman_r_h", Actor.ActorType.Sprite, unit.ID);
		actorList.Add(actor);

		actor.Position = new Vector3(unit.Position.x, ActorMananger.ACTOR_Y, unit.Position.y);

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
