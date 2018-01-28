using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManger : MonoBehaviour {

    public Image[] imageArray;

    public Image Fill;
	// Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
	

    public void HideImages(float alpha)
    {
        foreach(Image i in imageArray)
        {
            i.color= new Color(i.color.r, i.color.g, i.color.b, alpha);
        }
    }
}
