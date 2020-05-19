using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

    public Material LineMaterial;

    public static Utils Instance;
    void Awake() {
        Instance = this;

    }

    public void DrawLine(Vector3 start, Vector3 end, Color color, float width=.1f, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = LineMaterial;
        lr.material.color = color;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = width;
        lr.endWidth = width;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    public float RoundTo(float num, float amount=4.0f) {

        return Mathf.Round(num * amount) / amount;
    }
 
}
