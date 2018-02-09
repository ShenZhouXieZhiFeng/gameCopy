using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    public float ChargeFullTime = 2f;

    Animation _animation;
    AnimationState _aniState;
    float chargeLength = 0;

	void Start ()
    {
        _animation = GetComponent<Animation>();
        _aniState = _animation["charge"];
        chargeLength = _aniState.length;

        _aniState.speed = 0;
        _aniState.time = 0;

        _animation.Play();
    }

    float chargeTime = 0;
    void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            if (chargeTime < ChargeFullTime)
            {
                chargeTime += Time.deltaTime;
                _aniState.time = (chargeTime / ChargeFullTime) * chargeLength;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            chargeTime = 0;
            _aniState.time = (chargeTime / ChargeFullTime) * chargeLength;
        }
	}
}
