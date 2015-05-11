// Copyright (c) 2015 James Liu
//	
// See the LISCENSE file for copying permission.

using UnityEngine;


namespace DanmakU.DanmakuControllers {

	[System.Serializable]
	public class DelayedAngleChange : IDanmakuController {

		[SerializeField]
		private RotationMode rotationMode;

		[SerializeField]
		private float delay;

		[SerializeField]
		private DynamicFloat angle;

		[SerializeField]
		private bool setAngV;

		[SerializeField]
		private DynamicFloat angularVelocity;

		public Transform Target {
			get;
			set;
		}

		#region implemented abstract members of IDanmakuController

		public void UpdateDanmaku (Danmaku danmaku, float dt) {
			float time = danmaku.Time;
			if(time >= delay && time - dt <= delay) {
				float baseAngle = angle.Value;
				switch(rotationMode) {
					case RotationMode.Relative:
						baseAngle += danmaku.Rotation;
						break;
					case RotationMode.Object:
						baseAngle += DanmakuUtil.AngleBetween2D (danmaku.Position, Target.position);
						break;
					case RotationMode.Absolute:
						break;
				}
				danmaku.Rotation = baseAngle;
				if(setAngV)
					danmaku.AngularSpeed = angularVelocity;
			}
		}

		#endregion
	}

}