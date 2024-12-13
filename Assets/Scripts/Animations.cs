using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void FixedUpdate() {
        if (Input.GetKeyUp("z") || Input.GetKeyUp("s") || Input.GetKeyUp("q") || Input.GetKeyUp("d")) {
            anim.Play("Trump");
        }
	}

}

