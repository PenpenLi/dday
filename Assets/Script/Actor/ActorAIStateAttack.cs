using UnityEngine;
using System.Collections;

public class ActorAIStateAttack : ActorAIState 
{
	private float timeStamp = 0;

	public override void Init (Actor parent)
	{
		base.Init (parent);

		timeStamp = 5.0f;
			
		actor.PlaySkill("attack");
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick (float dt)
	{
		if(timeStamp < 0)
		{
			actor.RandomMove();
		}
		else
		{
			timeStamp -= dt;
		}
	}
}
