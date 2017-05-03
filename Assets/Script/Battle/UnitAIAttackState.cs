using UnityEngine;
using System.Collections;

public class UnitAIAttackState : UnitAIState 
{
	public Vector2 position;

	private int attackFrameCounter = 0;

	public override void Init (Battle b, Unit u)
	{
		base.Init (b, u);

		attackFrameCounter = unit.AttackSpeed;

		_doAttack();
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick ()
	{
		if(!_checkTargetAlive())
		{
			UnitAIIdleState state = new UnitAIIdleState();

			unit.State = state;

			return;
		}

		if(attackFrameCounter > 0)
		{
			--attackFrameCounter;
		}

		if(attackFrameCounter == 0)
		{
			_doAttack();

			attackFrameCounter = unit.AttackSpeed;
		}
	}

	private void _doAttack()
	{
		int damage = 0;
		if(_checkTargetAlive())
		{
			int old = unit.Target.HP;

			int tmp = (unit.Target.HP - unit.Attack);
			unit.Target.HP = tmp > 0 ? tmp : 0;

			damage = old - unit.Target.HP;
		}

		Unit target = unit.Target;

		bool isDead = !_checkTargetAlive();

		// 表现层接口
		Launch.battleplayer.Attack(battle.Frame, unit, target, damage, isDead);

		// 如果目标死亡的AI
		if(isDead)
		{
			// target dead state
			UnitAIDeadState stateDead = new UnitAIDeadState();
			unit.Target.State = stateDead;
			unit.Target = null;

			UnitAIIdleState state = new UnitAIIdleState();
			unit.State = state;

		}
	}

	private bool _checkTargetAlive()
	{
		return unit.Target != null && unit.Target.IsAlive;	
	}
}
