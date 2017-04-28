using UnityEngine;
using System.Collections;

public class UnitAIState 
{
	protected Battle battle;
	protected Unit unit;

	virtual public void Init(Battle b, Unit u)
	{
		battle = b;
		unit = u;
	}

	virtual public void Destroy()
	{
		
	}

	virtual public void Tick()
	{
		
	}
}
