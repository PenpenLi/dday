using UnityEngine;
using System.Collections;

public class BattleCommandIdle : BattleCommandBase  
{
	public override void Handle ()
	{
		Actor actor = battleplayer.unitActorMap[Caster];
		actor.Idle();
	}
}
