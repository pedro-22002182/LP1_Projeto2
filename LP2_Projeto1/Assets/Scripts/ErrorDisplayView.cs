using UnityEngine;
using TMPro;

/// <summary>
/// The <c>ErrorDisplayView</c> class.
/// It is responsible for displaying error messages to the user.
/// </summary>
public class ErrorDisplayView : MonoBehaviour
{
    /// <summary>
    /// Reference to the controller.
    /// </summary>
    [SerializeField]
    private Controller _controller;

    /// <summary>
    /// Reference to the panel where the error messages are displayed.
    /// </summary>
    [SerializeField]
    private GameObject _panel;

    /// <summary>
    /// Reference to the text containing the error information.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _text;


    /// <summary>
    /// Adds <c>DisplayErrorMessage()</c> as a listener to the <c>ErrorFound</c>
    /// event.
    /// </summary>
    private void OnEnable()
    {
        _controller.ErrorFound += DisplayErrorMessage;
    }

    /// <summary>
    /// Removes <c>DisplayErrorMessage()</c> from the the <c>ErrorFound</c>
    /// event's listeners.
    /// </summary>
    private void OnDisable()
    {
        _controller.ErrorFound -= DisplayErrorMessage;
    }

    /// <summary>
    /// Displays an error message that is based on the <c>ErrorCode</c> passed
    /// as an argument.
    /// </summary>
    /// <param name="errorCode">
    /// The <c>ErrorCode</c> that defines the message to display.
    /// </param>
    private void DisplayErrorMessage(ErrorCode errorCode)
    {
        string message = ProccessErrorCode(errorCode);

        _panel.SetActive(true);
        _text.text = message;
    }

    /// <summary>
    /// Customizes the message that is going to be displayed, based on the given
    /// <c>ErrorCode</c>.
    /// </summary>
    /// <param name="errorCode">
    /// The <c>ErrorCode</c> that defines the message to display.
    /// </param>
    /// <returns>The error message to be displayed.</returns>
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
