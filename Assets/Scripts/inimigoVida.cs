using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigoVida : MonoBehaviour
{
    public int vidas = 3;
    private bool podeTomarDano = true;
    private bool morto = false;
    private Animator animator; // Referência ao Animator
    public AudioClip somDano;
    // Start is called before the first frame update
    private AudioSource audioSource;
    void Start()
    {
        // Obtém o componente Animator que está no mesmo GameObject
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    public void TomarDano(){
        if (podeTomarDano && !morto)
        {
            podeTomarDano = false;
            vidas -= 1;
            audioSource.clip = somDano;
            audioSource.Play();

            if (vidas == 0)
            {
                morrer();
            }
            else if (vidas > 0)
            {
                tomarhit();
            }
            Invoke("imunidade", 1f);
            Invoke("paraAudio", 0.4f);
        }
    }
    void paraAudio()
    {
        audioSource.Stop();
    }

    void imunidade()
    {
        podeTomarDano = true;
    }
    void Suma(){
        Destroy(gameObject);
    }
    void morrer(){
        animator.SetBool("podeAndar", false);
        animator.SetTrigger("morreu");
        Invoke("mortinho", 0.5f);
        morto = true;
        Invoke("Suma",3f);
    }
    void tomarhit(){
        animator.SetTrigger("dano");
        animator.SetBool("podeAndar", false);
        Invoke("podeAndar", 0.7f); // Volta a permitir movimento após 0.5 segundos
    }
    void podeAndar(){
        animator.SetBool("podeAndar", true);
        animator.ResetTrigger("dano"); // Reseta o trigger para evitar que a animação se repita
    }
    void mortinho(){
        animator.SetBool("morto", true);
    }
}
