using UnityEngine;
using System.Collections;

public class AttackEffectStraightLine : AttackEffectBase 
{
	public string HitEffectPrefabName { set; get; }

	public string FlyEffectPrefabName { set; get; }

	GameObject _instanceFly = null;

	public float Speed { set; get; }

	private float _flyTime = 0;
	private Vector3 _dir = Vector3.zero;

	public override void Init ()
	{
		GameObject prefab = ActorMananger.Instance().GetPrefab("Effect/" + FlyEffectPrefabName);

		_instanceFly = GameObject.Instantiate(prefab);

		Speed = 10;

//		Matrix4x4 worldToLocal = Matrix4x4.identity;

//		if(Target != null)
//		{
//			Target.AddChild(_instanceFly);
//		
//			GameObject targetObj = Target.GetGameObject();
//
//			worldToLocal = targetObj.transform.worldToLocalMatrix;
//		}

//		Vector3 localCasterPosition = worldToLocal.MultiplyPoint3x4(Caster.Position);

//		_instanceFly.transform.localPosition = localCasterPosition;
//
//		_dir = -localCasterPosition.normalized;
//
//		_flyTime = localCasterPosition.magnitude / Speed;


		_instanceFly.transform.position = Caster.Position;

		Vector3 distance = Caster.Position - Target.Position;
		_dir = -distance.normalized;
		_flyTime = distance.magnitude / Speed;
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
		if(_flyTime > 0)
		{
			_flyTime -= dt;

			_instanceFly.transform.position += _dir * dt * Speed;

			return false;
		}
		else
		{
			GameObject.Destroy(_instanceFly);
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
