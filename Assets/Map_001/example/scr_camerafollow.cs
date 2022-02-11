using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_camerafollow : MonoBehaviour
{
    private Transform[] Children;

    public GameObject tommas;
    // Start is called before the first frame update
    void Start()
    {
        Children = GetComponentsInChildren<Transform>();

        GameObject myplayercontainre =GameObject.Find("Player_Container");


    }

    // Update is called once per frame
    void Update()
    {
        Children = GetComponentsInChildren<Transform>();

        Children[1].transform.position = Children[2].transform.position+ (new Vector3(0,1,1));

        Quaternion Playerrotation = Children[2].transform.rotation;

        Quaternion Camerarotation = Children[1].transform.rotation;

        Children[2].transform.rotation = new Quaternion(Playerrotation.x, Camerarotation.y, Playerrotation.z , Playerrotation.w);
    }
}
