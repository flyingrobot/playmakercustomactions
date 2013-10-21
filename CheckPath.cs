using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;
using System;
using System.IO;
using System.Text;

[ActionCategory(ActionCategory.String)]
[Tooltip("Check if path exists")]
public class CheckPath : FsmStateAction
{
	[RequiredField]
    [Tooltip("Absolute path of the text file to import")]
	public FsmString textFile;
	
	[Tooltip("File exists")]
    public FsmBool fileExists;
	
	public override void Reset()
	{
		textFile = null;
		fileExists = false;
	}
	
	// Code that runs on entering the state.
	public override void OnEnter()
	{
		string path = textFile.Value;
		
		if (File.Exists(path))
        {		
		fileExists.Value = true;
		}
		Finish();
	}


}
