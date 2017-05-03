using UnityEngine;
using System.Collections;

public class BattleCommandMove2Position : BattleCommandBase  
{
	public Vector2 StartPosition { set; get; }
	public Vector2 EndPosition { set; get; }

	public float MoveSpeed { set; get; }

	public override void Handle ()
	{
		Actor actor = battleplayer.unitActorMap[Caster];

		actor.Move2Position(new Vector3(StartPosition.x, ActorMananger.ACTOR_Y, StartPosition.y), new Vector3(EndPosition.x, ActorMananger.ACTOR_Y, EndPosition.y), MoveSpeed);
	}
}
