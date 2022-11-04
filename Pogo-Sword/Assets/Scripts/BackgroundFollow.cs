using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class BackGroundFollow : MonoBehaviour
{
    //Background image to be added
    List<SpriteRenderer> CurrentSpriteRenderers = new List<SpriteRenderer>();

    //Controls moving objects
    List<Transform> Parents = new List<Transform>();

    //Width of picture
     float spriteOffsetX = 35.74f;  //The offset between images, that is, the width of a background image


    //Main camera
    Camera cam;

    //The screen width corresponds to the length in the 2D world
    float CameraWightTranslate2Dlength;

    //Camera position in the previous frame
    Vector3 cameraLastPos;

    private void Awake()
    {
        cam = Camera.main;

        //Calculate Camera Width
        CameraWightTranslate2Dlength = GetCameraWightTranslate2Dlength();

        CreateBackgroundAndParents();

        //Get the width of the picture
        BoxCollider2D tmp = CurrentSpriteRenderers[0].gameObject.AddComponent< BoxCollider2D > ();
        spriteOffsetX = tmp.size.x;
        Destroy (tmp);
    }

    private void Start()
    {
        cameraLastPos = cam.transform.position;
    }

    private void Update()
    {
        Follow();

        //Record camera position
        cameraLastPos = cam.transform.position;
    }

    private void Follow()
    {

        bool isRight = false;

        //Decide whether the camera moves left or right
        if (cameraLastPos.x < cam.transform.position.x)
        {
            isRight = true;
        }
        else if (cameraLastPos.x > cam.transform.position.x)
        {
            isRight = false;
        }
        else
        {
            //No camera movement
            return;
        }


        for (int i = 0; i < CurrentSpriteRenderers.Count; i++)
        {

            Transform trans = CurrentSpriteRenderers[i].transform;

            //Left border of the current picture
            float spriteLeft = -spriteOffsetX / 2f + trans.position.x;
            float cameraLeft = cam.transform.position.x - CameraWightTranslate2Dlength / 2f;
            //Right border of the current picture
            float spriteRight = spriteOffsetX / 2f + CurrentSpriteRenderers[i].transform.position.x;
            float cameraRight = cam.transform.position.x + CameraWightTranslate2Dlength / 2f;


            //Determine the position where the new background image is generated
            if (isRight)
            {
                if (spriteRight - cameraRight < 3f)
                {
                    //Move another picture
                    for (int z = 0; z < trans.parent.childCount; z++)
                    {
                        if (trans.parent.GetChild(z) != trans)
                        {
                            trans.parent.GetChild(z).position = trans.position + new Vector3(spriteOffsetX, 0, 0);
                        }
                    }
                }
                if (spriteRight - cameraRight < -3f)
                {
                    //Replace List Elements
                    for (int z = 0; z < trans.parent.childCount; z++)
                    {
                        if (trans.parent.GetChild(z) != trans)
                        {
                            CurrentSpriteRenderers[i] = trans.parent.GetChild(z).GetComponent<SpriteRenderer>();
                        }
                    }
                }

            }
            else
            {

                if (Mathf.Abs(spriteLeft - cameraLeft) < 3f)
                {
                    //Move another picture
                    for (int z = 0; z < trans.parent.childCount; z++)
                    {
                        if (trans.parent.GetChild(z) != trans)
                        {
                            trans.parent.GetChild(z).position = trans.position - new Vector3(spriteOffsetX, 0, 0);
                        }
                    }
                }
                if (cameraLeft - spriteLeft < -3f)
                {
                    //Replace List Elements
                    for (int z = 0; z < trans.parent.childCount; z++)
                    {
                        if (trans.parent.GetChild(z) != trans)
                        {
                            CurrentSpriteRenderers[i] = trans.parent.GetChild(z).GetComponent<SpriteRenderer>();
                        }
                    }
                }
            }

            //Camera movement
            Vector3 cameraMove = cam.transform.position - cameraLastPos;

            //Moving background layers
            //The farther the layer moves, the slower it moves, and vice versa
            Parents[i].transform.position += cameraMove * 1f / (float)(Parents.Count + 1 - i);

        }


    }

    public void CreateBackgroundAndParents()
    {
        //Get all Sprites
        CurrentSpriteRenderers = transform.GetComponentsInChildren<SpriteRenderer>().ToList();
        //Sort by Layer    
        //From big to small
        CurrentSpriteRenderers.Sort((x, y) => -x.sortingOrder.CompareTo(y.sortingOrder));

        //Move the background at the same layer to the corresponding parent object to facilitate unified movement of the background at the same layer
        for (int i = 0; i < CurrentSpriteRenderers.Count; i++)
        {
            //New parent object
            GameObject groundParent = new GameObject("layer_" + CurrentSpriteRenderers[i].sortingOrder);
            groundParent.transform.parent = transform;

            //Same layer, added to a parent object
            CurrentSpriteRenderers[i].transform.parent = groundParent.transform;

            SpriteRenderer spr = Instantiate(CurrentSpriteRenderers[i], groundParent.transform);
            spr.transform.position = CurrentSpriteRenderers[i].transform.position;
            spr.name = CurrentSpriteRenderers[i].name;

            Parents.Add(groundParent.transform);
        }
    }

    private float GetCameraWightTranslate2Dlength()
    {
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        Vector3 cornerPos0 = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));

        //Calculate Camera Width
        return cornerPos.x - cornerPos0.x;
    }


}



