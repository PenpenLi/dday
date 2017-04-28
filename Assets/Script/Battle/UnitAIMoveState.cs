using UnityEngine;
using System.Collections;

public class UnitAIMoveState : UnitAIState 
{
	public Vector2 startPos;
	public Vector2 endPos;
	public int startFrame;

	private Vector2 _dir;

	public override void Init (Battle b, Unit u)
	{
		base.Init (b, u);

		_dir = (endPos - startPos).normalized;

		Launch.battleplayer.Move(unit, startPos, endPos);
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick ()
	{
		if(_checkCanAttack())
		{
			unit.Position = endPos;

			UnitAIAttackState state = new UnitAIAttackState();

			unit.State = state;
		}
		else
		{
			int frame = battle.Frame - startFrame;

			unit.Position = startPos + _dir * frame * unit.MoveSpeed;
		}
	}

	private bool _checkCanAttack()
	{
		float distance = (unit.Target.Position - unit.Position).sqrMagnitude;
		return distance <= unit.AttackRange * unit.AttackRange;
	}
}
