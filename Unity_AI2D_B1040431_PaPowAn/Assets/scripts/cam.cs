using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public float camspeed = 10;

    public Transform player;
    
    private void LateUpdate()
    {
        Vector3 cam = transform.position;
        Vector3 tar = player.transform.position;
        tar.z = -10;
        tar.y = Mathf.Clamp(tar.y, -1, 1);
        transform.position = Vector3.Lerp(cam, tar, 0.3f * Time.deltaTime * camspeed);
    }
}
