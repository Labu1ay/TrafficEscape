using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CarMove))]
public class CarRollback : MonoBehaviour {
    [SerializeField] private CarMove _carMove;
    
    private Stack<Vector3> _savedPosition = new Stack<Vector3>();
    private Stack<Vector3> _savedRotation = new Stack<Vector3>();
    private bool _canSaved = true;

    private void OnValidate() {
        _carMove ??= GetComponent<CarMove>();
    }

    [SerializeField] private bool _showDebug;

    // private void Start() {
    //     _savedPosition.Push(transform.localPosition);
    //     _savedRotation.Push(transform.localEulerAngles);
    // }

    private void Update() {
        if(_showDebug) Debug.Log(_savedPosition.Count + " " + _savedRotation.Count);
        //if (!_savedPosition.Contains(transform.localPosition) && _canSaved) {
        if( GetComponent<CarController>().CurrentCarState == CarState.MOVING || 
            GetComponent<CarController>().CurrentCarState == CarState.ACCIDENT ||
            GetComponent<CarController>().CurrentCarState == CarState.IDLE) {
            if (_savedPosition.Contains(transform.localPosition)) return;
            _savedPosition.Push(transform.localPosition);
            _savedRotation.Push(transform.localEulerAngles);
        }
        //}
    }

    private void OnCollisionEnter(Collision other) {
        other.gameObject.TryGetComponent(out CarMove carMove);
        if (!carMove) return;
        _carMove.canmovetrue();
        _carMove.UnfreezeRotation();
        GetComponent<CarController>().SetCurrentCarState(CarState.ACCIDENT);                         //To Do
        StartCoroutine(StartRollback());
    }

    private IEnumerator StartRollback() {
        yield return new WaitForSeconds(0.2f);
        _canSaved = false;
        _carMove.StopMove();
        GetComponent<CarController>().SetCurrentCarState(CarState.ROLLBACK);                         //To Do
        BackToStartPosition();
    }

    public void BackToStartPosition() {
        if (_savedPosition.Count == 0) return;
        transform.DORotate(_savedRotation.Pop(), 0.001f);
        transform.DOMove(_savedPosition.Pop(), 0.001f).OnComplete(() => {
            if (_savedPosition.Count > 0) {
                BackToStartPosition();
            }
            else {
                _canSaved = true;
                _carMove.FreezeRotation();
                _carMove.canmoveFalse();
                StartCoroutine(SetStateIdle());
                GetComponent<CarController>().SetCurrentTurn(GetComponent<CarController>().StartTurn);
            }
        });
    }

    private IEnumerator SetStateIdle() {
        yield return new WaitForSeconds(0.2f);
        GetComponent<CarController>().SetCurrentCarState(CarState.IDLE); 
    }
}