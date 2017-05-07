using UnityEngine;
using System.Collections;

public class ActorAIStateAttack : ActorAIState 
{
	public Vector3 Position { set; get;}

	public ActorCallbackData callbackData = null;

	public int attID = 1;

	private float _callBackTime = 0;

	private bool _triggered = false;

	public override void Init (Actor parent)
	{
		//Debug.Log("Renderer Attack at frame: " + Launch.battleplayer._battle.Frame + " time : " + System.Environment.TickCount );

		base.Init (parent);

		var data = AttackEffectConfig.AttackEffectConfigList[attID];
		_callBackTime = data.ForwardFrame * Battleplayer.TIME_PER_FRAME;

		actor.Position = Position;
		actor.PlaySkill("attack", true);
	}

	public override void Destroy ()
	{
		// 有的时候状态被打断，不会触发回调，这里强制处理一下
		if(callbackData != null)
		{
			callbackData.Destroy();
			callbackData = null;
		}

		base.Destroy ();
	}

	public override void OnHitCallback(string attName)
	{

	}

	private void _onHitCallBack()
	{
		if(callbackData != null && callbackData.Caster != null && callbackData.Target != null)
		{
			//Debug.Log("Renderer Forward at frame: " + Launch.battleplayer._battle.Frame + " time : " + System.Environment.TickCount );

			var data = AttackEffectConfig.AttackEffectConfigList[attID];


			if(!data.IsFly)
			{
				// 只有击中
				AttackEffectHit hitEffect = new AttackEffectHit();

				hitEffect.EffectPrefabName = data.HitEffectName;
				hitEffect.Caster = callbackData.Caster;
				hitEffect.Target = callbackData.Target;

				hitEffect.Init();

				callbackData.Target.attackEffectList.Add(hitEffect);
			}
			else
			{
				// 飞行击中
				AttackEffectStraightLine attackEffect = new AttackEffectStraightLine();
				attackEffect.HitEffectPrefabName = data.HitEffectName;
				attackEffect.FlyEffectPrefabName = data.FlyEffectName;

				attackEffect.Caster = callbackData.Caster;
				attackEffect.Target = callbackData.Target;

				attackEffect.Speed = data.Speed;

				attackEffect.Init();
				callbackData.Target.attackEffectList.Add(attackEffect);
			}
		}
	}

	public override void Tick (float dt)
	{
		if(_triggered)
		{
			return;
		}

		if(_callBackTime > 0)
		{
			_callBackTime -= dt;
		}
		else
		{
			_onHitCallBack();

			_triggered = true;
		}
	}
}
