using UnityEngine;
using System.Collections;

public class UnitSkillAttackStraightLine : UnitSkillAttackBase
{
	private int _FlyFrame = -1;

	protected override bool _calculateFly ()
	{
		if(_FlyFrame == -1)
		{
			//Debug.Log("Logic Forward at frame: " + Launch.battleplayer._battle.Frame + " time : " + System.Environment.TickCount );

			Vector2 distance = Caster.Position - Target.Position;

			_FlyFrame = (int)(distance.magnitude / Speed / Battleplayer.TIME_PER_FRAME);
		}
		else if( _FlyFrame > 0)
		{
			--_FlyFrame;
		}
		else if(_FlyFrame == 0)
		{
			_onTriggerHit();

			return true;
		}

		return false;
	}
}
