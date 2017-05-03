using UnityEngine;
using System.Collections;

public class ActorCallbackData 
{
	public int Damage 
	{
		get;

		set;
	}

	public Actor Caster
	{
		get;
		set;
	}

	public Actor Target
	{
		get;
		set;
	}

	public bool IsDead
	{
		get;
		set;
	}

	public void Init()
	{
		
	}

	public void Destroy()
	{
		
	}
}
