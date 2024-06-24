using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoginValidatorTest
{
    // Test case for empty fields
    [Test]
    public IEnumerator LoginValidator_EmptyFields_ShowErrorMessages()
    {
        // Load the scene
        SceneManager.LoadScene("UI");  
        yield return null;  

        // Get the UIDocument
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        Assert.IsNotNull(uiDocument, "UIDocument not found");

        // Get the UI elements
        var emailField = uiDocument.rootVisualElement.Q<TextField>("InputEmailText");
        var passwordField = uiDocument.rootVisualElement.Q<TextField>("inputPasswordText");
        var emailErrorMessage = uiDocument.rootVisualElement.Q<Label>("InfoHintEmail");
        var passwordErrorMessage = uiDocument.rootVisualElement.Q<Label>("infoHintPassword");
        var loginButton = uiDocument.rootVisualElement.Q<Button>("buttonSubmit");

        // Set empty values
        emailField.value = "";
        passwordField.value = "";

        // Simulate button click
        using (var evt = new NavigationSubmitEvent() { target = loginButton })
        {
            loginButton.SendEvent(evt);
        }
        yield return null;  

        // Assert error messages
        Assert.AreEqual("Please enter email.", emailErrorMessage.text);
        Assert.AreEqual("Please enter password.", passwordErrorMessage.text);
    }
}
