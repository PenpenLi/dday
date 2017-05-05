using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor 
{
	public enum ActorType
	{
		Sprite = 1,
		Skin = 2,
	}

	ActorRenderer renderer;
	ActorAIState state;

	public List<AttackEffectBase> attackEffectList = new List<AttackEffectBase>();

	public int ID { set; get; }

	public void Init(string resName, ActorType type, int id)
	{
		ID = id;

		if(type == ActorType.Sprite)
		{
			renderer =  new ActorRendererSprite();	
		}
		else if(type == ActorType.Skin)
		{
			renderer = new ActorRendererSkin();
		}

		renderer.Init(resName, this);
		renderer.AfterInit();
	}

	public void Destroy()
	{
		if(renderer != null)
		{
			renderer.BeforeDestroy();

			renderer.Destroy();
			renderer = null;
		}
	}

	public void AddChild(GameObject obj)
	{
		if(obj != null && renderer != null && renderer.instance != null)
		{
			obj.transform.SetParent(renderer.instance.transform, false);
		}	
	}

	public GameObject GetGameObject()
	{
		if(renderer != null)
		{
			return renderer.instance;
		}	
		else
		{
			return null;
		}
	}

	public void Tick(float dt)
	{
		if(state != null)
		{
			state.Tick(dt);	
		}

		if(renderer != null)
		{
			renderer.Tick(dt);
		}

		// 攻击效果的tick，效果结束要删除
		for(int i = attackEffectList.Count-1;i >= 0 ; i--)
		{
			AttackEffectBase attackEffect = attackEffectList[i];

			if(attackEffect.Tick(dt))
			{
				attackEffect.Destroy();
				// delete
				attackEffectList.RemoveAt(i);
			}
		}
	}

	public void OnHitCallBack(string attName)
	{
		if(state != null)
		{
			state.OnHitCallback(attName);
		}
	}

	public void SetState(ActorAIState newState)
	{
		if(state != null)
		{
			state.Destroy();

			state = null;
		}

		state = newState;
		state.Init(this);
	}

	public void PlaySkill(string skillName, bool playOnce = false)
	{
		if(renderer != null)
		{
			renderer.PlaySkill(skillName, playOnce);
		}
	}

	Vector3 position;
	public Vector3 Position
	{ 
		get
		{
			return position;
		} 
		set
		{
			if(value != position)
			{
				position = value;

				if(renderer != null)
				{
					renderer.SetPosition(position);
				}
			}
		}

	}

	// 绕y轴顺时针的转角
	float rotation;
	public float Rotation
	{
		get
		{
			return rotation;
		}

		set
		{
			if(value != rotation)
			{
				rotation = value;

				if(renderer != null)
				{
					renderer.SetRotation(rotation);
				}
			}
		}
	}

	public void RandomMove()
	{
		ActorAIStateMove2Position state = new ActorAIStateMove2Position();
		state.startPos = Position;

		int xendpos = Random.Rand(1, 20);
		int zendpos = Random.Rand(15, 30);

		state.endPos = new Vector3(xendpos, ActorMananger.ACTOR_Y, zendpos);

		state.moveSpeed = 2 * (float)Random.Rand();

		SetState(state);
	}

	public void Move2Position(Vector3 start, Vector3 end, float speed)
	{
		ActorAIStateMove2Position state = new ActorAIStateMove2Position();
		state.startPos = start;
		state.endPos = end;
		state.moveSpeed = speed;

		SetState(state);
	}

	public void Move2Target(Actor target, float speed)
	{
		ActorAIStateMove2Target state = new ActorAIStateMove2Target();
		state.moveSpeed = speed;
		state.target = target;

		SetState(state);
	}

	public void Attack(Vector3 position, Actor target, int damage, bool isDead)
	{
		ActorCallbackData attackCallback = new ActorCallbackData();
		attackCallback.Caster = this;
		attackCallback.Target = target;
		attackCallback.Damage = damage;
		attackCallback.IsDead = isDead;
		attackCallback.Init();

		ActorAIStateAttack state = new ActorAIStateAttack();
		state.callbackData = attackCallback;
		state.Position = position;

		SetState(state);
	}

	public void Dead()
	{
		ActorAIStateDead state = new ActorAIStateDead();

		SetState(state);
	}

	public void Idle()
	{
		ActorAIStateIdle state = new ActorAIStateIdle();

		SetState(state);
	}
}
