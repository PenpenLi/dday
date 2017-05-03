using UnityEngine;
using System.Collections;

public class BattleCommandMove2Target : BattleCommandBase 
{
	public int Target { set; get; }

	public float MoveSpeed { set; get; } 

	public override void Handle ()
	{
		Actor actor1 = battleplayer.unitActorMap[Caster];
		Actor actor2 = battleplayer.unitActorMap[Target];

		actor1.Move2Target(actor2, MoveSpeed);
	}
}
