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

public class ProjectorBehaviour : MonoBehaviour
{
    public GameObject character;

    public float offset = .45f;

    void LateUpdate()
    {
        if (character != null)
        {
            this.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + offset, character.transform.position.z);
        }
        else
        {
            Destroy(this.gameObject.GetComponent<Projector>());
            Destroy(this.gameObject);
        }
    }
}
