using System.Globalization;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject[] handPoints;

    void Update()
    {
        string data = udpReceive.data;
        if (string.IsNullOrEmpty(data)) return;

        data = data.Trim('[', ']');
        string[] points = data.Split(',');
        if (points.Length < 63) return;

        for (int i = 0; i < 21; i++)
        {
            float x = 7 - float.Parse(points[i * 3], CultureInfo.InvariantCulture) / 100;
            float y = float.Parse(points[i * 3 + 1], CultureInfo.InvariantCulture) / 100;
            float z = float.Parse(points[i * 3 + 2], CultureInfo.InvariantCulture) / 100;
            handPoints[i].transform.localPosition = new Vector3(x, y, z);
        }
    }
}
