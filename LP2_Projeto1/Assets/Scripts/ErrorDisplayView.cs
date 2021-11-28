using UnityEngine;
using TMPro;

public class ErrorDisplayView : MonoBehaviour
{
    [SerializeField]
    private Controller _controller;

    [SerializeField]
    private GameObject _panel;

    [SerializeField]
    private TextMeshProUGUI _text;


    private void OnEnable()
    {
        _controller.ErrorFound += DisplayErrorMessage;
    }

    private void OnDisable()
    {
        _controller.ErrorFound -= DisplayErrorMessage;
    }

    private void DisplayErrorMessage(ErrorCode errorCode)
    {
        string message = ProccessErrorCode(errorCode);

        _panel.SetActive(true);
        _text.text = message;
    }

    private string ProccessErrorCode(ErrorCode errorCode)
    {
        string message = default;

        switch (errorCode)
        {
            case ErrorCode.InvalidMapSize:
                message = "The number of rows and columns must be bigger " +
                    "than 0!";
                break;
            
            case ErrorCode.NoMapSize:
                message = "The first line of the file must contain the " +
                    "map size!";
                break;
            
            case ErrorCode.UnknownTerrain:
                message = $"Error found at line {_controller.UnknownWordLine + 1}:\n" +
                    $"{_controller.UnknownWord} is not a valid  terrain!";
                break;
            
            case ErrorCode.UnknownResource:
                message = $"Error found at line {_controller.UnknownWordLine + 1}:\n" +
                    $"{_controller.UnknownWord} is not a valid  resource!";
                break;
        }

        return message;
    }
}
