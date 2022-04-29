using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    // býr til variable fyrir click sound
    AudioSource clickSound;
    // senu númer úr playermovement skriftu, notað til að finna senu sem player deyr í svo hann geti haldið áfram þar
    int senuNr = PlayerMovement.senaNr;
    void Start()
    {
        // náð er í audio source á objectinu
        clickSound = GetComponent<AudioSource>(); 
    }
    public void OnButtonPressMenu() // þessi method fer í gang þegar smellt er á spila takka í aðalvalmynd
    {
        // spilar click sound þegar smellt er á takka
        clickSound.Play();
        // nær í núverandi senu
        Scene scene = SceneManager.GetActiveScene();
        // hleður upp næstu senu, sem myndi vera fyrsta borð
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
    public void OnButtonPressVictoryMenu() // þessi method fer í gang þegar smellt er á aðalvalmynd takka í victory senu
    {
        clickSound.Play();
        // hleður upp senu 0, sem er aðalvalmyndin
        SceneManager.LoadScene(0);
    }
    public void OnButtonPressVictoryStart() // þessi fer í gang þegar smellt er á spila aftur takka í victory senu
    {
        clickSound.Play();
        // hleður upp fyrsta leveli beint, sem er sena 1 í build settings
        SceneManager.LoadScene(1);
    }
    public void OnButtonPressGameOver() // þessi fer í gang þegar smellt er á byrja aftur takka í game over senu
    {
        clickSound.Play();
        // hleður upp senu sem player dó í seinast
        SceneManager.LoadScene(senuNr);
    }
}
