/***********************************************************
* Copyright (C) 2018 6degrees.xyz Inc.
*
* This file is part of the 6D.ai Beta SDK and is not licensed
* for commercial use.
*
* The 6D.ai Beta SDK can not be copied and/or distributed without
* the express permission of 6degrees.xyz Inc.
*
* Contact developers@6d.ai for licensing requests.
***********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlaneBehavior : MonoBehaviour
{
    public GameObject plane;

    void OnEnable()
    {
        //plane.transform.parent = gameObject.transform;
        //Debug.Log("reference " + plane);
        //Debug.Log("instance " + _plane);
    }

    void onDisable()
    {
        Destroy(plane);
    }

    void LateUpdate()
    {
        if(plane != null) {
            plane.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.01f, gameObject.transform.position.z);
        }
    }
}
