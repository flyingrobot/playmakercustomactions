using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Activates/deactivates a NavMeshObstacle")]
	public class ActivateNavMeshObstacle : FsmStateAction
	{
	
		[RequiredField]
        [Tooltip("The NavMesh Obstacle GameObject to activate/deactivate.")]
        public FsmOwnerDefault gameObject;
		
		[RequiredField]
        [Tooltip("Check to activate, uncheck to deactivate Game Object.")]
        public FsmBool activate;
		
		[Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
		public bool everyFrame;
		
		private NavMeshObstacle _obstacle;
		
		private void _getObstacle()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_obstacle =  go.GetComponent<NavMeshObstacle>();
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
			_getObstacle();
			
			DoActivateNavMeshObstacle();
			
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}
	
		// Code that runs every frame.
		public override void OnUpdate()
		{
			DoActivateNavMeshObstacle();
		}
	
		void DoActivateNavMeshObstacle()
		{
			
			_obstacle.enabled = activate.Value;
		}
	}
	
	
}
