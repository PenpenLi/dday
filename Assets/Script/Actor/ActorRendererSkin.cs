﻿using UnityEngine;
using System.Collections;

public class ActorRendererSkin : ActorRenderer 
{
	GameObject instance;
	Animator animator;

	public override void Init (string name, Actor parent)
	{
		base.Init (name, parent);

		GameObject prefab = ActorMananger.Instance().GetPrefab(actorName);

		instance = GameObject.Instantiate(prefab);
		animator = instance.GetComponentInChildren<Animator>();
	}

	public override void Destroy ()
	{
		if(instance != null)
		{
			GameObject.Destroy(instance);
			instance = null;
		}

		base.Destroy ();
	}

	public override void Tick (float dt)
	{
		base.Tick (dt);
	}

	public override void SetPosition (Vector3 position)
	{
		base.SetPosition (position);

		instance.transform.position = position;
	}

	public override void PlaySkill (string skillName)
	{
		animator.Play(skillName);
	}

	public override void SetRotation (float rotation)
	{
		Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * rotation, Vector3.up);

		instance.transform.rotation = q;
	}
}
