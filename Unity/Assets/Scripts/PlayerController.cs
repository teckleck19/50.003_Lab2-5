using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Physics
    public float horizontalSpeed;
    public float maxSpeed = 10;
    public float upSpeed = 10;
    
    //Components
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private Collider2D marioCollider;
    //State
    private bool onGroundState = false;
    private bool faceRightState = true;
    //private bool countScoreState = false;
    //private bool gameoverState = false;

    //Other GameObjects
    //public Text livesText;
    public Text gameOverText;
    public Button replayButton;
    private Vector3 originalPosition;
    private Animator marioAnimator;
    private AudioSource marioJumpAudio;
    public ParticleSystem dust;
    public float min_X;
    public float max_X;
    private bool isDead;
    
    
  
    // Start is called before the first frame update
    void Start(){
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioCollider = GetComponent<Collider2D>();
        marioAnimator  =  GetComponent<Animator>();
        marioJumpAudio = GetComponent<AudioSource>();
        isDead=false;
        originalPosition = transform.position;
        GameManager.OnPlayerDeath  +=  PlayerDiesSequence;
    }

    void Update() {
        //Update and not FixedUpdate since this logic has nothing to do with the Physics Engine:
        if (!isDead){
          if (Input.GetKeyDown("a") && faceRightState){
              faceRightState = false;
              marioSprite.flipX = true;
              if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
                marioAnimator.SetTrigger("onSkid");
          }

          if (Input.GetKeyDown("d") && !faceRightState){
              faceRightState = true;
              marioSprite.flipX = false;
              if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
                marioAnimator.SetTrigger("onSkid");
          }  
          if (Input.GetKeyDown("z")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z,this.gameObject);
          }

          if (Input.GetKeyDown("x")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.X,this.gameObject);
          }

          
          
          marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        }
    }
    void FixedUpdate(){

        
        if (!isDead){
          //dynamic rigidbody
          float moveHorizontal = Input.GetAxis("Horizontal");
          if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (Mathf.Abs(marioBody.velocity.x) < maxSpeed)
                    marioBody.AddForce(movement * horizontalSpeed);
          }else{
            marioBody.velocity = new Vector2(0,marioBody.velocity.y);
          }
          /* if ((Input.GetKeyUp("a") || Input.GetKeyUp("d")) && onGroundState){
            // stop
            marioBody.velocity = Vector2.zero;
          } */
          /* if(!(transform.position.x<=min_X || transform.position.x>=max_X)){
            transform.position += new Vector3(moveHorizontal,0,0) * Time.deltaTime * horizontalSpeed;
          }
          if (transform.position.x<=min_X){
            transform.position = new Vector3(min_X+0.1f,transform.position.y,0);
          }
          if (transform.position.x>=max_X){
            transform.position = new Vector3(max_X-0.1f,transform.position.y,0);
          }
          */

          if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            marioAnimator.SetBool("onGround", onGroundState);
            //countScoreState = true;
          }
        }
        

        
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") ) {
          onGroundState = true;
          marioAnimator.SetBool("onGround", onGroundState);
          dust.Play();
          //countScoreState = false; // reset score state
        }
        
        if (col.gameObject.CompareTag("Obstacle") && Mathf.Abs(marioBody.velocity.y) < 0.01f) {
          onGroundState = true;  
          marioAnimator.SetBool("onGround", onGroundState);
        }
        if (col.gameObject.CompareTag("PipeHead") && Mathf.Abs(marioBody.velocity.y) < 0.01f) {
          onGroundState = true;  
          marioAnimator.SetBool("onGround", onGroundState);
        }

        /* if (col.gameObject.CompareTag("Enemy")){
          lives--;
          if (lives>0){
            livesText.text = "Lives: " + lives.ToString(); 
            transform.position = originalPosition;
            //Time.timeScale = 0.0f;
          }else{
            livesText.text = "Lives: " + lives.ToString(); 
            marioBody.AddForce(Vector2.up * (upSpeed-10.0f), ForceMode2D.Impulse);
            marioCollider.enabled = false;
            gameoverState = true;
            
            // Debug.Log("GAME OVER");
            // livesText.text = "Lives: " + lives.ToString(); 
            // Time.timeScale = 0.0f;
            // gameOverText.gameObject.SetActive(true);
            // replayButton.gameObject.SetActive(true);
          }
        } */
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        /* if (other.gameObject.CompareTag("Enemy")){
          lives--;
          if (lives>0){
            livesText.text = "Lives: " + lives.ToString(); 
            transform.position = originalPosition;
            //Time.timeScale = 0.0f;
          }else{
            livesText.text = "Lives: " + lives.ToString(); 
            marioBody.AddForce(Vector2.up * (upSpeed-10.0f), ForceMode2D.Impulse);
            marioCollider.enabled = false;
            gameoverState = true;
            // Debug.Log("GAME OVER");
            // livesText.text = "Lives: " + lives.ToString(); 
            // Time.timeScale = 0.0f;
            // gameOverText.gameObject.SetActive(true);
            // replayButton.gameObject.SetActive(true);
          }
        } */
        
    }

    public void ReplayButtonClicked()
    {    
      SceneManager.LoadScene("Mario_1");
    }

    IEnumerator gameOver(){
      yield return null;
      Debug.Log("GAME OVER");
      gameOverText.gameObject.SetActive(true);

      this.gameObject.SetActive(false);
    }
    void PlayJumpSound(){
      marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
    }
    public void PlayerDiesSequence()
    {
      Debug.Log("GAME OVER");
      gameOverText.gameObject.SetActive(true);
      isDead = true;
      marioAnimator.SetBool("isDead", true);
      GetComponent<Collider2D>().enabled = false;
      marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
      marioBody.gravityScale = 30;
      StartCoroutine(dead());
    }
    
    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }
    
    
}

