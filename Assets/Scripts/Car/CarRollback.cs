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

    private void Start() {
        SavePosition();
    }


    public void SavePosition() {
        Debug.Log("save " +transform.localPosition);
        if (_savedPosition.Contains(transform.localPosition)) {

            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        
        _savedPosition.Push(transform.localPosition);
        _savedRotation.Push(transform.localEulerAngles);
    }
    
     private Coroutine _coroutine;
    private void OnCollisionEnter(Collision other) {
        other.gameObject.TryGetComponent(out CarMove carMove);
        if (!carMove) return;
        _carMove.canmovetrue();
        _carMove.UnfreezeRotation();
        GetComponent<CarController>().SetCurrentCarState(CarState.ACCIDENT);                         //To Do
         if(_coroutine == null) _coroutine = StartCoroutine(StartRollback());
    }


    private IEnumerator StartRollback() {
        yield return new WaitForSeconds(0.05f); 
        _canSaved = false;
        _carMove.StopMove();
        GetComponent<CarController>().SetCurrentCarState(CarState.ROLLBACK);                         //To Do
        BackToStartPosition();
    }

    public void BackToStartPosition() {
        if (_savedPosition.Count == 0) return;
        transform.DORotate(_savedRotation.Pop(), 0.1f);
        transform.DOMove(_savedPosition.Pop(), 0.1f).OnComplete(() => {
            if (_savedPosition.Count > 0 && _savedRotation.Count > 0) {
                BackToStartPosition();
            }
            else {
                _canSaved = true;
                _carMove.FreezeRotation();
                _carMove.canmoveFalse();
                SavePosition();
                GetComponent<CarController>().SetCurrentCarState(CarState.IDLE);
                if (_coroutine != null) {
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                }

                //StartCoroutine(SetStateIdle());
                GetComponent<CarController>().SetCurrentTurn(GetComponent<CarController>().StartTurn);
            }
        });
    }

    private IEnumerator SetStateIdle() {
        yield return new WaitForSeconds(0.2f);
        
    }
}