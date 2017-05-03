using UnityEngine;
using System.Collections;

public class ActorMonoBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnHitCallback()
	{
		Actor actor =  ActorMananger.Instance().GetActorMonoBehaviour(gameObject);

		actor.OnHitCallBack();
	}
}
