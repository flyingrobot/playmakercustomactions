using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory(ActionCategory.Math)]
[Tooltip("Takes a float and offsets it's value (both +ve and -ve) by a percentage")]
public class OffsetFloatByPercentage : FsmStateAction
{

	[RequiredField]
	public FsmFloat inputValue;
	[RequiredField]
	public FsmFloat offsetPercentage;
	[RequiredField]
	[UIHint(UIHint.Variable)]
	public FsmFloat storeResult;
	[Tooltip("Repeat every frame while the state is active.")]
	public bool everyFrame;
	
	private float min;
	private float max;
	
	public override void Reset()
	{
	    inputValue = 0f;
		offsetPercentage = 0f;
		storeResult = null;
		}	

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		max = inputValue.Value * offsetPercentage.Value/100;
		min = inputValue.Value * offsetPercentage.Value/100 * (-1);
		storeResult.Value = inputValue.Value + (Random.Range(min, max));
		
		if (!everyFrame)
		{
		    Finish();
		}
	}
	
	public override void OnUpdate()
	{
		max = inputValue.Value * offsetPercentage.Value/100;
		min = inputValue.Value * offsetPercentage.Value/100 * (-1);
		storeResult.Value = inputValue.Value + (Random.Range(min, max));
	}


}
