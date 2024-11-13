using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnim : MonoBehaviour
{
    private const string Grip = "Grip";
    private const string Trigger = "Trigger";
    [SerializeField] private Animator _animator;

    [SerializeField] private InputActionProperty _gripAction;
    [SerializeField] private InputActionProperty _activeAction;


    private void Update()
    {
        var gripValue = _gripAction.action.ReadValue<float>();
        var actionValue = _activeAction.action.ReadValue<float>();

        _animator.SetFloat(Grip, gripValue);
        _animator.SetFloat(Trigger, actionValue);
    }
}
