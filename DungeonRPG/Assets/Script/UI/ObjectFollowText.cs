using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonRPG.UI
{
    public class ObjectFollowText : MonoBehaviour
    {
        /// <summary>
        /// The target object of the follow text.
        /// </summary>
        public Transform TargetObject;

        /// <summary>
        /// The offset from the target object.
        /// </summary>
        public Vector3 ObjectOffset;

        /// <summary>
        /// If true the object will be visible even if the target is off screen. Default: false
        /// </summary>
        public bool ClampObjectToScreen = false;

        /// <summary>
        /// The border size of the clamp. Default: 0.05
        /// </summary>
        public float ClampBorderSize = 0.05F;

        /// <summary>
        /// If true the script uses the camera tagged as MainCamera as the camera.
        /// </summary>
        public bool UseMainCamera = true;

        /// <summary>
        /// If <see cref="UseMainCamera"/> is <code>false</code> then this camera is the camera used.
        /// </summary>
        public Camera CameraToUse;

        /// <summary>
        /// Camera currently in use.
        /// </summary>
        private Camera mCameraInUse;

        /// <summary>
        /// The transform of the object this script is attached to.
        /// </summary>
        private Transform mCurrentObjectTransform;

        /// <summary>
        /// The transform of the camera.
        /// </summary>
        private Transform mCameraTransform;

        /// <summary>
        /// Init of this code.
        /// </summary>
        private void Start()
        {
            this.mCurrentObjectTransform = this.transform;
            if (this.UseMainCamera)
                this.mCameraInUse = Camera.main;
            else
                this.mCameraInUse = this.CameraToUse;
            this.mCameraTransform = this.mCameraInUse.transform;
        }

        /// <summary>
        /// Frame loop.
        /// </summary>
        private void Update()
        {
            if (this.ClampObjectToScreen)
            {
                Vector3 relPosition = this.mCameraTransform.InverseTransformPoint(this.TargetObject.position);
                relPosition.z = Mathf.Max(relPosition.z, 1.0F);
                this.mCurrentObjectTransform.position = this.mCameraInUse.WorldToScreenPoint(this.mCameraTransform.TransformPoint(relPosition + this.ObjectOffset));
                this.mCurrentObjectTransform.position = new Vector3(
                    Mathf.Clamp(this.mCurrentObjectTransform.position.x, this.ClampBorderSize, 1.0F - this.ClampBorderSize),
                    Mathf.Clamp(this.mCurrentObjectTransform.position.y, this.ClampBorderSize, 1.0F - this.ClampBorderSize),
                    this.mCurrentObjectTransform.position.z);
            }
            else
            {
                this.mCurrentObjectTransform.position = this.mCameraInUse.WorldToScreenPoint(this.TargetObject.position + this.ObjectOffset);
            }
        }
    }

}