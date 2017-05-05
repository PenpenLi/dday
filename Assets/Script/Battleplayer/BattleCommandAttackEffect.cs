using UnityEngine;
using System.Collections;

public class BattleCommandAttackEffect : BattleCommandBase 
{
	// 攻击效果目前指令先按照非延时设计
	public int Target { set; get; }

	public int Damage { set; get; }

	public bool IsDead { set; get; }

	public override void Handle ()
	{
		//Actor attacker = battleplayer.unitActorMap[Caster];
		Actor target = battleplayer.unitActorMap[Target];

		if(IsDead)
		{
			target.Dead();
		}
	}
}
