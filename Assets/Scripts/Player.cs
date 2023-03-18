using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 jumpForce;
    public Vector2 a;
    public float  maxForceX, maxForceY,jumpUp, maxTime,minTime;
    [HideInInspector]
    public int lastPlatformId;
    bool m_isDidJump,m_isPowerSetted,m_checkPower;
    Platform m_curPlatform;
    Rigidbody2D m_rb;
    Animator m_anim;
    int m_pastPlatformId;
    float m_timeSpent;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
    }
    private void Start()
    {
        a = Vector2.zero;
        maxTime = 1.5f;
        UIManager.Ins.BuffPower(m_timeSpent,maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetPower(true);
        }
        else if(m_isPowerSetted)
        {
            SetPower(false);
        }
        a = m_rb.velocity;
    }
    
    void SetPower()
    {
        if (m_isPowerSetted && !m_isDidJump)
        {
            m_timeSpent += Time.deltaTime;
            if (minTime <= m_timeSpent && m_timeSpent <= maxTime)
            {
                if (jumpForce.x >= maxForceX) jumpForce.x = maxForceX;
                else jumpForce.x = jumpUp * m_timeSpent;

                if (jumpForce.y >= maxForceY) jumpForce.y = maxForceY;
                else jumpForce.y = jumpForce.x * 2.74f;

                UIManager.Ins.BuffPower(m_timeSpent, maxTime);
            }
            else if (minTime > m_timeSpent) UIManager.Ins.BuffPower(m_timeSpent, maxTime);

        }
        
    }
    void SetPower(bool isHolding)
    {
        m_isPowerSetted = isHolding;
        if (isHolding) SetPower();
        else
        {
            m_timeSpent = 0;
            UIManager.Ins.BuffPower(m_timeSpent, maxTime);
        }
        if (!isHolding && !m_isDidJump) Jump();
    }
    void Jump()
    {
        if (!m_rb || jumpForce.x <= 0 || jumpForce.y <= 0)
        {
            return;
        }
        AudioController.Ins.PlaySound(AudioController.Ins.jump);
        UIManager.Ins.BuffPower(m_timeSpent, maxTime);
        m_rb.velocity = jumpForce;
        jumpForce = Vector2.zero;
        m_isDidJump = true;
        if (m_anim) m_anim.SetBool("DidJump", m_isDidJump);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(TagConsts.GROUND))
        {
            if (m_rb)
            {
                m_rb.velocity = Vector2.zero;
            }
            if (m_anim)
            {
                m_isDidJump = false;
                m_anim.SetBool("DidJump", m_isDidJump);
            }
            m_curPlatform = col.GetComponentInParent<Platform>();
            int id = m_curPlatform.GetInstanceID();
            if (m_curPlatform && id != lastPlatformId && id != m_pastPlatformId)
            {
                m_pastPlatformId = lastPlatformId;
                lastPlatformId = m_curPlatform.GetInstanceID();
                GameManager.Ins.CreatePlatformAndMoveCam(m_curPlatform.transform.position.x);
                GameManager.Ins.IncreaseScore();
            }
        }
        else if (col.gameObject.CompareTag(TagConsts.DEATHZONE_B))
        {
            AudioController.Ins.PlaySound(AudioController.Ins.gameover);
            Destroy(gameObject);
            GameManager.Ins.Death();
        }

            
        
    }
}