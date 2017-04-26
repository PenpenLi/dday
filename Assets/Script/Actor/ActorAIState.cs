using UnityEngine;
using System.Collections;

public class ActorAIState 
{
	protected Actor actor;

	public virtual void Init(Actor parent)
	{
		actor = parent;
	}

	public virtual void Destroy()
	{
		
	}

	public virtual void Tick(float dt)
	{


	}
}
