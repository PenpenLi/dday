using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitAIIdleState : UnitAIState 
{
	public override void Init (Battle b, Unit u)
	{
		base.Init (b, u);
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
		float maxDistance = 0;

		List<Unit>.Enumerator enumerator = unitList.GetEnumerator();
		while(enumerator.MoveNext())
		{
			float distance = (enumerator.Current.Position - unit.Position).sqrMagnitude;
			if(distance > maxDistance)
			{
				maxDistance = distance;
				unit.Target = enumerator.Current;
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
			UnitAIMoveState state = new UnitAIMoveState();

			state.startPos = unit.Position;
			state.endPos = unit.Target.Position;
			state.startFrame = battle.Frame;

			unit.State = state;
		}
	}
}
