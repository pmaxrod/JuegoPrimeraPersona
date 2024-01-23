using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MouseLook rota usando el delta del mouse.
// Para crear un personaje estilo FPS:
// - Crea una cápsula.
// - Añade el script MouseLook a la cápsula.
//      -> Configurar el eje del script para usar Mouse X. (Solo queremos girar el personaje pero no inclinarlo)
// - Añade el script FPSInput a la cápsula
//      -> Se añadirá automáticamente un componente CharacterController.
//
// - Haz que la cámara sea hija de la cápsula. Colócalo en la cabeza y restablece la rotaciónn.
// - Añade el script MouseLook a la cámara.
//      -> Configurar el eje del script para usar MouseY. (Queremos que la cámara se incline hacia arriba y hacia abajo como una cabeza. El personaje ya gira).



public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float verticalRot = 0;
    private float horizontalRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Make the rigid body not change rotation
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

            float horizontalRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
        else
        {
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
                        
            horizontalRot += Input.GetAxis("Mouse X") * sensitivityHor;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
    }
}
