using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private int _powerupID;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerupID) {
                    case 0:
                    player.TripleShotActive();
                        break;

                    case 1:
                        player.SpeedActive();
                        break;

                    case 2:
                        player.ShieldActive();
                        break;

                    default:
                        Debug.Log("Default Case");
                        break;
                    
               }
            }
            Destroy(this.gameObject);
        }
    }
}
