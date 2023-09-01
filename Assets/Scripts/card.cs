using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    // �Ҹ� 
    public AudioClip flip; // ������ ����
    public AudioSource audioSource; // ���� �� ���� ������ ������ ���ΰ�

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

        transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f); // ȸ������ ����

        if (gameManager.I.firstCard == null)
        {
            // ���� ���� �� ī�尡 ù ��° ī���̴�.
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            // ���� ���� �� ī�尡 �� ��° ī���̴�.
            gameManager.I.secondCard = gameObject;

            // ��Ī���ּ���. (��Ī �Լ� �θ���)
            gameManager.I.isMatched();
        }
    }

    public void destroyCard()
    {
        // 1���� ���� ����
        Invoke("destroyCardInvoke", 1.0f);
    }
    void destroyCardInvoke()
    {
        // ����
        Destroy(gameObject);
    }
    public void closeCard()
    {
        // 1�� �� ���� ����
        Invoke("closeCardInvoke", 1.0f);
    }

    void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }
}
