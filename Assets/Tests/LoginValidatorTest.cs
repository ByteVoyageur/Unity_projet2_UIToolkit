using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoginValidatorTest
{
    private UIDocument uiDocument;
    private TextField emailField;
    private TextField passwordField;
    private Button loginButton;

    [UnityTest]
    public IEnumerator InputFieldTest()
    {
        // Load the scene
        SceneManager.LoadScene(0);
        yield return new WaitForSeconds(1);  // wait for the scene to load completely

        // Get the UIDocument
        uiDocument = GameObject.FindObjectOfType<UIDocument>();
        Assert.IsNotNull(uiDocument, "UIDocument not found");

        // Get the UI elements
        emailField = uiDocument.rootVisualElement.Q<TextField>("emailInput");
        Assert.IsNotNull(emailField, "Email input field not found");

        passwordField = uiDocument.rootVisualElement.Q<TextField>("passwordInput");
        Assert.IsNotNull(passwordField, "Password input field not found");

        // Get the login button
        loginButton = uiDocument.rootVisualElement.Q<Button>("buttonSubmit");
        Assert.IsNotNull(loginButton, "Login button not found");

        yield return null;
    }

        [UnityTest]
    public IEnumerator ButtonSubmitTest()
    {
        SceneManager.LoadScene(0);
        yield return new WaitForSeconds(1);

        UIDocument uiDocument = GameObject.FindObjectOfType<UIDocument>();
        Assert.IsNotNull(uiDocument, "UIDocument not found");

        Button loginButton = uiDocument.rootVisualElement.Q<Button>("buttonSubmit");
        Assert.IsNotNull(loginButton, "Login button not found");

    }

    [UnityTest]
    public IEnumerator Test_EmptyEmailErrorMessage()
    {
        yield return InputFieldTest();  // Ensure the UI elements are initialized

        // Set empty values
        emailField.value = "";
        passwordField.value = "validPassword";

        // Simulate button click
        using (var clicked = new NavigationSubmitEvent() { target = loginButton })
            loginButton.SendEvent(clicked);

        yield return null; // ensure the event is processed

        // Get error message
        Label emailErrorMessage = uiDocument.rootVisualElement.Q<Label>("InfoHintEmail");
        Assert.IsNotNull(emailErrorMessage, "Email error message label not found");

        // Assert error message
        Assert.AreEqual("Please enter email.", emailErrorMessage.text);
    }

    [UnityTest]
    public IEnumerator Test_InvalidEmailFormat()
    {
        yield return InputFieldTest();  // Ensure the UI elements are initialized

        // Set invalid email value
        emailField.value = "ifkld";
        passwordField.value = "validPassword";

        // Simulate button click
        using (var clicked = new NavigationSubmitEvent() { target = loginButton })
            loginButton.SendEvent(clicked);

        yield return null; // ensure the event is processed

        // Get error message
        Label emailErrorMessage = uiDocument.rootVisualElement.Q<Label>("InfoHintEmail");
        Assert.IsNotNull(emailErrorMessage, "Email error message label not found");

        // Assert error message
        Assert.AreEqual("Invalid email format.", emailErrorMessage.text);
    }

    [UnityTest]
    public IEnumerator Test_InvalidPasswordFormat()
    {
        yield return InputFieldTest();

        passwordField.value = "";

        using (var clicked = new NavigationSubmitEvent(){ target = loginButton })
            loginButton.SendEvent(clicked);
        
        yield return null;

        Label passwordErrorMessage = uiDocument.rootVisualElement.Q<Label> ("infoHintPassword");
        Assert.IsNotNull(passwordErrorMessage,"Password error message label not found");

        Assert.AreEqual("Please enter password.", passwordErrorMessage.text);
    }
}
