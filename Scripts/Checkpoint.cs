using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;

    // Esta es la función que Vidadeljugador.cs está buscando
    public void Activate()
    {
        isActivated = true;
        Debug.Log("¡Checkpoint activado!");
    }
}