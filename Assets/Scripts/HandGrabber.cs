using UnityEngine;

// Pinch thumb + index together to grab the nearest Rigidbody object,
// open your fingers to drop (or throw) it.
// Put this on the same GameObject as HandTracking and HandBuilder.
[RequireComponent(typeof(HandTracking))]
public class HandGrabber : MonoBehaviour
{
    [Tooltip("Thumb-index gap divided by palm size. Lower = must pinch tighter.")]
    public float pinchThreshold = 0.35f;
    [Tooltip("Gap ratio needed to let go. Keep above pinchThreshold so it doesn't flicker.")]
    public float releaseThreshold = 0.5f;
    [Tooltip("How far from the pinch point an object can be grabbed.")]
    public float grabRadius = 1.0f;
    [Tooltip("Higher = held object follows the fingers more tightly.")]
    public float followSmoothing = 20f;
    [Tooltip("Scales throw speed on release. 0 = just drop it.")]
    public float throwMultiplier = 1f;
    public Color pinchColor = Color.green;

    HandTracking tracking;
    Rigidbody held;
    Vector3 holdOffset;
    Vector3 pinchPoint, lastPinchPoint, velocity;
    bool pinching;
    Renderer thumbRenderer, indexRenderer;
    Color normalColor;

    void Start()
    {
        tracking = GetComponent<HandTracking>();

        // Turn off the joint colliders so they don't shove the cubes around
        foreach (var p in tracking.handPoints)
        {
            var c = p.GetComponent<Collider>();
            if (c) c.enabled = false;
        }

        thumbRenderer = tracking.handPoints[4].GetComponent<Renderer>();
        indexRenderer = tracking.handPoints[8].GetComponent<Renderer>();
        normalColor = thumbRenderer.material.color;
    }

    void Update()
    {
        var pts = tracking.handPoints;
        Vector3 thumb = pts[4].transform.position;   // thumb tip
        Vector3 index = pts[8].transform.position;   // index tip
        float palm = Vector3.Distance(pts[0].transform.position, pts[9].transform.position);
        if (palm < 0.01f) return; // no hand data yet

        float ratio = Vector3.Distance(thumb, index) / palm;
        pinchPoint = (thumb + index) * 0.5f;

        if (!pinching && ratio < pinchThreshold)
        {
            pinching = true;
            TryGrab();
        }
        else if (pinching && ratio > releaseThreshold)
        {
            pinching = false;
            Release();
        }

        // Smoothed hand velocity, used for throwing
        Vector3 instant = (pinchPoint - lastPinchPoint) / Mathf.Max(Time.deltaTime, 0.0001f);
        velocity = Vector3.Lerp(velocity, instant, 0.3f);
        lastPinchPoint = pinchPoint;

        // Visual feedback: fingertips change color while pinching
        Color c = pinching ? pinchColor : normalColor;
        thumbRenderer.material.color = c;
        indexRenderer.material.color = c;
    }

    void FixedUpdate()
    {
        if (held == null) return;
        Vector3 target = pinchPoint + holdOffset;
        held.MovePosition(Vector3.Lerp(held.position, target, followSmoothing * Time.fixedDeltaTime));
    }

    void TryGrab()
    {
        float best = float.MaxValue;
        foreach (var col in Physics.OverlapSphere(pinchPoint, grabRadius))
        {
            var rb = col.attachedRigidbody;
            if (rb == null || rb.isKinematic) continue;
            float d = Vector3.Distance(pinchPoint, col.ClosestPoint(pinchPoint));
            if (d < best) { best = d; held = rb; }
        }

        if (held != null)
        {
            held.isKinematic = true;
            holdOffset = held.position - pinchPoint;
        }
    }

    void Release()
    {
        if (held == null) return;
        held.isKinematic = false;
        held.linearVelocity = velocity * throwMultiplier;
        held = null;
    }
}
