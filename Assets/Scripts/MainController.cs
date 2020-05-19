using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainController : MonoBehaviour
{
    public CameraController CamController;
    public TextMeshPro Text;
    
    Camera _cam;

    Vector3 _camStartPos;
    float _camStartZoom;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _camStartPos = _cam.transform.position;
        _camStartZoom = _cam.fieldOfView;
    }

    void MoveCamera(float x, float y) {
        _cam.transform.position += new Vector3(x,y,0);
    }

    void ResetCamera() {
        _cam.transform.position = _camStartPos;
        _cam.fieldOfView = _camStartZoom;
    }

    void GeneratePattern() { 
        PatternGenerator.Instance.Generate();
    }
    void SetText(string str) {
        Text.text = str;
    }

    string GetText() {
        return Text.text;
    }
    float GetZoom() {
        return _cam.fieldOfView;
    }
    void SetZoom(float amount) {
        _cam.fieldOfView = amount;
    }

    void ZoomIn(float amount) {
        _cam.fieldOfView -= amount;
    }

    void ZoomOut(float amount) {
        _cam.fieldOfView += amount;
    }

    Color[] GetPixels() {
        return PixelProcessor.Instance.GetPixels();
    }

    void Update() {

        float h = Input.GetAxis("Horizontal") * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * Time.deltaTime;;

        MoveCamera(h,v);

        float zoomSpeed = 2;
        if (Input.GetKey(KeyCode.E)) {

            ZoomIn(zoomSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q)) {
            ZoomOut(zoomSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            string s = "";
            foreach (Color c in GetPixels()) {
                s += " " + c.r;
            }
            Debug.Log(s);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GeneratePattern();
        }

    }
}
