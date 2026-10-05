using UnityEngine;

// Auto-creates the 21 landmark spheres and the lines between them,
// then hands the spheres to HandTracking. Put this on the same
// GameObject as HandTracking and leave HandTracking's Hand Points empty.
[RequireComponent(typeof(HandTracking))]
public class HandBuilder : MonoBehaviour
{
    public float pointSize = 0.3f;
    public Color pointColor = Color.white;
    public Color lineColor = Color.cyan;

    // MediaPipe hand landmark connections (21 bones)
    static readonly int[,] bones = {
        {0,1},{1,2},{2,3},{3,4},          // thumb
        {0,5},{5,6},{6,7},{7,8},          // index
        {5,9},{9,10},{10,11},{11,12},     // middle
        {9,13},{13,14},{14,15},{15,16},   // ring
        {13,17},{0,17},{17,18},{18,19},{19,20} // pinky + palm
    };

    void Awake()
    {
        var tracking = GetComponent<HandTracking>();
        var points = new GameObject[21];

        for (int i = 0; i < 21; i++)
        {
            var p = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            p.name = "Point" + i;
            p.transform.SetParent(transform, false);
            p.transform.localScale = Vector3.one * pointSize;
            p.GetComponent<Renderer>().material.color = pointColor;
            points[i] = p;
        }
        tracking.handPoints = points;

        var lineMat = new Material(Shader.Find("Sprites/Default"));
        for (int b = 0; b < bones.GetLength(0); b++)
        {
            var go = new GameObject($"Line {bones[b, 0]}-{bones[b, 1]}");
            go.transform.SetParent(transform, false);

            var lr = go.AddComponent<LineRenderer>();
            lr.material = lineMat;
            lr.positionCount = 2;
            lr.startColor = lr.endColor = lineColor;

            var lc = go.AddComponent<LineCode>();
            lc.origin = points[bones[b, 0]].transform;
            lc.destination = points[bones[b, 1]].transform;
        }
    }
}
