using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAIDeadState : UnitAIState 
{
	public override void Init (Battle b, Unit u)
	{
		base.Init (b, u);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick ()
	{

	}
}
