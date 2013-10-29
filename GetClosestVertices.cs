using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Mesh")]
	[Tooltip("Gets the closest vertices of a mesh from a vector position and stores them in a Vertex ID arraylist")]
	public class GetClosestVertices : ArrayListActions
	{

		[ActionSection("Source : Mesh and Position")]
		
		[Tooltip("the GameObject to get the mesh from")]
		[CheckForComponent(typeof(MeshFilter))]
		public FsmGameObject mesh;
		
		[Tooltip("Closest to the vector position")]
		public FsmVector3 closestFrom;
		
		[Tooltip("Threshold Distance")]
		public FsmFloat threshold;
		
		public FsmBool everyFrame;
		public FsmBool colorDebug;
		
		[ActionSection("Result : Vertex ID ArrayList")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component to hold vertex IDs")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		private Mesh _mesh;
		private Color[] _colors;
		
		
		public override void Reset()
		{
			gameObject = null;
			reference = null;
			closestFrom = null;
			mesh = null;
			everyFrame = false;
			colorDebug = false;
			threshold = 0f;
		}
	
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			if (! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				Finish();
			}
				DoGetClosestVertices();

			if (!everyFrame.Value)
			{
				Finish();
			}
			
		}
	
		// Code that runs every frame.
		public override void OnUpdate()
		{
				DoGetClosestVertices();
		}
	
		void DoGetClosestVertices()
		{		
			
			if (! isProxyValid()) 
				return;
			
			proxy.arrayList.Clear();
			
			GameObject _go = mesh.Value;
			if (_go==null)
			{
				return;
			}
			
			_mesh = _go.GetComponent<MeshFilter>().mesh;
			if (_mesh ==null)
			{
				return;
			}
			Vector3[] vertices = _mesh.vertices;
			
			int i = 0;
			int index = 0;	

			float sqrDistTest;
			
			_colors = _mesh.colors;
			
        	while (i < vertices.Length) {
				
				var position = vertices[i];
				var wPosition = _go.transform.TransformPoint(position);
				
				sqrDistTest = (closestFrom.Value - wPosition).sqrMagnitude;
				
				if(sqrDistTest < threshold.Value)
				{				
	          
					if (colorDebug.Value) _colors[i] = Color.red;
					try
					{
						proxy.arrayList.Insert(index, i);
						
					}catch(System.Exception e){
						Debug.LogError(e.Message);
					}
					index++;
				}
				else
				{
					if (colorDebug.Value) _colors[i] = Color.green;
				}
				
				
	            i++;
        	}
			
			if (colorDebug.Value) _mesh.colors = _colors;

		}
	
	}
	
}