using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAIAttackState : UnitAIState 
{
	public Vector2 position;

	private int attackFrameCounter = 0;

	//攻击前摇的帧数
	private int forwardFrame = 18;

	List<UnitAttackEffect> _effectList = new List<UnitAttackEffect>();

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
		for(int i = _effectList.Count-1;i >= 0 ; i--)
		{
			UnitAttackEffect attackEffect = _effectList[i];

			if(attackEffect.Tick())
			{
				// delete
				_effectList.RemoveAt(i);
			}
		}

		// 少一个距离的判断，如果打的过程中，目标走了
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
		UnitAttackEffect attackEffect = new UnitAttackEffect();
		attackEffect.ForwardFrame = forwardFrame;
		attackEffect.Caster = unit;
		attackEffect.Target = unit.Target;
		attackEffect.IsFly = true;
		attackEffect.Speed = 50;

		_effectList.Add(attackEffect);

		// 表现层接口
		Launch.battleplayer.Attack(battle.Frame, unit, unit.Target);
	}

	private bool _checkTargetAlive()
	{
		return unit.Target != null && unit.Target.IsAlive;	
	}
}
