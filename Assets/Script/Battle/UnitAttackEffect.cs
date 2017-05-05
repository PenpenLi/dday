using UnityEngine;
using System.Collections;

public class UnitAttackEffect 
{
	public int ForwardFrame { set; get; }
	public bool IsFly { set; get; }
	public Unit Caster { set; get; }
	public Unit Target { set; get; }

	public float Speed { set; get; }

	private int _FlyFrame = -1;

	public bool Tick()
	{
		if(ForwardFrame > 0)
		{
			--ForwardFrame;
			return false;
		}
		else
		{
			if(IsFly)
			{
				if(_FlyFrame == -1)
				{
					Vector2 distance = Caster.Position - Target.Position;

					_FlyFrame = (int)(distance.magnitude / Speed / Battleplayer.TIME_PER_FRAME);
				}
				else if( _FlyFrame > 0)
				{
					--_FlyFrame;
				}
				else if(_FlyFrame == 0)
				{
					Caster.CastAttack(Target);

					return true;
				}

				return false;
			}
			else
			{
				Caster.CastAttack(Target);
				return true;
			}
		}
	}

}
