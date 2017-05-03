using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAIIdleState : UnitAIState 
{
	public override void Init (Battle b, Unit u)
	{
		base.Init (b, u);

		u.Target = null;

		Launch.battleplayer.Idle(u);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	private void _findTarget()
	{
		List<Unit> unitList = null;
		if(unit.IsAttacker)
		{
			unitList = battle.defenderList;
		}
		else
		{
			unitList = battle.attakerList;
		}

		// 找距离最近的
		float minDistance = float.MaxValue;

		List<Unit>.Enumerator enumerator = unitList.GetEnumerator();
		while(enumerator.MoveNext())
		{
			if(enumerator.Current.IsAlive)
			{
				float distance = (enumerator.Current.Position - unit.Position).sqrMagnitude;
				if(distance < minDistance)
				{
					minDistance = distance;
					unit.Target = enumerator.Current;
				}	
			}
		}

	}

	public override void Tick ()
	{
		if(unit.Target == null)
		{
			_findTarget();
		}

		if(unit.Target != null)
		{
			// 找到目标, move state
			UnitAIMove2TargetState state = new UnitAIMove2TargetState();
			unit.State = state;
		}
	}
}
