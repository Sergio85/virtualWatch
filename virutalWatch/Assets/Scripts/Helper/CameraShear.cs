using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
public class CameraShear : MonoBehaviour
{

    public float multiplyEyePosition = 10.0f;
    public GameObject screen;
    public GameObject cam;
    public Camera eye;
    bool init, setTL, setBL, setBR;
    public bool startCam;
    public bool estimateViewFrustum = true;
    public GameObject GO_BottomLeft, GO_BottomRight, GO_TopLeft;
    Vector3 pa, pb, pc;
    public Vector3 positionFromTracker;
    bool setX, setY, setZ; 


    public void GeneralizedPerspectiveProjection(Vector3 pe)
    {
        Vector3 va, vb, vc;
        Vector3 vr, vu, vn;

        float near = eye.nearClipPlane;
        float far = eye.farClipPlane;
        float left, right, bottom, top, eyedistance;

        Matrix4x4 transformMatrix;
        Matrix4x4 projectionM;
        Matrix4x4 eyeTranslateM;

        //Calculate the orthonormal for the screen (the screen coordinate system
        vr = pb - pa;
        vr.Normalize();
        vu = pc - pa;
        vu.Normalize();
        vn = -Vector3.Cross(vr, vu);
        vn.Normalize();

        //left eye right eye
        Vector3 peLeft = new Vector3(pe.x + eye.transform.localPosition.x, pe.y, pe.z);
        Vector3 peRight = new Vector3(pe.x - eye.transform.localPosition.x, pe.y, pe.z);

        //Debug.Log("peLeft: " + peLeft);
        //Debug.Log("peRight: " + peRight);

        //Calculate the vector from eye (pe) to screen corners (pa, pb, pc)
        va = pa - peLeft;
        vb = pb - peLeft;
        vc = pc - peLeft;

        //Get the distance;; from the eye to the screen plane
        eyedistance = -(Vector3.Dot(va, vn));

        //Get the varaibles for the off center projection
        left = (Vector3.Dot(vr, va) * near) / eyedistance;
        right = (Vector3.Dot(vr, vb) * near) / eyedistance;
        bottom = (Vector3.Dot(vu, va) * near) / eyedistance;
        top = (Vector3.Dot(vu, vc) * near) / eyedistance;

        //Get this projection
        projectionM = PerspectiveOffCenter(left, right, bottom, top, near, far);

        //Fill in the transform matrix
        transformMatrix = new Matrix4x4();
        transformMatrix[0, 0] = vr.x;   transformMatrix[0, 1] = vr.y;   transformMatrix[0, 2] = vr.z;   transformMatrix[0, 3] = 0;
        transformMatrix[1, 0] = vu.x;   transformMatrix[1, 1] = vu.y;   transformMatrix[1, 2] = vu.z;   transformMatrix[1, 3] = 0;
        transformMatrix[2, 0] = vn.x;   transformMatrix[2, 1] = vn.y;   transformMatrix[2, 2] = vn.z;   transformMatrix[2, 3] = 0;
        transformMatrix[3, 0] = 0;      transformMatrix[3, 1] = 0;      transformMatrix[3, 2] = 0;      transformMatrix[3, 3] = 1;

        //Now for the eye transform
        eyeTranslateM = new Matrix4x4();
        eyeTranslateM[0, 0] = 1;    eyeTranslateM[0, 1] = 0;    eyeTranslateM[0, 2] = 0;    eyeTranslateM[0, 3] = -peLeft.x;
        eyeTranslateM[1, 0] = 0;    eyeTranslateM[1, 1] = 1;    eyeTranslateM[1, 2] = 0;    eyeTranslateM[1, 3] = -peLeft.y;
        eyeTranslateM[2, 0] = 0;    eyeTranslateM[2, 1] = 0;    eyeTranslateM[2, 2] = 1;    eyeTranslateM[2, 3] = -peLeft.z;
        eyeTranslateM[3, 0] = 0;    eyeTranslateM[3, 1] = 0;    eyeTranslateM[3, 2] = 0;    eyeTranslateM[3, 3] = 1f;

        eye.projectionMatrix = projectionM * transformMatrix * eyeTranslateM;
        eye.worldToCameraMatrix = Matrix4x4.identity;




        //------------------------------------------------------------------------------------




        //Calculate the vector from eye (pe) to screen corners (pa, pb, pc)
        va = pa - peRight;
        vb = pb - peRight;
        vc = pc - peRight;

        //Get the distance;; from the eye to the screen plane
        eyedistance = -(Vector3.Dot(va, vn));

        //Get the varaibles for the off center projection
        left = (Vector3.Dot(vr, va) * near) / eyedistance;
        right = (Vector3.Dot(vr, vb) * near) / eyedistance;
        bottom = (Vector3.Dot(vu, va) * near) / eyedistance;
        top = (Vector3.Dot(vu, vc) * near) / eyedistance;

        //Get this projection
        projectionM = PerspectiveOffCenter(left, right, bottom, top, near, far);

        //Fill in the transform matrix
        transformMatrix = new Matrix4x4();
        transformMatrix[0, 0] = vr.x;   transformMatrix[0, 1] = vr.y;   transformMatrix[0, 2] = vr.z;   transformMatrix[0, 3] = 0; 
        transformMatrix[1, 0] = vu.x;   transformMatrix[1, 1] = vu.y;   transformMatrix[1, 2] = vu.z;   transformMatrix[1, 3] = 0;
        transformMatrix[2, 0] = vn.x;   transformMatrix[2, 1] = vn.y;   transformMatrix[2, 2] = vn.z;   transformMatrix[2, 3] = 0;
        transformMatrix[3, 0] = 0;      transformMatrix[3, 1] = 0;      transformMatrix[3, 2] = 0;      transformMatrix[3, 3] = 1;

        //Now for the eye transform
        eyeTranslateM = new Matrix4x4();
        eyeTranslateM[0, 0] = 1;    eyeTranslateM[0, 1] = 0;    eyeTranslateM[0, 2] = 0;    eyeTranslateM[0, 3] = -peRight.x;
        eyeTranslateM[1, 0] = 0;    eyeTranslateM[1, 1] = 1;    eyeTranslateM[1, 2] = 0;    eyeTranslateM[1, 3] = -peRight.y;
        eyeTranslateM[2, 0] = 0;    eyeTranslateM[2, 1] = 0;    eyeTranslateM[2, 2] = 1;    eyeTranslateM[2, 3] = -peRight.z;
        eyeTranslateM[3, 0] = 0;    eyeTranslateM[3, 1] = 0;    eyeTranslateM[3, 2] = 0;    eyeTranslateM[3, 3] = 1f;

        //Multiply all together
        //finalProjection = new Matrix4x4();
        //finalProjection = Matrix4x4.identity * projectionM * transformMatrix * eyeTranslateM;
        eye.projectionMatrix = projectionM * transformMatrix * eyeTranslateM;
        eye.worldToCameraMatrix = Matrix4x4.identity;

        //finally return
        //return finalProjection;
        if (estimateViewFrustum)
        {
            Vector3 err = new Vector3((eye.transform.position.x) / 2F, 0F, 0F);
            Quaternion q1 = eye.transform.rotation;
            q1.SetLookRotation((0.5F * (pb + pc) - pe) - err, vu);
            eye.transform.rotation = q1;

            if (eye.aspect >= 1.0F)
            {
                eye.fieldOfView = Mathf.Rad2Deg * Mathf.Atan((vu.magnitude + vr.magnitude) / va.magnitude);
            }
            else
            {
                eye.fieldOfView = Mathf.Rad2Deg / eye.aspect * Mathf.Atan((vu.magnitude + vr.magnitude) / va.magnitude);
            }
        }

        //Debug.Log("camEye: " + eye.transform.forward);
    }

    public static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
    {
        float x = 2.0F * near / (right - left);
        float y = 2.0F * near / (top - bottom);
        float a = (right + left) / (right - left);
        float b = (top + bottom) / (top - bottom);
        float c = (far + near) / (near - far);
        float d = (2.0F * far * near) / (near - far);
        float e = -1.0F;
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x;    m[0, 1] = 0;    m[0, 2] = a;    m[0, 3] = 0;
        m[1, 0] = 0;    m[1, 1] = y;    m[1, 2] = b;    m[1, 3] = 0;
        m[2, 0] = 0;    m[2, 1] = 0;    m[2, 2] = c;    m[2, 3] = d;
        m[3, 0] = 0;    m[3, 1] = 0;    m[3, 2] = e;    m[3, 3] = 0;
        return m;
    }

    void Start()
    {
        init = true;
        setBL = false;
        setBR = false;
        setTL = false;
        startCam = true;
    }

    // Update is called once per frame
    void Update()
    {
        pc = GO_TopLeft.transform.position;
        screen.transform.TransformPoint(pc);

        pa = GO_BottomLeft.transform.position; 
        screen.transform.TransformPoint(pa);

        pb = GO_BottomRight.transform.position;
        screen.transform.TransformPoint(pb);
           
        cam.transform.position = positionFromTracker;
        Vector3 trackerPosition = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);

        //Calculate Projection
        GeneralizedPerspectiveProjection(trackerPosition);
        setX = true;
        setY = true;
        setZ = true;
    }

    


 }
