using UnityEngine;
using System.Collections;

public class UnitAIMove2TargetState : UnitAIState 
{
	private int _lastFrame = 0;
	private Vector2 _dir;

	public override void Init (Battle b, Unit u)
	{
		base.Init (b, u);

		_lastFrame = b.Frame;

		Launch.battleplayer.Move2Target(unit, unit.Target);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick ()
	{
		// 向目标移动
		// 1. 目标死亡的处理
		// 2. 进入自己射程的处理

		if(_checkCanAttack())
		{
			UnitAIAttackState state = new UnitAIAttackState();
			state.position = unit.Position;

			unit.State = state;
		}
		else
		{
			int frame = battle.Frame - _lastFrame;

			_dir = (unit.Target.Position - unit.Position).normalized;

			unit.Position = unit.Position + _dir * frame * unit.MoveSpeed;

			_lastFrame = battle.Frame;
		}
	}

	private bool _checkCanAttack()
	{
		float distance = (unit.Target.Position - unit.Position).sqrMagnitude;
		return distance <= unit.AttackRange * unit.AttackRange;
	}
}
