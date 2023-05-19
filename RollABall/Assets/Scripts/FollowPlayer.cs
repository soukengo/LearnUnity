using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetTransform;

    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - targetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = targetTransform.position + _offset;
    }
}