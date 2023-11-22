using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarRollback : MonoBehaviour {
    private CarController _carController;
    private CarMove _carMove;
    
    private Stack<Vector3> _savedPosition = new Stack<Vector3>();
    private Stack<Vector3> _savedRotation = new Stack<Vector3>();

    [SerializeField] private float _delayToRollBack = 0.05f;

    
    public void Init(CarController carController) {
        _carController = carController;
        _carMove = _carController.CarMove;
    }
    
    private void Start() {
        SavePosition();
    }
    
    public void SavePosition() {
        _savedPosition.Push(transform.localPosition);
        _savedRotation.Push(transform.localEulerAngles);
    }
    
     private Coroutine _coroutine;
    private void OnCollisionEnter(Collision other) {
        other.gameObject.TryGetComponent(out CarMove carMove);
        if (!carMove) return;
        
        _carMove.SetCanMove(true);
        _carMove.UnfreezeRotation();
        _carController.SetCurrentCarState(CarState.ACCIDENT);
        
         if(_coroutine == null) _coroutine = StartCoroutine(StartRollback());
    }


    private IEnumerator StartRollback() {
        yield return new WaitForSeconds(_delayToRollBack);
        
        _carMove.StopMove();
        _carController.SetCurrentCarState(CarState.ROLLBACK);                         
        BackToStartPosition();
    }

    public void BackToStartPosition() {
        if (_savedPosition.Count == 0) return;
        
        transform.DORotate(_savedRotation.Pop(), 0.1f);
        transform.DOMove(_savedPosition.Pop(), 0.1f).OnComplete(() => {
            if (_savedPosition.Count > 0) {
                BackToStartPosition();
            }
            else {
                _carMove.FreezeRotation();
                _carMove.SetCanMove(false);
                SavePosition();
                _carController.SetCurrentCarState(CarState.IDLE);
                
                if (_coroutine != null) {
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                }

                _carController.SetCurrentTurn(_carController.StartTurn);
            }
        });
    }
}