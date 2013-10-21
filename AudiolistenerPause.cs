using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory(ActionCategory.Audio)]
[Tooltip("Pauses the Audio Listener component on a Game Object.")]
public class AudiolistenerPause : FsmStateAction
{
	[RequiredField]
	[Tooltip("Pause listener")]
	public FsmBool pauseListener;
	
	public override void Reset()
	{
			pauseListener = false;
	}
	
	
	// Code that runs on entering the state.
	public override void OnEnter()
	{
			AudioListener.pause = pauseListener.Value;
			Finish();
	}


}
