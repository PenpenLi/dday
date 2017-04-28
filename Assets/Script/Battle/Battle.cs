﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle 
{
	public List<Unit> attakerList = new List<Unit>();
	public List<Unit> defenderList = new List<Unit>();

	public static int MAX_BATTLE_FILED_X = 160;
	public static int MAX_BATTLE_FILED_Y = 90;

	int _frame = 0;
	public int Frame 
	{
		get
		{
			return _frame;
		}
		set
		{
			_frame = value;
		}
	}

	bool _isStart = false;
	public bool IsStart
	{
		get
		{
			return _isStart;
		}
		set
		{
			_isStart = value;
		}

	}

	Vector2[] ATTACKER_INIT_POSITION = new Vector2[5]
	{
			new Vector2(0, 45),
			new Vector2(0, 47),
			new Vector2(0, 43),

		new Vector2(-1, 46),
		new Vector2(-1, 44),
	};

	Vector2[] DEFENDER_INIT_POSITION = new Vector2[5]
	{
		new Vector2(40, 45),
		new Vector2(40, 47),
		new Vector2(40, 43),

			new Vector2(41, 46),
			new Vector2(41, 44),
	};

	public void Init()
	{
		InitTroopUnit();

		Frame = 0;
		IsStart = true;
	}

	public void Tick()
	{
		List<Unit>.Enumerator enumerator = attakerList.GetEnumerator();
		while(enumerator.MoveNext())
		{
			enumerator.Current.Tick();
		}

		enumerator = defenderList.GetEnumerator();
		while(enumerator.MoveNext())
		{
			enumerator.Current.Tick();
		}

		++Frame;
	}

	public void Destroy()
	{
		
	}

	// 
	private void InitTroopUnit()
	{
		// attacker 
		for(int troop = 0; troop < 5; ++troop)
		{
			int unitcount = 1;//Random.Rand(1, 10);

			for(int unitIndex = 0; unitIndex < unitcount; ++unitIndex)
			{
				Unit unit = new Unit(this);

				int offset = 0; //Random.Rand(-2, 2);
				unit.Position = ATTACKER_INIT_POSITION[troop] + new Vector2(offset, offset);

				unit.HP = 100;

				unit.AttackRange = 1;
				unit.IsAttacker = true;

				unit.Attack = Random.Rand(3, 10);
				unit.AttackSpeed = 30;
				unit.MoveSpeed = 1 / 30.0f;

				attakerList.Add(unit);
			}
		}

		// defender
		for(int troop = 0; troop < 5; ++troop)
		{
			int unitcount = 1;//Random.Rand(1, 10);

			for(int unitIndex = 0; unitIndex < unitcount; ++unitIndex)
			{
				Unit unit = new Unit(this);

				int offset = 0;//Random.Rand(-2, 2);

				unit.Position = DEFENDER_INIT_POSITION[troop] + new Vector2(offset, offset);

				unit.HP = 100;

				unit.AttackRange = 1;
				unit.IsAttacker = false;

				unit.Attack = Random.Rand(3, 10);
				unit.AttackSpeed = 30;
				unit.MoveSpeed = 1 / 30.0f;

				defenderList.Add(unit);
			}
		}

	}
}