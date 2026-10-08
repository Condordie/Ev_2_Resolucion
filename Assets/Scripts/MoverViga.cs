using UnityEngine;

public class MoverViga : MonoBehaviour
{
    // Eje seleccionado:
    // 0 = X
    // 1 = Y
    // 2 = Z
    private int ejeSeleccionado = 0;

    // Modo seleccionado:
    // 0 = Movimiento
    // 1 = Rotación
    // 2 = Tamaño
    private int modoSeleccionado = 0;

    // Cantidad de movimiento por cada pulsación
    public float velocidadMovimiento = 0.1f;

    // Cantidad de rotación por cada pulsación
    public float velocidadRotacion = 10f;

    // Cantidad de cambio de tamaño por cada pulsación
    public float velocidadEscala = 0.1f;

    // Estado de la viga
    private bool vigaColocada = false;

    //Referencia al panel de ajustes
    public GameObject panelAjustes;


    // =========================
    // SELECCIÓN DE MODO
    // =========================

    public void SeleccionarMovimiento()
    {
        modoSeleccionado = 0;
    }

    public void SeleccionarRotacion()
    {
        modoSeleccionado = 1;
    }

    public void SeleccionarTamano()
    {
        modoSeleccionado = 2;
    }


    // =========================
    // SELECCIÓN DE EJE
    // =========================

    public void SeleccionarX()
    {
        ejeSeleccionado = 0;
    }

    public void SeleccionarY()
    {
        ejeSeleccionado = 1;
    }

    public void SeleccionarZ()
    {
        ejeSeleccionado = 2;
    }


    // =========================
    // AUMENTAR
    // =========================

    public void Aumentar()
    {
        Modificar(1);
    }


    // =========================
    // DISMINUIR
    // =========================

    public void Disminuir()
    {
        Modificar(-1);
    }


    // =========================
    // MODIFICAR
    // =========================

    private void Modificar(int direccion)
    {
        if (modoSeleccionado == 0)
        {
            Mover(direccion);
        }
        else if (modoSeleccionado == 1)
        {
            Rotar(direccion);
        }
        else if (modoSeleccionado == 2)
        {
            Escalar(direccion);
        }
    }


    // =========================
    // MOVIMIENTO
    // =========================

    private void Mover(int direccion)
    {
        Vector3 movimiento = Vector3.zero;

        if (ejeSeleccionado == 0)
        {
            movimiento.x = velocidadMovimiento * direccion;
        }
        else if (ejeSeleccionado == 1)
        {
            movimiento.y = velocidadMovimiento * direccion;
        }
        else if (ejeSeleccionado == 2)
        {
            movimiento.z = velocidadMovimiento * direccion;
        }

        transform.position += movimiento;
    }


    // =========================
    // ROTACIÓN
    // =========================

    private void Rotar(int direccion)
    {
        Vector3 rotacion = Vector3.zero;

        if (ejeSeleccionado == 0)
        {
            rotacion.x = velocidadRotacion * direccion;
        }
        else if (ejeSeleccionado == 1)
        {
            rotacion.y = velocidadRotacion * direccion;
        }
        else if (ejeSeleccionado == 2)
        {
            rotacion.z = velocidadRotacion * direccion;
        }

        transform.Rotate(rotacion);
    }


    // =========================
    // TAMAÑO
    // =========================

    private void Escalar(int direccion)
    {
        Vector3 escala = transform.localScale;

        if (ejeSeleccionado == 0)
        {
            escala.x += velocidadEscala * direccion;
        }
        else if (ejeSeleccionado == 1)
        {
            escala.y += velocidadEscala * direccion;
        }
        else if (ejeSeleccionado == 2)
        {
            escala.z += velocidadEscala * direccion;
        }

        // Evitar que la viga llegue a una escala negativa o cero
        escala.x = Mathf.Max(0.1f, escala.x);
        escala.y = Mathf.Max(0.1f, escala.y);
        escala.z = Mathf.Max(0.1f, escala.z);

        transform.localScale = escala;
    }

    public void ColocarViga()
    {
        vigaColocada = true;

        // Guardar la transformación actual en el mundo
        Vector3 posicionMundo = transform.position;
        Quaternion rotacionMundo = transform.rotation;
        Vector3 escalaMundo = transform.lossyScale;

        // Separar la viga del ImageTarget
        transform.SetParent(null);

        // Restaurar su transformación en el mundo
        transform.position = posicionMundo;
        transform.rotation = rotacionMundo;

        // Restaurar aproximadamente su escala
        transform.localScale = escalaMundo;

        if (panelAjustes != null)
        {
            panelAjustes.SetActive(false);
        }

        Debug.Log("Viga colocada en el mundo.");
    }
}
