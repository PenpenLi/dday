using UnityEngine;
using System.Collections;

public class ActorAIStateAttack : ActorAIState 
{
	public Vector3 Position { set; get;}

	public ActorCallbackData callbackData = null;

	public override void Init (Actor parent)
	{
		base.Init (parent);

		actor.Position = Position;
		actor.PlaySkill("attack", true);
	}

	public override void Destroy ()
	{
		// 有的时候状态被打断，不会触发回调，这里强制处理一下
		_onHitEffectImpl();

		if(callbackData != null)
		{
			callbackData.Destroy();
			callbackData = null;
		}

		base.Destroy ();
	}

	public override void OnHitCallback(string attName)
	{
		if(callbackData != null && callbackData.Caster != null && callbackData.Target != null)
		{
			_onAttackEffect(attName);

			_onHitEffectImpl();
		}
	}

	private void _onAttackEffect(string attName)
	{
		if(callbackData != null && callbackData.Caster != null && callbackData.Target != null)
		{
			string[] attNames = attName.Split(';');

			if(attNames.Length == 1)
			{
				// 只有击中
				AttackEffectHit hitEffect = new AttackEffectHit();

				hitEffect.EffectPrefabName = attNames[0];
				hitEffect.Caster = callbackData.Caster;
				hitEffect.Target = callbackData.Target;

				hitEffect.Init();

				callbackData.Target.attackEffectList.Add(hitEffect);
			}
			else if(attNames.Length == 2)
			{
				// 飞行击中
				AttackEffectStraightLine attackEffect = new AttackEffectStraightLine();
				attackEffect.HitEffectPrefabName = attNames[0];
				attackEffect.FlyEffectPrefabName = attNames[1];

				attackEffect.Caster = callbackData.Caster;
				attackEffect.Target = callbackData.Target;

				attackEffect.Init();
				callbackData.Target.attackEffectList.Add(attackEffect);
			}
		}
	}

	// 这部分是临时的，
	// 改为延迟结算以后，效果统一做成effect指令处理
	private void _onHitEffectImpl()
	{
		if(callbackData != null && callbackData.Caster != null && callbackData.Target != null)
		{
			if(callbackData.IsDead)
			{
				callbackData.Target.Dead();	
			}
		}
	}

	public override void Tick (float dt)
	{
		
	}
}
