using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    public float speed = 5.0f;
    public XRController controller = null;

    private Animator animator = null;

    private readonly List<Finger> fingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    private readonly List<Finger> pointFingers = new List<Finger>
    {
        new Finger(FingerType.Index),
        new Finger(FingerType.Thumb),
    };

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckGrip();
        CheckPointer();

        SmoothFinger(pointFingers);
        SmoothFinger(fingers);

        AnimateFinger(pointFingers);
        AnimateFinger(fingers);
    }

    private void CheckGrip()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            SetFingerTargets(fingers, gripValue);
    }

    private void CheckPointer()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float gripValue))
            SetFingerTargets(pointFingers, gripValue);
    }

    private void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach (Finger finger in fingers)
        {
            finger.target = value;
        }
    }

    private void SmoothFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            AnimateFinger(finger.type.ToString(), finger.current);
        }
    }

    private void AnimateFinger(string finger, float blend)
    {
        animator.SetFloat(finger, blend);
    }
}