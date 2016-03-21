using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ColourController : MonoBehaviour
{
    public Color holdingColour = Color.white;
    [SerializeField]
    PhotoShot shots;

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "Back"))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Photo"))
        {
            shots.TakePhoto();
        }
    }
}
