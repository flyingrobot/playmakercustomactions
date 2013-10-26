using UnityEngine;
using HutongGames.PlayMaker;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("ArrayMaker")]
	[Tooltip("Uses line renderer to render vector positions of an arraylist")]
	public class LineRenderArrayList : ArrayListActions
	{
		[ActionSection("Set up")]
		
	    [RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[Tooltip("Start Color")]
		public FsmColor c1;
   		
		[Tooltip("End Color")]
		public FsmColor c2;
		
		[Tooltip("Start Width")]
		public FsmFloat width1;
		
		[Tooltip("End Width")]
		public FsmFloat width2;
		
		[Tooltip("Material")]
		public FsmMaterial lineMaterial;
		
		public bool everyFrame;
		
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("The event to trigger if the action fails ( likely and index is out of range exception)")]
		public FsmEvent failureEvent;
		
		
	    private GameObject go;
		private LineRenderer lineRenderer;
		private Vector3 pos;
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			lineRenderer = go.AddComponent<LineRenderer>();
			lineRenderer.material = lineMaterial.Value;
			lineRenderer.SetColors(c1.Value, c2.Value);
			lineRenderer.SetWidth(width1.Value, width2.Value);
			
			if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			renderArrayList();
			
			if (!everyFrame)
				Finish();
			
		}
	
		// Code that runs every frame.
		public override void OnUpdate()
		{
				if ( SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
				renderArrayList();
		}
		
		void tryArrayList()
		{
			if (! isProxyValid()) 
			Finish();
			
			try{
			pos = (Vector3) proxy.arrayList[0];
			}catch(System.Exception e){
			Debug.Log(e.Message);
			Fsm.Event(failureEvent);
			Finish();
			}
			
		}
		
		
		void renderArrayList()
		{
			if (! isProxyValid()) 
			return;
			
			int lengthOfLineRenderer = proxy.arrayList.Count;
			
			lineRenderer.SetVertexCount(lengthOfLineRenderer);
	        
			int i = 0;
			
	        while (i < lengthOfLineRenderer) {
				
				try{
				pos = (Vector3) proxy.arrayList[i];
				}catch(System.Exception e){
				lineRenderer.SetVertexCount(0);
				Debug.Log(e.Message);
				Fsm.Event(failureEvent);
				return;
				}
				
				lineRenderer.SetPosition(i, pos);
	            i++;
	        }
		}
	
	
	}
}
