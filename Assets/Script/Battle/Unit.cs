using UnityEngine;
using System.Collections;

public class Unit 
{
	public Battle battle;

	public Unit(Battle b)
	{
		battle = b;

		State = null;
	}

	int _id;
	public int ID
	{
		get
		{
			return _id;
		}
		set
		{
			if(_id != value)
			{
				_id = value;
			}
		}
	}

	int _hp;
	public int HP 
	{
		get
		{
			return _hp;
		}
		set
		{
			if(_hp != value)
			{
				_hp = value;
			}
		}
	}

	int _attackRange;
	public int AttackRange
	{
		get
		{
			return _attackRange;
		}
		set
		{
			if(_attackRange != value)
			{
				_attackRange = value;
			}
		}
	}

	bool _isAttacker;
	public bool IsAttacker
	{
		get
		{
			return _isAttacker;
		}

		set
		{
			if(_isAttacker != value)
			{
				_isAttacker = value;
			}
		}
	}

	Vector2 _position;
	public Vector2 Position
	{
		get
		{
			return _position;
		}
		set
		{
			if(_position != value)
			{
				_position = value;
			}
		}
	}

	int _attack;
	public int Attack
	{
		get
		{
			return _attack;
		}
		set
		{
			if(_attack != value)
			{
				_attack = value;
			}
		}
	}

	// 几帧攻击一次
	int _attackSpeed;
	public int AttackSpeed
	{
		get
		{
			return _attackSpeed;
		}
		set
		{
			if(_attackSpeed != value)
			{
				_attackSpeed = value;
			}
		}
	}

	// 每帧移动多远
	float _moveSpeed;
	public float MoveSpeed
	{
		get
		{
			return _moveSpeed;
		}
		set
		{
			if(_moveSpeed != value)
			{
				_moveSpeed = value;
			}
		}
	}

	public bool IsAlive
	{
		get
		{
			return HP > 0;
		}
	}

	private Unit _target = null;
	public Unit Target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
		}
	}

	private UnitAIState _state = null;
	public UnitAIState State
	{
		get
		{
			return _state;
		}
		set
		{
			if(_state != null)
			{
				_state.Destroy();
				_state = null;
			}

			_state = value;

			if(_state != null)
			{
				_state.Init(battle, this);	
			}
		}
	}

	public void Tick()
	{
		if(_state != null)
		{
			_state.Tick();
		}
	}

	// 计算实际效果
	public void CastAttack(Unit target)
	{
		int damage = 0;
		if(target != null && target.IsAlive)
		{
			int old = target.HP;

			int tmp = (target.HP - Attack);
			target.HP = tmp > 0 ? tmp : 0;

			damage = old - target.HP;
		}

		bool isDead = !target.IsAlive;

		Launch.battleplayer.AttackEffect(battle.Frame, this, target, damage, isDead);

		// 如果目标死亡的AI
		if(isDead)
		{
			// target dead state
			UnitAIDeadState stateDead = new UnitAIDeadState();
			target.State = stateDead;

			UnitAIIdleState state = new UnitAIIdleState();
			State = state;
		}
	}
}
