using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAIAttackState : UnitAIState 
{
	public Vector2 position;

	private int attackFrameCounter = 0;

	public int attID = 1;

	List<UnitSkillAttackBase> _skillAttackList = new List<UnitSkillAttackBase>();

	public override void Init (Battle b, Unit u)
	{
		base.Init (b, u);

		attackFrameCounter = 0;

		//Debug.Log("Logic Attack at frame: " + Launch.battleplayer._battle.Frame + " time : " + System.Environment.TickCount );
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick ()
	{
		for(int i = _skillAttackList.Count-1;i >= 0 ; i--)
		{
			UnitSkillAttackBase attackEffect = _skillAttackList[i];

			if(attackEffect.Tick())
			{
				// delete
				_skillAttackList.RemoveAt(i);
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
		UnitSkillAttackBase attackEffect = null;

		// 获得att配置信息
		SkillAttackFlyAttributeConfig.SkillAttackFlyAttribute data = SkillAttackFlyAttributeConfig.AttackEffectConfigList[attID];


		switch(data.FlyType)
		{
			case 0:
			{
				// no fly
				attackEffect = new UnitSkillAttackBase();

				attackEffect.ForwardFrame = data.ForwardFrame;
				attackEffect.Caster = unit;
				attackEffect.Target = unit.Target;
				attackEffect.IsFly = data.IsFly;
				attackEffect.Speed = data.Speed;
			}
			break;
			case 1:
			{
				// straight line
				attackEffect = new UnitSkillAttackStraightLine();

				attackEffect.ForwardFrame = data.ForwardFrame;
				attackEffect.Caster = unit;
				attackEffect.Target = unit.Target;
				attackEffect.IsFly = data.IsFly;
				attackEffect.Speed = data.Speed;
			}
			break;
			default:
			{
				Debug.LogError(" 攻击没有飞行类型: " + attID);
			}
			break;
		}

		if(attackEffect != null)
		{
			_skillAttackList.Add(attackEffect);	
			// 表现层接口
			Launch.battleplayer.Attack(battle.Frame, unit, unit.Target);
		}
	}

	private bool _checkTargetAlive()
	{
		return unit.Target != null && unit.Target.IsAlive;	
	}
}
