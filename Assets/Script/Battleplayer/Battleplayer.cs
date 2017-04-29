using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battleplayer  
{
	Battle _battle = null;
	float _timeStamp = 0;
	int _lastFrame = 0;

	public static int FRAME_RATE = 30;
	public static float TIME_PER_FRAME = 1.0f / FRAME_RATE;

	// 数据与对应表现数据的映射关系
	Dictionary<Unit, Actor> unitActorMap = new Dictionary<Unit, Actor>();

	public void Init()
	{
		_battle = new Battle();
		_battle.Init();
		_timeStamp = 0;
		_lastFrame = 0;

		// map unit and actor
		List<Unit>.Enumerator enumerator = _battle.attakerList.GetEnumerator();
		while(enumerator.MoveNext())
		{
			Actor actor = ActorMananger.Instance().CreateActor(enumerator.Current.Position);
			unitActorMap.Add(enumerator.Current, actor);
		}

		enumerator = _battle.defenderList.GetEnumerator();
		while(enumerator.MoveNext())
		{
			Actor actor = ActorMananger.Instance().CreateActor(enumerator.Current.Position);
			unitActorMap.Add(enumerator.Current, actor);
		}

	}

	public void Destroy()
	{
		if(_battle != null)
		{
			_battle.Destroy();
			_battle = null;
		}
	}

	public void Tick(float dt)
	{
		if(_battle != null && _battle.IsStart)
		{
			_timeStamp += dt;

			int nowFrame = (int)(_timeStamp / TIME_PER_FRAME);

			int elapsedFrame = nowFrame - _lastFrame;

			while(elapsedFrame > 0)
			{
				_battle.Tick();

				--elapsedFrame;
			}

			_lastFrame = nowFrame;
		}
	}

	// 与表现层适配接口
	public void Move2Position(Unit unit, Vector2 startpos, Vector2 endpos)
	{
		//Debug.Log("start " + startpos.ToString() + " end " + endpos.ToString());

		Actor actor = unitActorMap[unit];

		actor.Move2Position(new Vector3(startpos.x, ActorMananger.ACTOR_Y, startpos.y), new Vector3(endpos.x, ActorMananger.ACTOR_Y, endpos.y), unit.MoveSpeed * FRAME_RATE);
	}

	public void Move2Target(Unit unit, Unit target)
	{
		Actor actor1 = unitActorMap[unit];
		Actor actor2 = unitActorMap[target];

		actor1.Move2Target(actor2, unit.MoveSpeed * FRAME_RATE);
	}

	public void Attack(Unit attacker, Unit target)
	{
		Actor actor1 = unitActorMap[attacker];
		Actor actor2 = unitActorMap[target];

		actor1.Attack(attacker.AttackSpeed * FRAME_RATE);
	}
}
