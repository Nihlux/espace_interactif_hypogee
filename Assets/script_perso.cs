using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using OscJack;
public class script_perso : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;
    Vector2 movement;
    private int left_right;
    private int top_down;
    private int pot;
    private float movement_x;
    private float movement_y;
    OscServer _server;
    public float moveSpeed = 5f;
    public GameObject lightGO;
    private Light2D light;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        light = lightGO.GetComponent<Light2D>();

        _server = new OscServer(7001); // Port number

        _server.MessageDispatcher.AddCallback(
            "/upDownVal", // OSC address
            (string address, OscDataHandle data) => {
                top_down = data.GetElementAsInt(0);
            }
        );

        _server.MessageDispatcher.AddCallback(
            "/leftRightVal", // OSC address
            (string address, OscDataHandle data) => {
                left_right = data.GetElementAsInt(0);
            }
        );

        _server.MessageDispatcher.AddCallback(
            "/pot", // OSC address
            (string address, OscDataHandle data) => {
                pot = data.GetElementAsInt(0);
            }
        );

        //_server.MessageDispatcher.AddCallback(
        //  "/left", // OSC address
        //  (string address, OscDataHandle data) => {
        //      int left = data.GetElementAsInt(0);
        //  }
        //);

        Debug.Log(light.pointLightOuterRadius);
    }

    void Update()
    {
        // Gives a value between -1 and 1
        //movement.x = Input.GetAxisRaw("Horizontal"); // -1 is left
        //movement.y = Input.GetAxisRaw("Vertical"); // -1 is down


            if (left_right < 10 && left_right > -15)
            {
                movement_x = 0;
            }
            else if (left_right > 10)
            {
                movement_x = 1;
            }
            else if (left_right < -15)
            {
                movement_x = -1;
            }

            if (top_down < 10 && top_down > -15)
            {
                movement_y = 0;
            }
            else if(top_down > 10)
            {
                movement_y = 1;
            }
            else if (top_down < -15)
            {
                movement_y = -1;
            }

        movement = new Vector2(movement_x * moveSpeed, movement_y * moveSpeed);

        animator.SetFloat("Horizontal", -movement_x);
        animator.SetFloat("Vertical", movement_y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


        float potvalue = (((pot - 500) * 13) / (-500)) + 2;
        Debug.Log(potvalue);

        light.pointLightOuterRadius = potvalue;

        //if (pot > 100) {
        //    light.enabled = false;
        //}
        //else
        //{
        //    light.enabled = true;
        //}
    }

    void FixedUpdate()
    {
        //body.AddForce(new Vector2(movement_x * moveSpeed, movement_y * moveSpeed));
        //body.AddForce(new Vector2(left_right, top_down));

        body.MovePosition(body.position + movement * Time.fixedDeltaTime);
    }

    void OnDestroy()
    {
        _server?.Dispose();
        _server = null;
    }


}