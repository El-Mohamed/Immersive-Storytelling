using System;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLookAdvanced : MonoBehaviour
{
    public float sensitivityX = 5f;

    public float sensitivityY = 5f;

    public float minimumX = -360f;

    public float maximumX = 360f;

    public float minimumY = -90f;

    public float maximumY = 90f;

    public float smoothSpeed = 20f;

    private float verticalAcceleration;

    private float rotationX;

    private float smoothRotationX;

    private float rotationY;

    private float smoothRotationY;

    private Vector3 vMousePos;

    public float Speed = 100f;

    private bool bActive;

    private bool IsCursorLock
    {
        get
        {
            return Screen.lockCursor;
        }
        set
        {
            Screen.lockCursor = value;
        }
    }

    public MouseLookAdvanced()
    {
    }

    private void Start()
    {
        this.rotationY = -base.transform.localEulerAngles.x;
        this.rotationX = base.transform.localEulerAngles.y;
        this.smoothRotationX = base.transform.localEulerAngles.y;
        this.smoothRotationY = -base.transform.localEulerAngles.x;
    }

    private void Update()
    {
        this.verticalAcceleration = 0f;
        if (Input.GetMouseButtonDown(1))
        {
            Screen.lockCursor = !Screen.lockCursor;
            Cursor.visible = !Cursor.visible;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.verticalAcceleration = 1f;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.verticalAcceleration = -1f;
        }
        if (!this.IsCursorLock)
        {
            return;
        }
        MouseLookAdvanced axis = this;
        axis.rotationX = axis.rotationX + Input.GetAxis("Mouse X") * this.sensitivityX;
        MouseLookAdvanced mouseLookAdvanced = this;
        mouseLookAdvanced.rotationY = mouseLookAdvanced.rotationY + Input.GetAxis("Mouse Y") * this.sensitivityY;
        this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
        MouseLookAdvanced mouseLookAdvanced1 = this;
        mouseLookAdvanced1.smoothRotationX = mouseLookAdvanced1.smoothRotationX + (this.rotationX - this.smoothRotationX) * this.smoothSpeed * Time.smoothDeltaTime;
        MouseLookAdvanced mouseLookAdvanced2 = this;
        mouseLookAdvanced2.smoothRotationY = mouseLookAdvanced2.smoothRotationY + (this.rotationY - this.smoothRotationY) * this.smoothSpeed * Time.smoothDeltaTime;
        base.transform.localEulerAngles = new Vector3(-this.smoothRotationY, this.smoothRotationX, 0f);
        Vector3 vector3 = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 vector31 = base.transform.rotation * vector3;
        Transform speed = base.transform;
        speed.position = speed.position + ((vector31 * this.Speed) * Time.smoothDeltaTime);
        Transform transforms = base.transform;
        transforms.position = transforms.position + new Vector3(0f, this.Speed / 2f * this.verticalAcceleration * Time.smoothDeltaTime, 0f);
        Transform axis1 = base.transform;
        axis1.position = axis1.position + (((base.transform.rotation * Vector3.forward) * Input.GetAxis("Mouse ScrollWheel")) * 200f);
    }
}