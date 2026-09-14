using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Asegúrate de que tenga "public void Collect()"
    public void Collect()
    {
        // Tu lógica de recolección (por ejemplo, sumar puntos o curar)
        Destroy(gameObject); // Destruye el objeto recolectable al tocarlo
    }
}