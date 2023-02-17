using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RobotMenu : MonoBehaviour
{
    Vector3 rot = Vector3.zero;
    float _rotSpeed = 40f;
    Animator anim;
    bool _rotateLeft;
    bool _rotateRight;

    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
    }
    private void Start()
    {
        //MenuRobot();
        StartCoroutine(MenuRobot());
    }


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.eulerAngles = rot;

        if (_rotateLeft) RotateLeft();
        if (_rotateRight) RotateRight();

    }


    IEnumerator MenuRobot()
    {
        anim.SetBool("Open_Anim", true);
        yield return new WaitForSeconds(5f);
        _rotateLeft = true;
        yield return new WaitForSeconds(0.5f);
        StopRotate();
        yield return new WaitForSeconds(2f);
        _rotateRight = true;
        yield return new WaitForSeconds(1f);
        StopRotate();
        yield return new WaitForSeconds(5f);
        anim.SetBool("Roll_Anim", true);
        yield return new WaitForSeconds(3f);
        anim.SetBool("Roll_Anim", false);
        yield return new WaitForSeconds(5f);
        anim.SetBool("Open_Anim", false);
        yield return new WaitForSeconds(5f);
        _rotateLeft = true;
        yield return new WaitForSeconds(0.5f);
        StopRotate();
        yield return new WaitForSeconds(15f);
        Menu();
    }




    void Menu()
    {
        //Invoke(nameof(ScanerReady), 120f);
        StartCoroutine(MenuRobot());

    }




    void RotateLeft()
    {
        rot[1] -= _rotSpeed * Time.fixedDeltaTime;
    }
    void RotateRight() { rot[1] += _rotSpeed * Time.fixedDeltaTime; }

    void StopRotate()
    {
        _rotateLeft = false;
        _rotateRight = false;
    }


    void CheckKey()
    {
        // Walk
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Walk_Anim", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Walk_Anim", false);
        }

        // Rotate Left
        if (Input.GetKey(KeyCode.A))
        {
            rot[1] -= _rotSpeed * Time.fixedDeltaTime;
        }

        // Rotate Right
        if (Input.GetKey(KeyCode.D))
        {
            rot[1] += _rotSpeed * Time.fixedDeltaTime;
        }

        // Roll
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim.GetBool("Roll_Anim"))
            {
                anim.SetBool("Roll_Anim", false);
            }
            else
            {
                anim.SetBool("Roll_Anim", true);
            }
        }

        // Close
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!anim.GetBool("Open_Anim"))
            {
                anim.SetBool("Open_Anim", true);
            }
            else
            {
                anim.SetBool("Open_Anim", false);
            }
        }
    }
}
