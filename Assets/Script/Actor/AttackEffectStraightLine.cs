using UnityEngine;
using System.Collections;

public class AttackEffectStraightLine : AttackEffectBase 
{
	public string HitEffectPrefabName { set; get; }

	public string FlyEffectPrefabName { set; get; }

	GameObject _instanceFly = null;

	public float Speed { set; get; }


	private ParticleSystem particle_system_ = null;

	private Vector3 start_position = Vector3.zero;
	private float current_fly_time = 0;
	private float total_fly_time = 0;

	public override void Init ()
	{
		//GameObject prefab = ActorMananger.Instance().GetPrefab("Effect/" + FlyEffectPrefabName);

		//_instanceFly = GameObject.Instantiate(prefab);

		_instanceFly = ActorMananger.Instance().GetGameObjectInstance("Effect/" + FlyEffectPrefabName);

		_instanceFly.transform.position = Caster.Position;
		start_position = Caster.Position;

		Vector3 distance = Target.Position - Caster.Position;

		total_fly_time = distance.magnitude / Speed;
		current_fly_time = 0;

		particle_system_ = _instanceFly.GetComponent<ParticleSystem>();

		float cosTheta = Vector3.Dot(Vector3.up, distance.normalized);
		float theta = Mathf.Acos(cosTheta);

		particle_system_.startRotation3D = new Vector3(0, Caster.Rotation * Mathf.Deg2Rad, theta);

	}

	public override void Destroy ()
	{
		if(_instanceFly != null)
		{
			GameObject.Destroy(_instanceFly);
			_instanceFly = null;
		}
	}

	public override bool Tick (float dt)
	{
		if(current_fly_time < total_fly_time)
		{
			_instanceFly.transform.position = start_position + (Target.Position - start_position) * current_fly_time / total_fly_time;

			//Debug.Log("current postion in tick " + _instanceFly.transform.position);

			current_fly_time += dt;

			return false;
		}
		else
		{
			//Debug.Log("Renderer Attack Effect  at frame: " + Launch.battleplayer._battle.Frame + " time : " + System.Environment.TickCount );

			_instanceFly.transform.position = Target.Position;

			//GameObject.Destroy(_instanceFly);

			ActorMananger.Instance().RecycleGameObjectInstance("Effect/" + FlyEffectPrefabName, _instanceFly);

			_instanceFly = null;

			// 只有击中
			AttackEffectHit hitEffect = new AttackEffectHit();

			hitEffect.EffectPrefabName = HitEffectPrefabName;
			hitEffect.Caster = Caster;
			hitEffect.Target = Target;
			hitEffect.Init();
			Target.attackEffectList.Add(hitEffect);

			return true;
		}
	}
}
