using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    Rigidbody rb;
    float z;
    public float x;
    public Transform LBwheel;
    public Transform RBwheel;
    public Transform LFwheel;
    public Transform RFwheel;
    public GameObject VehicleModel;
    public float RotationLimit; // Лимиты поворота колес. В градусах. Пример : -50, 50

    public bool ReturnWheelRotation = false; // Возвращать ли колеса в исходное положение, когда клавиши поворота отжаты.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        z = Input.GetAxis("Vertical") * speed;
        x = Input.GetAxis("Horizontal") * rotationSpeed;
        bool realistic = ReturnWheelRotation ? x != 0 : true;
        int axis = z > 0 ? 1 : -1;
        if (LFwheel.localEulerAngles.y < 358 && z != 0 && realistic) // Влево
        {
            print($"Поворачиваем влево.., потому что {LFwheel.rotation.y}, {z}");
            RaycastHit hit;
            Physics.Raycast(LBwheel.transform.position, LBwheel.transform.right,
                out hit, Mathf.Infinity, 1 << 8, QueryTriggerInteraction.Collide);
            Debug.DrawRay(LBwheel.transform.position, LBwheel.transform.right, Color.red);
            //print("Точка найдена! Поворот..");
            if (hit.point != Vector3.zero)
                transform.RotateAround(hit.point, new Vector3(0, axis, 0), (-speed / 9.25f) * (1 / hit.distance)); //rotationSpeed * (1/hit.distance) * 1.6f
        }
        if (RFwheel.localEulerAngles.y > 2 && z != 0 && realistic) // Вправо
        {
            print($"Поворачиваем вправо.., потому что {LFwheel.rotation.y}, {z}");
            RaycastHit hit;
            Physics.Raycast(RBwheel.transform.position, RBwheel.transform.right,
                out hit, Mathf.Infinity, 1 << 8, QueryTriggerInteraction.Collide);
            Debug.DrawRay(RBwheel.transform.position, -RBwheel.transform.right, Color.red);
            //print("Точка найдена! Поворот..");
            if (hit.point != Vector3.zero)
                transform.RotateAround(hit.point, new Vector3(0, -axis, 0), (-speed / 9.25f) * (1 / hit.distance)); //10.75f
        }
        /*if (ReturnWheelRotation && !realistic)
        {
            LFwheel.GetComponent<rot>().SetDefaultRotation();
            RFwheel.GetComponent<rot>().SetDefaultRotation();
        }*/

        /*if (LFwheel.localRotation.y < 0 && x < 0.3 && z > speed / 2)
        {
            Drift(Dir.Left);
        }
        if (LFwheel.localRotation.y > 0 && x > -0.3 && z > speed / 2)
        {
            Drift(Dir.Right);
        }*/
    }
    private void FixedUpdate()
    {
        float c = 1;
        if (z == 0) c = 0.2f;
        rb.AddForce(VehicleModel.transform.forward * z * c);
    }
    enum Dir
    {
        Left,
        Right
    }
    /*void Drift(Dir direction)
    {
        Vector3 rotation = new Vector3(0, direction == Dir.Left ? -1 : 1, 0);
        rb.AddTorque(rotation * 800);
    }*/
}
