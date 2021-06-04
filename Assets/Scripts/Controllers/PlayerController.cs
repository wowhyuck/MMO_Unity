﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill,
    }
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;
    Vector3 _destPos;

    [SerializeField]
    PlayerState _state = PlayerState.Idle;

    GameObject _lockTarket;

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case PlayerState.Die:
                    break;
                case PlayerState.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case PlayerState.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
            }
        }
    }

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
    }

    void Update()
    {
        switch (State)
        {
            case PlayerState.Die:
                UpdateDie();
                break;

            case PlayerState.Moving:
                UpdateMoving();
                break;

            case PlayerState.Idle:
                UpdateIdle();
                break;

            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
    }

    void UpdateDie()
    {
        // 아무것도 못함

    }

    void UpdateMoving()
    {
        // 몬스터가 내 사정거리보다 가까우면 공격
        if (_lockTarket != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 2)
            {
                State = PlayerState.Skill;
                return;
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);

            //nma.CalculatePath
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if (Input.GetMouseButton(0) == false)
                    State = PlayerState.Idle;
                return;
            }

            //transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    void UpdateIdle()
    {

    }

    void UpdateSkill()
    {
        if (_lockTarket != null)
        {
            Vector3 dir = _lockTarket.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        if(_stopSkill)
        {
            State = PlayerState.Idle;

        }
        else
        {
            State = PlayerState.Skill;

        }
    }

    bool _stopSkill = false;
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = PlayerState.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarket = hit.collider.gameObject;
                        else
                            _lockTarket = null;
                    }
                }
                break;

            case Define.MouseEvent.Press:
                {
                    if (_lockTarket == null && raycastHit)
                        _destPos = hit.point;
                }
                break;

            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;
        }
    }
}
