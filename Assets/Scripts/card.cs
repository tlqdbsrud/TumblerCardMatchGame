using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    // 소리 
    public AudioClip flip; // 실행할 음악
    public AudioSource audioSource; // 누가 그 음악 파일을 실행할 것인가

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCard()
    {
        audioSource.PlayOneShot(flip);

        anim.SetBool("isOpen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);

        transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f); // 회색으로 변경

        if (gameManager.I.firstCard == null)
        {
            // 현재 내가 연 카드가 첫 번째 카드이다.
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            // 현재 내가 연 카드가 두 번째 카드이다.
            gameManager.I.secondCard = gameObject;

            // 매칭해주세요. (매칭 함수 부르기)
            gameManager.I.isMatched();
        }
    }

    public void destroyCard()
    {
        // 1초후 동작 실행
        Invoke("destroyCardInvoke", 1.0f);
    }
    void destroyCardInvoke()
    {
        // 제거
        Destroy(gameObject);
    }
    public void closeCard()
    {
        // 1초 후 동작 실행
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }
}
