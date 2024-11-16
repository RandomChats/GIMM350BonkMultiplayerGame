using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class StartGameText : MonoBehaviour
{
    public GameObject startPanel;
    public TextMeshProUGUI startText;

    private bool gameStarted = false;
    public InputActionReference start;
    
    void OnEnable()
    {
        start.action.Enable();
        start.action.performed += OnStartPressed;
    }

    void OnDisable()
    {
        start.action.Disable();
        start.action.performed -= OnStartPressed;
    }
    void OnStartPressed(InputAction.CallbackContext context)
    {
        if (!gameStarted)
        {
            StartGame();
        }
    }
    
    void StartGame()
    {
        startPanel.SetActive(false);
        startText.gameObject.SetActive(false);
        gameStarted = true;
    }
}
