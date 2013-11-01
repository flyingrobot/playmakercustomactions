using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Performs various Operations on the vertices of the mesh specified by an ArrayList")]
	public class OperateOnVertices : ArrayListActions
	{
		public enum Operations
		{
			Push,
			Add,
			setVertexColor
		}
		
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component holding the vertex IDs")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		public Operations operation = Operations.Add;
		
		[ActionSection("Source")]
		
		[Tooltip("the GameObject to get the mesh from")]
		[CheckForComponent(typeof(MeshFilter))]
		public FsmGameObject mesh;
		
		[ActionSection("Effectors")]

		public FsmVector3 vectorInput;

		public FsmFloat scalarInput;
		
		public FsmColor colorInput;
		
		public FsmBool everyFrame;
		
		private Mesh _mesh;
		private Vector3[] vertices;
		private Vector3[] vertices1;
		private Vector3[] normals;
		private Color[] _colors;
		
		private int i;
		private bool elementContained;
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			if (! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				Finish();
			}
			
			if (! isProxyValid()) 
				Finish();
			
			GameObject _go = mesh.Value;
			
			if (_go==null)
			{
				Finish();
			}
			
			_mesh = _go.GetComponent<MeshFilter>().mesh;
			
			if (_mesh ==null)
			{
				Finish();
			}
			
				vertices = _mesh.vertices;
				vertices1 = _mesh.vertices;
				normals = _mesh.normals;
			
				DoVector3OperatorOnVertexIDs();

			if (!everyFrame.Value)
			{
				Finish();
			}
		}
	
		// Code that runs every frame.
		public override void OnUpdate()
		{
			DoVector3OperatorOnVertexIDs();
		}
	
		void DoVector3OperatorOnVertexIDs()
		{
			switch (operation)
			{
				case Operations.Push:
				
					i = 0;
					
					elementContained = false;
					
					while (i < vertices.Length) {
						
						elementContained = proxy.arrayList.Contains(i);
						
						if(elementContained)
						{
							vertices1[i] = vertices[i] + normals[i] * scalarInput.Value;
						}
						
						i++;
		        	}
					
					_mesh.vertices = vertices1;
					
					break;

				case Operations.Add:
					
					i = 0;
					
					elementContained = false;
					
					while (i < vertices.Length) {
						
						elementContained = proxy.arrayList.Contains(i);
						
						if(elementContained)
						{
							vertices1[i] = vertices[i] + vectorInput.Value;
						}
						
						i++;
		        	}
					
					_mesh.vertices = vertices1;
					
					break;
				
				case Operations.setVertexColor:
					
					i = 0;
				
					_colors = _mesh.colors;
					
					elementContained = false;
					
					while (i < vertices.Length) {
						
						elementContained = proxy.arrayList.Contains(i);
						
						if(elementContained)
						{
							_colors[i] = colorInput.Value;
							
						}
						
						i++;
		        	}
					
					_mesh.colors = _colors;
					
					break;
				

			}
		}
	}
	
}

