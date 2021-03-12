using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _safePropsLayer; // checking for other safe solid objects that hero can stand on

    private Collider2D _collider;

    public bool IsTouchingLayer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        IsTouchingLayer = (_collider.IsTouchingLayers(_groundLayer) || _collider.IsTouchingLayers(_safePropsLayer));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsTouchingLayer = (_collider.IsTouchingLayers(_groundLayer) || _collider.IsTouchingLayers(_safePropsLayer));
    }
}
