using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory("NGUI")]
[Tooltip("Adds NGUI HUD text floating text. Needs NGUI HUDText script attached to the gameobject.")]
public class AddNGUIFloatingText : FsmStateAction
{
	[RequiredField]
	[CheckForComponent(typeof(HUDText))]
    [Tooltip("The NGUI HUD GameObject")]
    public FsmOwnerDefault NGUIGameObject;
	
	[RequiredField]
    [Tooltip("Some text to add")]
    public FsmString text;
	
	[RequiredField]
    [Tooltip("Text Color")]
    public FsmColor color;
	
	[RequiredField]
    [Tooltip("Stay Duration")]
    public FsmFloat duration;
	
	public override void Reset()
	{
		NGUIGameObject = null;
		text = null;
		color = null;
		duration = 0f;
	}
	
	// Code that runs on entering the state.
	public override void OnEnter()
	{
		GameObject go = Fsm.GetOwnerDefaultTarget(NGUIGameObject);
		
		if (go == null) 
			{
				return;
			}
		
		HUDText hudText = go.GetComponent<HUDText>();
		hudText.Add(text.Value, color.Value, duration.Value);
		
		Finish();
	}



}
