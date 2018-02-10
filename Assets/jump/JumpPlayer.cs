using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlayer : MonoBehaviour
{
    public float ChargeFullTime = 2f;
    public float JumpFullTime = 2f;
    public string ChargeAnimationName = "charge";
    public string JumpAnimationName = "jump";

    Animation _animation;
    AnimationState _chargeAni;
    AnimationState _jumpAni;
    float chargeLength = 1;
    float jumpLength = 2;

    bool canCharge = true;

	void Start ()
    {
        _animation = GetComponent<Animation>();
        _chargeAni = _animation[ChargeAnimationName];
        _jumpAni = _animation[JumpAnimationName];

        chargeLength = _chargeAni.length;
        jumpLength = _jumpAni.length;

        _chargeAni.speed = 0;
        _chargeAni.time = 0;

        switchAnimtion(AnimationType.Charge);
    }

    float chargeTime = 0;
    void Update ()
    {
        if (!canCharge)
            return;
        if (Input.GetMouseButton(0))
        {
            if (chargeTime < ChargeFullTime)
            {
                chargeTime += Time.deltaTime;
                _chargeAni.time = (chargeTime / ChargeFullTime) * chargeLength;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            chargeTime = 0;
            _chargeAni.time = (chargeTime / ChargeFullTime) * chargeLength;
            jump();
        }
	}

    void switchAnimtion(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Charge:
                _animation.Play(ChargeAnimationName);
                break;
            case AnimationType.Jump:
                _jumpAni.speed = jumpLength / JumpFullTime;
                _animation.Play(JumpAnimationName);
                break;
            default:
                break;
        }
    }

    void jump()
    {
        canCharge = false;
        StartCoroutine(reset());
    }

    IEnumerator reset()
    {
        yield return 0;
        switchAnimtion(AnimationType.Jump);
        Invoke("endJump", jumpLength);
        while (transform.localScale.y < 1)
        {
            transform.localScale = new Vector3(1, transform.localScale.y + 0.05f, 1);
            yield return 0.1f;
        }
    }

    void endJump()
    {
        canCharge = true;
        switchAnimtion(AnimationType.Charge);
    }

    public enum AnimationType
    {
        Charge,
        Jump
    }
}
