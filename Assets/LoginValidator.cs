using UnityEngine;
using UnityEngine.UIElements;

public class LoginValidator : MonoBehaviour
{
    private TextField emailField;
    private TextField passwordField;
    private Label emailErrorMessage;
    private Label passwordErrorMessage;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        emailField = root.Q<TextField>("InputEmailText");
        passwordField = root.Q<TextField>("inputPasswordText");
        emailErrorMessage = root.Q<Label>("InfoHintEmail");
        passwordErrorMessage = root.Q<Label>("infoHintPassword");

        emailErrorMessage.style.display = DisplayStyle.None;
        passwordErrorMessage.style.display = DisplayStyle.None;

        var loginButton = root.Q<Button>("buttonSubmit");
        loginButton.clicked += OnLoginButtonClicked;
    }

    private void OnLoginButtonClicked()
    {
        emailErrorMessage.style.display = DisplayStyle.None;
        passwordErrorMessage.style.display = DisplayStyle.None;

        bool isValid = true;

        if (string.IsNullOrEmpty(emailField.value))
        {
            ShowErrorMessage(emailErrorMessage, "Please enter email.", Color.red);
            isValid = false;
        }

        if (string.IsNullOrEmpty(passwordField.value))
        {
            ShowErrorMessage(passwordErrorMessage, "Please enter password.", Color.red);
            isValid = false;
        }

        if (isValid)
        {
            ShowErrorMessage(emailErrorMessage, "Login successful", Color.green);
            ShowErrorMessage(passwordErrorMessage, "Login successful", Color.green);
            Debug.Log("Login successful");
        }
    }

    private void ShowErrorMessage(Label label, string message, Color color)
    {
        label.text = message;
        label.style.color = new StyleColor(color);
        label.style.display = DisplayStyle.Flex;
    }
}