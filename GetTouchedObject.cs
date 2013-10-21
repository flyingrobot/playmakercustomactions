using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory(ActionCategory.Device)]
[Tooltip("Gets the touched object")]
public class GetTouchedObject : FsmStateAction
{
	
	[RequiredField]
	[Tooltip("How far from the camera is the Game Object pickable.")]
	public FsmFloat pickDistance;
	
	[Tooltip("Only detect touches that match this fingerID, or set to None.")]
	public FsmInt fingerId;
	
	[ActionSection("Events")]
	
	[Tooltip("Event to send on touch began.")]
	public FsmEvent touchBegan;
	
	[Tooltip("Event to send on touch moved.")]
	public FsmEvent touchMoved;
	
	[Tooltip("Event to send on stationary touch.")]
	public FsmEvent touchStationary;
	
	[Tooltip("Event to send on touch ended.")]
	public FsmEvent touchEnded;
	
	[Tooltip("Event to send on touch cancel.")]
	public FsmEvent touchCanceled;

	[ActionSection("Store Results")]
	
	[UIHint(UIHint.Variable)]
	[Tooltip("Store the fingerId of the touch.")]
	public FsmInt storeFingerId;
	
	[UIHint(UIHint.Variable)]
	[Tooltip("Store the touched Game Object")]
	public FsmGameObject storeCollider;
	
	public override void Reset()
		{
//			gameObject = null;
			pickDistance = 100;
			fingerId = new FsmInt { UseVariable = true };
		
			touchBegan = null;
			touchMoved = null;
			touchStationary = null;
			touchEnded = null;
			touchCanceled = null;
		
			storeFingerId = null;	
			storeCollider = null;
		}
	
	// Code that runs on entering the state.
	public override void OnEnter()
	{
		
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
		if (Camera.main == null)
			{
				LogError("No MainCamera defined!");
				Finish();
				return;
			}

			if (Input.touchCount > 0)
			{

				foreach (var touch in Input.touches)
				{
					if (fingerId.IsNone || touch.fingerId == fingerId.Value)
					{
						var screenPos = touch.position;

						RaycastHit hitInfo;
						
						if( Physics.Raycast(Camera.main.ScreenPointToRay(screenPos), out hitInfo, pickDistance.Value))
							{
						
						// Store hitInfo so it can be accessed by other actions
						// E.g., Get Raycast Hit Info
								Fsm.RaycastHitInfo = hitInfo;
						
								if (hitInfo.collider != null)
								{

									storeCollider.Value = hitInfo.collider.gameObject;
									storeFingerId.Value = touch.fingerId;
								
									switch (touch.phase)
									{
										case TouchPhase.Began:
											Fsm.Event(touchBegan);
											return;
	
										case TouchPhase.Moved:
											Fsm.Event(touchMoved);
											return;
	
										case TouchPhase.Stationary:
											Fsm.Event(touchStationary);
											return;
	
										case TouchPhase.Ended:
											Fsm.Event(touchEnded);
											return;
	
										case TouchPhase.Canceled:
											Fsm.Event(touchCanceled);
											return;
									}
								}
							}
					}
				}
			}
		

	}

}
