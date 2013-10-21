using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;
using System;
using System.IO;
using System.Text;

[ActionCategory(ActionCategory.String)]
[Tooltip("imports a text file into a string at runtime")]
public class ImportTXT : FsmStateAction
{
	[RequiredField]
    [Tooltip("Absolute path of the text file to import")]
	public FsmString textFile;
	
	[RequiredField]
	[UIHint(UIHint.Variable)]
	[Tooltip("Store the content in a string.")]
    public FsmString storeResult;
	
	//[UIHint(UIHint.Variable)]
	[Tooltip("File exists")]
    public FsmBool fileExists;
	
	public override void Reset()
	{
		textFile = null;
		storeResult = null;
		fileExists = false;
	}

	// Code that runs on entering the state.
	public override void OnEnter()
	{
		string path = textFile.Value;
		
		if (File.Exists(path))
        {
		string text = System.IO.File.ReadAllText(path);
		storeResult.Value = text;
		fileExists.Value = true;
		}
		Finish();
		
	}


}
