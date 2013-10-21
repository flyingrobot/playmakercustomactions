using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Activates/deactivates a NavMeshAgent")]
	public class ActivateNavMeshAgent : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The NavMesh Agent GameObject to activate/deactivate.")]
		[CheckForComponent(typeof(NavMeshAgent))]
        public FsmOwnerDefault gameObject;
		
		[RequiredField]
        [Tooltip("Check to activate, uncheck to deactivate Game Object.")]
        public FsmBool activate;
		
		[Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
		public bool everyFrame;
		
		private NavMeshAgent _agent;
		
		private void _getAgent()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_agent =  go.GetComponent<NavMeshAgent>();
		}
		
		public override void Reset()
		{
			gameObject = null;
			activate = true;
			everyFrame = false;
		}
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			_getAgent();
			
			DoActivateNavMeshAgent();
			
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}
	
		// Code that runs every frame.
		public override void OnUpdate()
		{
			DoActivateNavMeshAgent();
		}
	
		void DoActivateNavMeshAgent()
		{
			
			_agent.enabled = activate.Value;
		}
	}

}