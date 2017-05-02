﻿using UnityEngine;
using System.Collections;

public class ActorAIStateIdle : ActorAIState 
{
	public override void Init (Actor parent)
	{
		base.Init (parent);

		actor.PlaySkill("casual", true);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick (float dt)
	{
		
	}
}
