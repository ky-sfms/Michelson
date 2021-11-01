﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class  KnobController: MonoBehaviour
{
    public float speed = 5;
    private float angle = 0f;
    public Text txt;
    public DateTime LastTime;
    public bool IsTextActive = false;
    private String ObjectName;


    //click部分
    //begin
    public bool IsClick = false;
    public bool IsDown = false;
    public Color color;
    public Color ActiveColor = Color.red;

    public void OnMouseDown()
    {
        if (MessageBox.state == -1)
            this.IsDown = true;
    }

    public void OnMouseUp()
    {

        if (IsDown)
        {
            if (GetComponent<MeshRenderer>().material.color == color)
            {
                GetComponent<MeshRenderer>().material.color = ActiveColor;
                IsClick = true;
                SetText(ObjectName + "角度为：" + angle.ToString());
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = color;
                IsClick = false;
            }

            IsDown = false;
        }
    }
    //end

    public void JudgeIsArrival()
    {
        if (txt.text == "" || LastTime == null || !IsTextActive) return;
        TimeSpan ts = DateTime.Now - LastTime;
        if (Convert.ToInt64(ts.TotalSeconds) > 10)
        {
            txt.text = "";
            IsTextActive = false;
        }
        return;
    }

    public void SetText(String text)
    {
        txt.text = text;
        LastTime = DateTime.Now;
        IsTextActive = true;
        Debug.Log(text);
    }

    void Start()
    {
        color = GetComponent<MeshRenderer>().material.color;
        switch (gameObject.name)
        {
            case "luoding_M1_1":
                ObjectName = "上方螺丝";
                break;
            case "luoding_M1_2":
                ObjectName = "右侧螺丝";
                break;
            case "luoding_M1_3":
                ObjectName = "左侧螺丝";
                break;
            default:
                ObjectName = gameObject.name;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsClick)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                angle -= speed * Time.deltaTime;
                this.transform.Rotate(new Vector3(0,90, 0),-speed * Time.deltaTime);
                SetText(ObjectName + "角度为：" + angle.ToString());

            }
            if (Input.GetKey(KeyCode.E))
            {
                angle += speed * Time.deltaTime;
                this.transform.Rotate(new Vector3(0,90 , 0), speed * Time.deltaTime);
                SetText(ObjectName + "角度为：" + angle.ToString());
            }
        }

        JudgeIsArrival();

    }






}

