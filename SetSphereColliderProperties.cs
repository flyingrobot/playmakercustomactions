using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory(ActionCategory.GameObject)]
[Tooltip("Set Sphere Collider Properties of the GameObject")]
public class SetSphereColliderProperties : FsmStateAction
{
	[RequiredField]
	[CheckForComponent(typeof(SphereCollider))]
    [Tooltip("GameObject with SphereCollider")]
	public FsmOwnerDefault gameObject;
	
	[Tooltip("Center of Collider")]
    public FsmVector3 colliderCenter;
	
	[Tooltip("Radius")]
    public FsmFloat colliderRadius;
	
	[Tooltip("Is Trigger")]
    public FsmBool colliderIsTrigger;
	
	public bool everyFrame;
	
	
	public override void Reset()
	{
		gameObject = null;
		colliderCenter = null;
		colliderRadius = 0f;
		colliderIsTrigger = false;
		everyFrame = false;
	}
	
	// Code that runs on entering the state.
	public override void OnEnter()
	{
		SetColliderProperties();
		
		if (!everyFrame)
				Finish();
		
	}

	public override void OnUpdate()
	{
		SetColliderProperties();
	}
	
	void SetColliderProperties()
	{
		GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
		
		if (go == null) 
			{
				return;
			}
		
		SphereCollider collider = go.GetComponent<SphereCollider>();
		
		if (colliderRadius.Value != 0f) collider.radius = colliderRadius.Value;
		
		collider.isTrigger = colliderIsTrigger.Value;
		collider.center = colliderCenter.Value;
	}
	
}
