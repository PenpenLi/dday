using UnityEngine;
using System.Collections;

public class BattleCommandAttack : BattleCommandBase 
{
	// 攻击效果目前指令先按照非延时设计
	public int Target { set; get; }

	public int Damage { set; get; }

	public bool IsDead { set; get; }

	public Vector2 Position { set; get; }

	public override void Handle ()
	{
		Actor attacker = battleplayer.unitActorMap[Caster];
		Actor target = battleplayer.unitActorMap[Target];

		attacker.Attack(new Vector3(Position.x, ActorMananger.ACTOR_Y, Position.y), target, Damage, IsDead);
	}
}
