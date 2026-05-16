using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Lever : Interactable
{
    private bool turnOn = false;

    [SerializeField] private UnityEvent<bool> onLeverSwitched;

    [Header("Animation")]
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private Transform stick;
    [SerializeField] private float animationDuration = 0.5f;
    private float rotationAngle = 60f;
    private Coroutine animationCoroutine = null;

    void Start()
    {
        PromptMessage = turnOn ? "Turn Off" : "Turn On";
        animationCoroutine = StartCoroutine(AnimateLever(turnOn));
    }

    protected override void Interact(GameObject interacter)
    {
        base.Interact(interacter);

        turnOn = !turnOn;
        onLeverSwitched.Invoke(turnOn);
        PromptMessage = turnOn ? "Turn Off" : "Turn On";

        if (animationCoroutine != null) StopCoroutine(animationCoroutine);
        animationCoroutine = StartCoroutine(AnimateLever(turnOn));
    }

    private IEnumerator AnimateLever(bool isOn)
    {
        float elapsed = 0f;

        Quaternion startRotation = stick.localRotation;
        Quaternion targetRotation = Quaternion.Euler(isOn ? rotationAngle : -rotationAngle, 0, 0);

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / animationDuration;

            float curveValue = speedCurve.Evaluate(t);

            stick.localRotation = Quaternion.Slerp(startRotation, targetRotation, curveValue);

            yield return null;
        }

        stick.localRotation = targetRotation;
        animationCoroutine = null;
    }
}