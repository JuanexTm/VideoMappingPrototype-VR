using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscenasXR : MonoBehaviour
{
    public string nombreEscenaExperiencia = "Experiencia";
    public string nombreEscenaLobby = "Lobby";

    public void CargarExperiencia()
    {
        SceneManager.LoadScene(nombreEscenaExperiencia);
    }

    public void VolverAlLobby()
    {
        SceneManager.LoadScene(nombreEscenaLobby);
    }
}
