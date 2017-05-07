using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackEffectConfig 
{
	public class AttackEffectData
	{
		public int ID { set; get; }

		public bool IsFly { set; get; }

		public float Speed { set; get; }

		// 前摇时间
		public int ForwardFrame { set; get; }

		public string HitEffectName { set; get; }

		public string FlyEffectName { set; get; }
	}

	public static Dictionary<int , AttackEffectData> AttackEffectConfigList = new Dictionary<int, AttackEffectData>();

	public static void Init()
	{
		AttackEffectData data = new AttackEffectData();
		data.ID = 1;
		data.IsFly = true;
		data.Speed = 50;
		data.ForwardFrame = 1;
		data.HitEffectName = "beiji_ren_ani";
		data.FlyEffectName = "arrow";

		AttackEffectConfigList.Add(data.ID, data);

	}
}
