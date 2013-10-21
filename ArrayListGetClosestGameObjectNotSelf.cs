using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory("ArrayMaker/ArrayList")]
[Tooltip("Return the closest GameObject within an arrayList from a transform or position. Excludes self if already a member of array.")]
public class ArrayListGetClosestGameObjectNotSelf : ArrayListActions
{

		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
		
		[Tooltip("Compare the distance of the items in the list to the position of this gameObject")]
		public FsmGameObject distanceFrom;
		
		public bool everyframe;
		
		[ActionSection("Result")]
		
		[UIHint(UIHint.Variable)]
		public FsmGameObject closestGameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmInt closestIndex;
		
		
		
		
		public override void Reset()
		{
		
			gameObject = null;
			reference = null;
			distanceFrom = null;
			closestGameObject = null;
			closestIndex = null;
			
			everyframe = true;
		}
		
		
		public override void OnEnter()
		{

			if (! SetUpArrayListProxyPointer(Fsm.GetOwnerDefaultTarget(gameObject),reference.Value) )
			{
				Finish();
			}
			
			DoFindClosestGo();
			
			if (!everyframe)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			
			DoFindClosestGo();
		}
		
		void DoFindClosestGo()
		{
			Vector3 root;
			
			if (! isProxyValid())
			{
				return;
			}
						
			GameObject _rootGo = distanceFrom.Value;
		
			if (_rootGo!=null)
			{
				root = _rootGo.transform.position;
			}
			
			float sqrDist = Mathf.Infinity;
		
			int _index = 0;
			float sqrDistTest;
			foreach(GameObject _go in proxy.arrayList)
			{
				
				if (_go!=null && _go!=_rootGo) 
				{
					sqrDistTest = (_go.transform.position - root).sqrMagnitude;
					if (sqrDistTest<= sqrDist)
					{
						sqrDist = sqrDistTest;
						closestGameObject.Value = _go;
						closestIndex.Value = _index;
					}
				}
				_index++;
			}

		}
		
}
	
}


