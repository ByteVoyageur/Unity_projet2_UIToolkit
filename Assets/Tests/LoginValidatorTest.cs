using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoginValidatorTest
{
    // test empty case
    [Test]
    public IEnumerator LoginValidator_EmptyFields_ShowErrorMessages()
    {
        SceneManager.LoadScene("UI");  
        yield return null;  

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        Assert.IsNotNull(uiDocument, "UIDocument not found");

        var emailField = uiDocument.rootVisualElement.Q<TextField>("InputEmailText");
        var passwordField = uiDocument.rootVisualElement.Q<TextField>("inputPasswordText");
        var emailErrorMessage = uiDocument.rootVisualElement.Q<Label>("InfoHintEmail");
        var passwordErrorMessage = uiDocument.rootVisualElement.Q<Label>("infoHintPassword");
        var loginButton = uiDocument.rootVisualElement.Q<Button>("buttonSubmit");

        emailField.value = "";
        passwordField.value = "";

        using (var evt = new NavigationSubmitEvent() { target = loginButton })
        {
            loginButton.SendEvent(evt);
        }
        yield return null;  

        Assert.AreEqual("Please enter email.", emailErrorMessage.text);
        Assert.AreEqual("Please enter password.", passwordErrorMessage.text);
    }

    [Test]
    public IEnumerator LoginValidator_NonEmptyFields_ShowSuccessMessage()
    {
        SceneManager.LoadScene("UI");  
        yield return null;  

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        Assert.IsNotNull(uiDocument, "UIDocument not found");

        var emailField = uiDocument.rootVisualElement.Q<TextField>("InputEmailText");
        var passwordField = uiDocument.rootVisualElement.Q<TextField>("inputPasswordText");
        var emailErrorMessage = uiDocument.rootVisualElement.Q<Label>("InfoHintEmail");
        var passwordErrorMessage = uiDocument.rootVisualElement.Q<Label>("infoHintPassword");
        var loginButton = uiDocument.rootVisualElement.Q<Button>("buttonSubmit");

        emailField.value = "test@example.com";
        passwordField.value = "password";

        using (var evt = new NavigationSubmitEvent() { target = loginButton })
        {
            loginButton.SendEvent(evt);
        }
        yield return null;  

        Assert.AreEqual("Login successful", emailErrorMessage.text);
        Assert.AreEqual("Login successful", passwordErrorMessage.text);
    }
}
