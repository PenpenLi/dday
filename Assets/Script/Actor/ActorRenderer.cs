using UnityEngine;
using System.Collections;

public class ActorRenderer 
{
	protected string actorName;
	protected Actor actor;

	virtual public void Init(string name, Actor parent)
	{
		actorName = name;
		actor = parent;
	}

	virtual public void Destroy()
	{
		
	}

	virtual public void Tick(float dt)
	{
		
	}

	virtual public void SetPosition(Vector3 position)
	{
		
	}

	virtual public void PlaySkill(string skillName, bool playOnce)
	{
		
	}

	virtual public void SetRotation(float rotation)
	{
		
	}
}
