using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    /// <summary>
    /// The target of the camera.
    /// </summary>
    public Transform CameraTarget;

    /// <summary>
    /// The smoothness coefficient of the cameras movement.
    /// </summary>
    public float SmoothingCoefficient;

    /// <summary>
    /// The x-position of the camera relative to the object to focus.
    /// </summary>
    public float OffsetX;

    /// <summary>
    /// The y-position of the camera relative to the object to focus.
    /// </summary>
    public float OffsetY;

    /// <summary>
    /// The minimum x-position of the camera.
    /// </summary>
    public float MinX;

    /// <summary>
    /// The maximum x-position of the camera.
    /// </summary>
    public float MaxX;

    /// <summary>
    /// The minimum y-position of the camera.
    /// </summary>
    public float MinY;

    /// <summary>
    /// The maximum y-position of the camera.
    /// </summary>
    public float MaxY;

    /// <summary>
    /// The camera velocity.
    /// </summary>
    private Vector3 mCameraVelocity = Vector3.zero;

	/// <summary>
    /// Game loop.
    /// </summary>
	public void FixedUpdate()
    {
        Vector3 targetPos = this.CameraTarget.TransformPoint(this.OffsetX, this.OffsetY, -10);
        Vector3 actualCameraPos = Vector3.SmoothDamp(this.transform.position, targetPos, ref this.mCameraVelocity, this.SmoothingCoefficient);
        this.transform.position = new Vector3(
            Mathf.Clamp(actualCameraPos.x, this.MinX, this.MaxX),
            Mathf.Clamp(actualCameraPos.y, this.MinY, this.MaxY),
            actualCameraPos.z);
    }

}
