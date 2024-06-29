using UnityEngine;
using UnityEngine.UIElements;
using System.Net.Mail;

public class LoginValidator : MonoBehaviour
{
    private TextField emailField;
    private TextField passwordField;
    private Label emailErrorMessage;
    private Label passwordErrorMessage;
    private const string placeholderText = "Enter text...";  // Set placeholder text
    private VisualElement loginFrame; // Use this to hide the login frame after login is successful

    private PostLoginManager postLoginManager;
    private VisualElement root; // Class-level variable for the root element

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        emailField = root.Q<TextField>("emailInput");
        passwordField = root.Q<TextField>("passwordInput");

        // Set initial placeholder text
        emailField.value = placeholderText;
        passwordField.value = placeholderText;
        passwordField.isPasswordField = false; // Initially not a password field

        emailErrorMessage = root.Q<Label>("InfoHintEmail");
        passwordErrorMessage = root.Q<Label>("infoHintPassword");

        emailErrorMessage.style.display = DisplayStyle.None;
        passwordErrorMessage.style.display = DisplayStyle.None;

        var loginButton = root.Q<Button>("buttonSubmit");
        loginButton.clicked += OnLoginButtonClicked;

        // Add event listeners for email and password field focus and change events
        emailField.RegisterCallback<FocusInEvent>(OnEmailFieldFocus);
        emailField.RegisterCallback<BlurEvent>(OnEmailFieldBlur);
        passwordField.RegisterCallback<FocusInEvent>(OnPasswordFieldFocus);
        passwordField.RegisterCallback<BlurEvent>(OnPasswordFieldBlur);

        loginFrame = root.Q<VisualElement>("frameLogin");

        // Initialize the PostLoginManager
        postLoginManager = gameObject.AddComponent<PostLoginManager>();
    }

    private void OnEmailFieldFocus(FocusInEvent evt)
    {
        if (emailField.value == placeholderText)
        {
            emailField.value = ""; // Clear the placeholder text when field gets focus
        }
    }

    private void OnEmailFieldBlur(BlurEvent evt)
    {
        if (string.IsNullOrEmpty(emailField.value))
        {
            emailField.value = placeholderText; // Show placeholder if the field is empty
        }
    }

    private void OnPasswordFieldFocus(FocusInEvent evt)
    {
        if (passwordField.value == placeholderText)
        {
            passwordField.value = ""; // Clear the placeholder text when field gets focus
            passwordField.isPasswordField = true; // Change to password field to mask input
        }
    }

    private void OnPasswordFieldBlur(BlurEvent evt)
    {
        if (string.IsNullOrEmpty(passwordField.value))
        {
            passwordField.isPasswordField = false; // Show placeholder if the field is empty
            passwordField.value = placeholderText;
        }
    }

    private void OnLoginButtonClicked()
    {
        emailErrorMessage.style.display = DisplayStyle.None;
        passwordErrorMessage.style.display = DisplayStyle.None;

        bool isValid = true;

        if (string.IsNullOrEmpty(emailField.value) || emailField.value == placeholderText)
        {
            ShowErrorMessage(emailErrorMessage, "Please enter email.", Color.red);
            isValid = false;
        }
        else if (!IsValidEmail(emailField.value))
        {
            ShowErrorMessage(emailErrorMessage, "Invalid email format.", Color.red);
            isValid = false;
        }
        else
        {
            ShowErrorMessage(emailErrorMessage, "Email format is correct.", Color.green);
        }

        if (passwordField.value == placeholderText || string.IsNullOrEmpty(passwordField.value))
        {
            ShowErrorMessage(passwordErrorMessage, "Please enter password.", Color.red);
            isValid = false;
        }

        if (isValid)
        {
            ShowErrorMessage(emailErrorMessage, "Login successful", Color.green);
            ShowErrorMessage(passwordErrorMessage, "Login successful", Color.green);
            Debug.Log("Login successful");

            loginFrame.style.display = DisplayStyle.None; // Hide the login frame
            Debug.Log($"Initializing PostLoginManager with email: {emailField.value}");
            postLoginManager.Initialize(root, emailField.value);
            postLoginManager.ShowWelcomeScreen(); // Show the welcome screen
        }
    }

    private void ShowErrorMessage(Label label, string message, Color color)
    {
        label.text = message;
        label.style.color = new StyleColor(color);
        label.style.display = DisplayStyle.Flex;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
