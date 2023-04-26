using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject MenuPausa;
    bool PauseMode;

    //[SerializeField] private Camera Camara;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Pausa();
        }

    }

    public void Pausa()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        MenuPausa.SetActive(true);
        //Camara.
    }

    public void Renaudar()
    {
        Time.timeScale = 1f;
        MenuPausa.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        //Camara.enabled = true;

    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
