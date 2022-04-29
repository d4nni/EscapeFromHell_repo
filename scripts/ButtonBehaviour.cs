using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    // b�r til variable fyrir click sound
    AudioSource clickSound;
    // senu n�mer �r playermovement skriftu, nota� til a� finna senu sem player deyr � svo hann geti haldi� �fram �ar
    int senuNr = PlayerMovement.senaNr;
    void Start()
    {
        // n�� er � audio source � objectinu
        clickSound = GetComponent<AudioSource>(); 
    }
    public void OnButtonPressMenu() // �essi method fer � gang �egar smellt er � spila takka � a�alvalmynd
    {
        // spilar click sound �egar smellt er � takka
        clickSound.Play();
        // n�r � n�verandi senu
        Scene scene = SceneManager.GetActiveScene();
        // hle�ur upp n�stu senu, sem myndi vera fyrsta bor�
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
    public void OnButtonPressVictoryMenu() // �essi method fer � gang �egar smellt er � a�alvalmynd takka � victory senu
    {
        clickSound.Play();
        // hle�ur upp senu 0, sem er a�alvalmyndin
        SceneManager.LoadScene(0);
    }
    public void OnButtonPressVictoryStart() // �essi fer � gang �egar smellt er � spila aftur takka � victory senu
    {
        clickSound.Play();
        // hle�ur upp fyrsta leveli beint, sem er sena 1 � build settings
        SceneManager.LoadScene(1);
    }
    public void OnButtonPressGameOver() // �essi fer � gang �egar smellt er � byrja aftur takka � game over senu
    {
        clickSound.Play();
        // hle�ur upp senu sem player d� � seinast
        SceneManager.LoadScene(senuNr);
    }
}
