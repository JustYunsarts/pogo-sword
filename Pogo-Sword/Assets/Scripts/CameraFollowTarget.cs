using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target; //Target to look towards
    public string lookTargetTag = "Player"; //Look to the tag of the target
    
    public Vector2 targetOffset = new Vector3(2, 2); //Offset in X-axis and Y-axis directions

    public float maxLimit; //Right border limit
    
    public float minLimit; //Left border limit
    
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag(lookTargetTag).transform;
            return;
        }

        Follow();
    }

    private void Follow()
    {
        float xOffset = 0;
        float yOffset = 0;

        bool ismoveX = false;
        bool ismoveY = false;

        if (target.position.x < transform.position.x - targetOffset.x)
        {
            xOffset = -targetOffset.x;
            ismoveX = true;
        }
        else if (target.position.x > transform.position.x + targetOffset.x)
        {
            xOffset = -targetOffset.x;
            ismoveX = true;
        }

        if (target.position.y < transform.position.y + targetOffset.y)
        {
            yOffset = targetOffset.y;
            ismoveY = true;
        }
        else if (target.position.y > transform.position.y + targetOffset.y)
        {
            yOffset = -targetOffset.y;
            ismoveY = true;
        }

        if (ismoveX)
        {
            float x = target.position.x + xOffset;
            x = Mathf.Clamp(x, minLimit, maxLimit);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        if (ismoveY)
        {
            transform.position = new Vector3(transform.position.x, target.position.y + yOffset, transform.position.z);
        }
    }
}
