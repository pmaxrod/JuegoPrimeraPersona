using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad;
    public float fuerzaSalto;

    [Header("Cámara")]
    public float sensibilidadRaton;
    public float maxVistaX;
    public float minVistaX;
    private float rotacionX;

    private Camera camara;
    private Rigidbody fisica;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        VistaCamara();

        if (Input.GetButtonDown("Jump"))
        {
            Salto();
        }
    }

    private void Awake()
    {
        camara = Camera.main;
        fisica = gameObject.GetComponent<Rigidbody>();
    }

    private void Movimiento()
    {
        float x = Input.GetAxis("Horizontal") * velocidad * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * velocidad * Time.deltaTime;
        Vector3 direccion = transform.right * x + transform.forward * z;

        fisica.velocity = new Vector3(direccion.x, fisica.velocity.y, direccion.z);
    }

    private void VistaCamara()
    {
        float y = Input.GetAxis("Mouse X") * sensibilidadRaton;
        rotacionX += Input.GetAxis("Mouse Y") * sensibilidadRaton;

        // Nos quedamos con un valor entre la mínima vista y la máxima (ángulo visión del jugador)
        rotacionX = Mathf.Clamp(rotacionX, minVistaX, maxVistaX);

        camara.transform.localRotation = Quaternion.Euler(-rotacionX, y, 0);
        transform.eulerAngles += Vector3.up * y;
    }
    private void Salto()
    {
        Ray rayo = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(rayo, 1.0f))
        {
            fisica.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
