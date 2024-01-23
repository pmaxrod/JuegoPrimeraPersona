using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugador : MonoBehaviour
{
    private Vector2 ratonDelta;

    [Header("Movimiento")]
    public float velocidadMovimiento;
    private Vector2 movimientoActualEntrada;

    [Header("Vista Camara")]
    public float sensibilidadRaton;
    public float maxVistaX;
    public float minVistaX;
    public Transform camara;
    private float rotacionActualCamara;

    private Rigidbody fisica;

    private void Awake()
    {
        fisica = gameObject.GetComponent<Rigidbody>();
    }

    // Método para recoger el movimiento del ratón
    public void OnVistaInput(InputAction.CallbackContext context)
    {
        ratonDelta = context.ReadValue<Vector2>();
    }

    public void OnMovimientoInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            movimientoActualEntrada = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            movimientoActualEntrada = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
       Movimiento();
    }
    
    private void Movimiento()
    {
        Vector3 direccion = transform.forward * movimientoActualEntrada.y + transform.right * movimientoActualEntrada.x;
        direccion *= velocidadMovimiento;
        
        // Para que mantenga la dirección "y" en caso de que no la haya movido.
        direccion.y = fisica.velocity.y;
        
        fisica.velocity = direccion;
    }


    private void LateUpdate()
    {
        VistaCamara();
    }

    private void VistaCamara()
    {
        // Calculamos la rotación de la cámara
        rotacionActualCamara += ratonDelta.y * sensibilidadRaton;
        rotacionActualCamara = Mathf.Clamp(rotacionActualCamara, minVistaX, maxVistaX);

        camara.transform.localRotation = Quaternion.Euler(-rotacionActualCamara, 0, 0);

        transform.eulerAngles += Vector3.up * ratonDelta.x * sensibilidadRaton;

    }
}
