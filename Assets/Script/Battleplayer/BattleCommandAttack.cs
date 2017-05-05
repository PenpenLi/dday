using UnityEngine;
using System.Collections;

public class BattleCommandAttack : BattleCommandBase 
{
	public int Target { set; get; }

	public Vector2 Position { set; get; }

	public override void Handle ()
	{
		Actor attacker = battleplayer.unitActorMap[Caster];
		Actor target = battleplayer.unitActorMap[Target];

		attacker.Attack(new Vector3(Position.x, ActorMananger.ACTOR_Y, Position.y), target);
	}
}
