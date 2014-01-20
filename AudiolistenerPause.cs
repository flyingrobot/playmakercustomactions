using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory(ActionCategory.Audio)]
[Tooltip("Pauses the Audio Listener component on a Game Object.")]
public class AudiolistenerPause : FsmStateAction
{
	[RequiredField]
	[Tooltip("Pause listener")]
	public FsmBool pauseListener;
	public bool everyFrame;
	
	public override void Reset()
	{
			pauseListener = false;
	}

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		DoAudiolistenerPause();
			if (!everyFrame)
			{
			Finish();
			}
			
	}

	public override void OnUpdate()
	{
		DoAudiolistenerPause();
	}

	void DoAudiolistenerPause()
	{
		AudioListener.pause = pauseListener.Value;
	}


}
