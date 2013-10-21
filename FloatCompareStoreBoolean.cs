using UnityEngine;
using HutongGames.PlayMaker;

[ActionCategory(ActionCategory.Logic)]
[Tooltip("Sets booleans base on the comparison of 2 Floats")]
public class FloatCompareStoreBoolean : FsmStateAction
{

		[RequiredField]
        [Tooltip("The first float variable.")]
		public FsmFloat float1;

		[RequiredField]
        [Tooltip("The second float variable.")]
		public FsmFloat float2;

		[RequiredField]
        [Tooltip("Tolerance for the Equal test (almost equal).")]
		public FsmFloat tolerance;
		
		[ActionSection("Store Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Sets Bool if Float 1 equals Float 2 (within Tolerance)")]
		public FsmBool storeEqual;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Sets Bool if Float 1 is less than Float 2")]
		public FsmBool storeLessThan;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Event sent if Float 1 is greater than Float 2")]
		public FsmBool storeGreaterThan;
		
        [Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
        public bool everyFrame;

		public override void Reset()
		{
			float1 = 0f;
			float2 = 0f;
			tolerance = 0f;
			storeEqual = false;
			storeLessThan = false;
			storeGreaterThan = false;
		}

		public override void OnEnter()
		{
			DoCompare();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoCompare();
		}

		void DoCompare()
		{

			if (Mathf.Abs(float1.Value - float2.Value) <= tolerance.Value)
			{
				storeEqual.Value = true;
				storeLessThan.Value = false;
				storeGreaterThan.Value = false;
				return;
			}


			if (float1.Value < float2.Value)
			{
				storeLessThan.Value = true;
				storeEqual.Value = false;
				storeGreaterThan.Value = false;
				return;
			}

			if (float1.Value > float2.Value)
			{
				storeGreaterThan.Value = true;
				storeEqual.Value = false;
				storeLessThan.Value = false;
				return;
			}

		}


}
	
