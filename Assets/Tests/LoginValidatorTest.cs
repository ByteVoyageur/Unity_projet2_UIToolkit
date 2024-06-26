using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoginValidatorTest
{
    [UnityTest]
    public IEnumerator LoginValidator_EmptyFields_ShowErrorMessages()
    {
        // Load the scene
        SceneManager.LoadScene(0);
        yield return new WaitForSeconds(1);  // wait for the scene load completely

        // Get the UIDocument
        UIDocument uiDocument = GameObject.FindObjectOfType<UIDocument>();
        Assert.IsNotNull(uiDocument, "UIDocument not found");

        // Get the UI elements
        TextField emailField = uiDocument.rootVisualElement.Q<TextField>("emailInput");
        Assert.IsNotNull(emailField, "Email input field not found");

        TextField passwordField = uiDocument.rootVisualElement.Q<TextField>("passwordInput");
        Assert.IsNotNull(passwordField, "Password input field not found");

        // Set empty values
        emailField.value = "";
        passwordField.value = "";

        // Get the login button
        Button loginButton = uiDocument.rootVisualElement.Q<Button>("buttonSubmit");
        Assert.IsNotNull(loginButton, "Login button not found");

        // Simulate button click
        loginButton.clicked += () => Debug.Log("Login button clicked");
        loginButton.SendEvent(new NavigationSubmitEvent());

        yield return null; // ensure the event is processed

        // Get error messages
        Label emailErrorMessage = uiDocument.rootVisualElement.Q<Label>("InfoHintEmail");
        Assert.IsNotNull(emailErrorMessage, "Email error message label not found");

        Label passwordErrorMessage = uiDocument.rootVisualElement.Q<Label>("infoHintPassword");
        Assert.IsNotNull(passwordErrorMessage, "Password error message label not found");

        // Assert error messages
        
    }
}
