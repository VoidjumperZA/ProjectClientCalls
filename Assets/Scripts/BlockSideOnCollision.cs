using UnityEngine;
using System.Collections;

public class BlockSideOnCollision : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private DataHandler dataHandler;

    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    // Use this for initialization
    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        playerInput = gameObject.GetComponentInParent<PlayerInput>();
        dataHandler = gameManager.GetComponent<DataHandler>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = gameObject.GetComponentInParent<Transform>().position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "JumpableObject")
        {
            Debug.Log("Child collides");
            playerMovement.SetForceMovement(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Lume")
        {
            dataHandler.IncrementCurrentSanity(dataHandler.GetSanityGainOnFirefly((int)dataHandler.difficulty));
            dataHandler.IncrementSanityBuffer(dataHandler.GetSanityGainOnFirefly((int)dataHandler.difficulty));
            Destroy(col.gameObject);
            //need to just switch off the renderer and collider and reanble later
        }
    }
}
