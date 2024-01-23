using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5;
    public float jumpPower = 4; 
    Rigidbody rb;
    CapsuleCollider col;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }
    // Update is called once per frame
    void Update()
    {
        //Get the input value from the controllers
        float Horizontal = Input.GetAxis("Horizontal") * speed;
        float Vertical = Input.GetAxis("Vertical") * speed;
        Horizontal *= Time.deltaTime;
        Vertical *= Time.deltaTime;
        //Translate our character via our inputs.
        transform.Translate(Horizontal, 0,Vertical);
        if (isGrounded() && Input.GetButtonDown("Jump"))
        {
            //Añade fuerza hacia arriba al cuerpo rígido cuando presionamos saltar.
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }
    private bool isGrounded()
    {
        //Test that we are grounded by drawingan invisible line (raycast)
        //If this hits a solid object e.g. floor then we are grounded.
        //Prueba que estamos pegados a tierra dibujando una línea invisible (raycast)
        //Si el rayo toca un objeto sólido, p.e. suelo, entonces groundes es verdadero.
        return Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }
}