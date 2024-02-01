using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugador : MonoBehaviour
{
    private Vector2 ratonDelta;

    [Header("Vista Cámara")]
    public float sensibilidadRaton;
    public float maxVistaX;
    public float minVistaX;
    public Transform camara;
    private float rotacionActualCamara;

    [Header("Movimiento")]
    public float velocidadMovimiento;
    public float fuerzaSalto;
    public bool estaSuelo = true;
    private Vector2 movimientoActualEntrada;
    private Rigidbody fisica;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        VistaCamara();
    }

    private void Awake()
    {
        fisica = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movimiento();
    }

    public void OnMirarInput(InputAction.CallbackContext context)
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

    public void OnSaltoInput(InputAction.CallbackContext context)
    {
//        saltoEntrada = context.ReadValue<Vector2>();
        Salto();
    }

    private void VistaCamara()
    {
        rotacionActualCamara += ratonDelta.y * sensibilidadRaton;
        rotacionActualCamara = Mathf.Clamp(rotacionActualCamara,
        minVistaX, maxVistaX);
        camara.transform.localRotation = Quaternion.Euler(-rotacionActualCamara, 0, 0);
        transform.eulerAngles += Vector3.up * ratonDelta.x * sensibilidadRaton;
    }

    private void Movimiento()
    {
        Vector3 direccion = transform.forward * movimientoActualEntrada.y + transform.right * movimientoActualEntrada.x;
        direccion *= velocidadMovimiento;
        direccion.y = fisica.velocity.y;
        fisica.velocity = direccion;
    }

    private void Salto()
    {
        if (estaSuelo)
        {
            fisica.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Rampa"))
        {
            estaSuelo = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Rampa"))
        {
            estaSuelo = false;
        }
    }
}
