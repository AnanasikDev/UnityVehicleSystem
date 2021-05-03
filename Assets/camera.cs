using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Pos;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Pos.position, 0.5f);
    }
}
