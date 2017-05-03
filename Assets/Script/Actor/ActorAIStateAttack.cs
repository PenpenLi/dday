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
		OnHitCallback();

		base.Destroy ();
	}

	public override void OnHitCallback()
	{
		if(callbackData != null && callbackData.Caster != null && callbackData.Target != null)
		{
			if(callbackData.IsDead)
			{
				callbackData.Target.Dead();	
			}

			callbackData.Destroy();
			callbackData = null;
		}
	}

	public override void Tick (float dt)
	{
		
	}
}
