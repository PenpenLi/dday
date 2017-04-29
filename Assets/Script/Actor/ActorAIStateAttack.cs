using UnityEngine;
using System.Collections;

public class ActorAIStateAttack : ActorAIState 
{
	public float AttackInterval {set; get;}

	private float _attackTimer = 0;

	public override void Init (Actor parent)
	{
		base.Init (parent);

		_attackTimer = AttackInterval;
		actor.PlaySkill("attack", true);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick (float dt)
	{
		if(_attackTimer < 0)
		{
			actor.PlaySkill("attack", true);

			_attackTimer = AttackInterval;
		}
		else
		{
			_attackTimer -= dt;
		}
	}
}
