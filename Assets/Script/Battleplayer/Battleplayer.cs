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
	public Dictionary<int, Actor> unitActorMap = new Dictionary<int, Actor>();

	Queue<BattleCommandBase> _commandQueue = new Queue<BattleCommandBase>();

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
			Actor actor = ActorMananger.Instance().CreateActor(enumerator.Current.Position, enumerator.Current.ID);
			unitActorMap.Add(enumerator.Current.ID, actor);
		}

		enumerator = _battle.defenderList.GetEnumerator();
		while(enumerator.MoveNext())
		{
			Actor actor = ActorMananger.Instance().CreateActor(enumerator.Current.Position, enumerator.Current.ID);
			unitActorMap.Add(enumerator.Current.ID, actor);
		}

		_battle.InitState();
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

			// 指令按时间顺序排好序了
			while(_commandQueue.Count > 0)
			{
				BattleCommandBase command = _commandQueue.Peek();

				if(command.Frame <= nowFrame)
				{
					command.Handle();

					_commandQueue.Dequeue();
				}
				else
				{
					// 这一帧需要处理的都处理完了
					break;
				}
			}



			_lastFrame = nowFrame;
		}
	}

	// 与表现层适配接口
	public void Move2Position(int frame, Unit unit, Vector2 startpos, Vector2 endpos)
	{
		BattleCommandMove2Position command = new BattleCommandMove2Position();

		command.battleplayer = this;
		command.Caster = unit.ID;
		command.StartPosition = startpos;
		command.EndPosition = endpos;
		command.MoveSpeed = unit.MoveSpeed * FRAME_RATE;
		command.Frame = frame;

		_commandQueue.Enqueue(command);
	}

	public void Move2Target(int frame, Unit unit, Unit target)
	{
		BattleCommandMove2Target command = new BattleCommandMove2Target();
		command.battleplayer = this;

		command.Caster = unit.ID;
		command.Target = target.ID;
		command.MoveSpeed = unit.MoveSpeed * FRAME_RATE;
		command.Frame = frame;

		_commandQueue.Enqueue(command);
	}

	public void Attack(int frame, Unit attacker, Unit target, int damage, bool isDead)
	{
		BattleCommandAttack command = new BattleCommandAttack();
		command.battleplayer = this;

		command.Caster = attacker.ID;
		command.Target = target.ID;
		command.Damage = damage;
		command.IsDead = isDead;
		command.Frame = frame;
		command.Position = attacker.Position;

		_commandQueue.Enqueue(command);
	}

	public void Idle(int frame, Unit unit)
	{

		BattleCommandIdle command = new BattleCommandIdle();
		command.battleplayer = this;
		command.Caster = unit.ID;
		command.Frame = frame;

		_commandQueue.Enqueue(command);
	}
}
