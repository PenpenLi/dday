using UnityEngine;
using System.Collections;

public class ActorAIStateMove : ActorAIState 
{
	public Vector3 startPos { set; get; }
	public Vector3 endPos { set; get; }
	public float moveSpeed { set; get; }

	private float timeStamp = 0;
	private float totalTime = 0;
	private Vector3 dir = Vector3.zero;

	public override void Init (Actor parent)
	{
		base.Init (parent);

		timeStamp = 0;

		dir = (endPos - startPos).normalized;
		totalTime = (endPos - startPos).magnitude / moveSpeed;

		Vector3 tmp = endPos - startPos;
		tmp.y = 0;
		tmp.Normalize();
		float cosTheta = Vector3.Dot(tmp, Vector3.forward);
		float theta = Mathf.Acos(cosTheta);

		if(tmp.x <= 0)
		{
			theta = -theta;	
		}

		actor.Rotation = theta;

		actor.PlaySkill("running");
	}

	public override void Destroy ()
	{
		base.Destroy ();
	}

	public override void Tick (float dt)
	{
		if(timeStamp >= totalTime)
		{
			actor.Position = endPos;
			return;
		}
		else
		{
			timeStamp += dt;

			actor.Position = startPos + dir * moveSpeed * timeStamp;
		}
	}
}
