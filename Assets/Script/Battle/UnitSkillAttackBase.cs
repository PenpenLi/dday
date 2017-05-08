using UnityEngine;
using System.Collections;

// base是不带飞行的，其他的飞行效果集成以后直接修改
public class UnitSkillAttackBase 
{
	public int ForwardFrame { set; get; }
	public bool IsFly { set; get; }
	public float Speed { set; get; }

	public Unit Caster { set; get; }
	public Unit Target { set; get; }

	virtual public bool Tick()
	{
		if(ForwardFrame > 0)
		{
			--ForwardFrame;

			return false;
		}
		else
		{
			if(IsFly)
			{
				return _calculateFly();
			}
			else
			{
				_onTriggerHit();
				return true;	
			}
		}
	}

	virtual protected bool _calculateFly()
	{
		return true;
	}

	protected void _onTriggerHit()
	{
		Caster.CastAttack(Target);
	}
}
