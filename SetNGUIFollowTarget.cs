using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("NGUI")]
[Tooltip("NGUI HUD Follow. Needs NGUI UIFollowTarget Script attached to the gameobject.")]
public class SetNGUIFollowTarget : FsmStateAction
{
	[RequiredField]
	[CheckForComponent(typeof(UIFollowTarget))]
    [Tooltip("The NGUI HUD GameObject")]
    public FsmOwnerDefault NGUIGameObject;
	
	[RequiredField]
    [Tooltip("The HUD UI Target GameObject")]
    public FsmGameObject followTarget;
	
	private Transform HUD_t;
	
	public override void Reset()
	{
			NGUIGameObject = null;
			followTarget = null;
	}
	
	// Code that runs on entering the state.
	public override void OnEnter()
	{
		
		GameObject go = Fsm.GetOwnerDefaultTarget(NGUIGameObject);
		
		if (go == null) 
			{
				return;
			}
		
		UIFollowTarget UIFollowTarget = go.GetComponent<UIFollowTarget>();
		
		HUD_t = followTarget.Value.transform;
		
		UIFollowTarget.target = HUD_t;
		
		Finish();
		
	}



}
