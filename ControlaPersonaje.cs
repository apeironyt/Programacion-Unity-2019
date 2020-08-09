using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ControlaPersonaje : MonoBehaviour
{

    [SerializeField] float multiplicador;
    [SerializeField] float multiplicadorHorizontal;
    [SerializeField] bool ToyTocandoSuelo = false;
    [SerializeField] LayerMask layersSuelo;
    [SerializeField] float VisorAxisHorizontal = 0;
    [SerializeField] float DistanciaRayo;
 
    
    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        CaminarAndSaltar();
    }

    void CaminarAndSaltar()
    {
        float AxisHorizontal = Input.GetAxisRaw("Horizontal");
        bool vertical = Input.GetKey(KeyCode.Space);
        var component = this.GetComponent<Rigidbody2D>();
        RaycastHit2D rh = Physics2D.Raycast(gameObject.transform.position,Vector2.down,DistanciaRayo,layersSuelo);
        VisorAxisHorizontal = AxisHorizontal;

        if (rh.transform != null)
        {
            Transform objetocolicionado = rh.transform;
            if (objetocolicionado.CompareTag("suelo"))
            {
                ToyTocandoSuelo = true;
            }
            else if (objetocolicionado.CompareTag("obstaculos"))
            {
                ToyTocandoSuelo = true;
            }
        }

        if ((component.velocity.x == 0 && component.velocity.y == 0) && !ToyTocandoSuelo)
        {
            if (AxisHorizontal != 0)
            {
                ToyTocandoSuelo = true;
            }

        }

        if (!ToyTocandoSuelo)
        {
            AxisHorizontal = 0;

        }

        //en esta sentencia preguntamos si el axis positivo ha sido presionado y si es cierto el personaje se movera hacia la derecha
        if (AxisHorizontal > 0)
        {
            component.velocity = new Vector2(1f*multiplicadorHorizontal,component.velocity.y);
        }
       
        //en esta sentencia preguntamos si el axis negativo ha sido presionado y si es cierto el personaje se movera hacia la izquierda
        if (AxisHorizontal < 0)
        {
            component.velocity = new Vector2(-1f * multiplicadorHorizontal, component.velocity.y);
        }


        //aqui lo que hacemos es comprobar si el boton de saltar fue presionado y el personaje este tocando el suelo para poder saltar
        if (vertical && ToyTocandoSuelo)
        {     
                component.AddForce(Vector2.up * multiplicador);
                ToyTocandoSuelo = false;           
        }


        //en esta parte comprabadomos que el personaje este tocando el suelo y ademas de eso que si el jugador a dejado de presionar el axis horizontal el personaje 
        //envie su velocidad a 0 y se detenga instantaneamente al ser soltado el axis horizontal
        if (ToyTocandoSuelo)
        {
            if (AxisHorizontal == 0)
            {
                component.velocity = new Vector2(0, component.velocity.y);
            }
        }

    }

}
