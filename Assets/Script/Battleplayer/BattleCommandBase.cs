using UnityEngine;
using System.Collections;

public class BattleCommandBase  
{
	public Battleplayer battleplayer { set; get; }

	public int Frame { set; get; }
	public int Caster { set; get; }

	// 实际的指令执行内容
	public virtual void Handle()
	{
		
	}

}
