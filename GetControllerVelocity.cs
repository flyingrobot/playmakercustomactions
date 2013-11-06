using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Gets Character Controller Horizontal velocity and speed.")]
	public class GetControllerVelocity : FsmStateAction
	{
		[RequiredField]
	    [Tooltip("The GameObject with Character Controller")]
		[CheckForComponent(typeof(CharacterController))]
	    public FsmOwnerDefault gameObject;
		
		public FsmBool everyFrame;
		
		public FsmVector3 storeVelocity;
		public FsmFloat storeSpeed;
		
		private CharacterController controller;
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			controller = go.GetComponent<CharacterController>();
			
			DoGetControllerVelocity();
			
			if (!everyFrame.Value)
				{
					Finish();
				}
			
		}
	
		// Code that runs every frame.
		public override void OnUpdate()
		{
			DoGetControllerVelocity();
		}
		
		void DoGetControllerVelocity()
		{
			Vector3 horizontalVelocity = controller.velocity;
			horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
			float horizontalSpeed = horizontalVelocity.magnitude;
			
			storeVelocity.Value = horizontalVelocity;
			storeSpeed.Value = horizontalSpeed;
			
		}
		
	
	}
	
}
