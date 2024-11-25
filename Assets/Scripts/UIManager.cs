using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick = null;

    public Joystick Joystick { get { return joystick; } }

    private static UIManager _instance = null;
    public static UIManager Instance { get { return _instance; } }

    private void Awake() {
        if(_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}

