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


		Launch.battleplayer.Attack(unit, unit.Target);


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
		if(_checkTargetAlive())
		{
			int tmp = (unit.Target.HP - unit.Attack);
			unit.Target.HP = tmp > 0 ? tmp : 0;
		}

		if(!_checkTargetAlive())
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
