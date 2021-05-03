using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rot : MonoBehaviour
{
    public float speed;
    public float y;
    public float Limit;
    public Transform car;
    float yRotation;

    bool stopped = false;
    private void Start()
    {
        //MinLimit = 360 + car.GetComponent<Drive>().RotationLimits.x;
        Limit = car.GetComponent<Drive>().RotationLimit;
    }
    float Clamp(float value, float l)
    {
        if (180 - value < 0) value = 360 - l;
        else value = l;
        print("v = " + value); 
        return value;
    }
    void RotateObject(float y)
    {
        transform.Rotate(Vector3.up * y);
        if (!get(transform.localEulerAngles.y))
        {
            float e = transform.localEulerAngles.y;
            print("e = " + e);
            float angle = Clamp(e, Limit);
            Quaternion target = Quaternion.Euler(0, angle, 0);
            transform.localRotation = target;
            print(angle);
        }
    }
    bool get(float a)
    {
        float limit = Limit;
        return a > 360 - limit || a < limit; 
    }
    void Update()
    {
        y = Input.GetAxis("Horizontal") * speed;
        RotateObject(y);
    }
    public void Stop()
    {
        stopped = true;
    }
    public void SetDefaultRotation()
    {
        /*float y = transform.localRotation.y;
        for (int i = 0; i < 100; i++)
        {
            if (stopped) yield break;
            transform.localRotation = new Quaternion(0, 0, -y/100*i, transform.localRotation.w);
            yield return new WaitForSeconds(0.001f);
        }*/
        transform.localRotation = Quaternion.identity;
    }
}
