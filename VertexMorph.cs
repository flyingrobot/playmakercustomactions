using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("mesh")]
	[Tooltip("Blends/Morphs Base Mesh to Morph Mesh")]
	public class VertexMorph : FsmStateAction
	{
	
		[ActionSection("Source")]
		
		[Tooltip("the GameObject to get the basemesh from")]
		[CheckForComponent(typeof(MeshFilter))]
		public FsmGameObject baseMesh;
		
		[Tooltip("the GameObject to get the morph target from")]
		[CheckForComponent(typeof(MeshFilter))]
		public FsmGameObject morphMesh;
		
		[RequiredField]
		[Tooltip("Interpolate between Base Mesh and Morph Target by this amount. Value is clamped to 0-1 range. 0 = From Vector; 1 = To Vector; 0.5 = half way between.")]
		public FsmFloat amount;

		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;
		
		private Mesh _baseMesh;
		private Mesh _morphMesh;
		
		private Vector3[] baseMeshVertices;
		private Vector3[] morphMeshVertices;
		private Vector3[] resultMeshVertices;
		
		public override void Reset()
		{
			baseMesh = null;
			morphMesh = null;
			everyFrame = false;
			
		}
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameObject go = baseMesh.Value;
			
			if (go==null)
			{
				return;
			}
			
			GameObject _go = morphMesh.Value;
			
			if (_go==null)
			{
				return;
			}
			
			_baseMesh = go.GetComponent<MeshFilter>().mesh;
			baseMeshVertices = _baseMesh.vertices;
					
			_morphMesh = _go.GetComponent<MeshFilter>().mesh;
			morphMeshVertices = _morphMesh.vertices;

        	resultMeshVertices = _baseMesh.vertices;

			
			DoMorph();

			if (!everyFrame)
			{
				Finish();
			}
		}
	
		// Code that runs every frame.
		public override void OnUpdate()
		{
			DoMorph();
		}
	
		void DoMorph()
		{
		
			int p = 0;
			
      		while (p < baseMeshVertices.Length) 
				{
          		resultMeshVertices[p] = Vector3.Lerp(baseMeshVertices[p], morphMeshVertices[p], amount.Value);
           		p++;
        		}
			
			_baseMesh.vertices = resultMeshVertices;
			_baseMesh.RecalculateBounds();

		}
		
		
	}
	
}