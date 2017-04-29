using UnityEngine;
using System.Collections;

public class Actor 
{
	public enum ActorType
	{
		Sprite = 1,
		Skin = 2,
	}

	ActorRenderer renderer;
	ActorAIState state;

	public void Init(string resName, ActorType type)
	{
		if(type == ActorType.Sprite)
		{
			renderer =  new ActorRendererSprite();	
		}
		else if(type == ActorType.Skin)
		{
			renderer = new ActorRendererSkin();
		}

		renderer.Init(resName, this);
	}

	public void Destroy()
	{
		if(renderer != null)
		{
			renderer.Destroy();
			renderer = null;
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

	public void PlaySkill(string skillName)
	{
		if(renderer != null)
		{
			renderer.PlaySkill(skillName);
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

	public void Attack(float attackInterval)
	{
		ActorAIStateAttack state = new ActorAIStateAttack();
		state.AttackInterval = attackInterval;

		SetState(state);
	}
}
