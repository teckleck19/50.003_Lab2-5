using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;
    private Rigidbody2D marioBody;
    private Animator marioAnimator;
    private SpriteRenderer marioSprite;
    private Collider2D marioCollider;
    private AudioSource marioJumpAudio;
    public ParticleSystem dust;
    public Text gameOverText;
    private bool isDead;
    //private bool isADKeyUp;
    //private bool isSpacebarUp;
    private bool onGroundState;
    private bool faceRightState;
    private bool countScoreState;
    public CustomCastEvent onPlayerCast;

    void Start()
    {
        isDead = false;
        //isADKeyUp = true;
        //isSpacebarUp = true;
        onGroundState = false;
        faceRightState = true;

        marioBody = GetComponent<Rigidbody2D>();
        marioAnimator = GetComponent<Animator>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioCollider = GetComponent<Collider2D>();
        marioJumpAudio = GetComponent<AudioSource>();
        
        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;
    }

    // Update is called once per frame
    /* void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
            isADKeyUp = false;
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	            marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
            isADKeyUp = false;
            if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	            marioAnimator.SetTrigger("onSkid");
        }
        if (Input.GetKeyUp("d") && Input.GetKeyUp("a")){
            isADKeyUp = true;
        }
        if (Input.GetKeyDown("z")){
          CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z,this.gameObject);
        }

        if (Input.GetKeyDown("x")){
          CentralManager.centralManagerInstance.consumePowerup(KeyCode.X,this.gameObject);
        }
        if (Input.GetKeyDown("space") && onGroundState){
          isSpacebarUp = false;
          onGroundState = false;
          marioAnimator.SetBool("onGround", onGroundState);
          //countScoreState = true;
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                isSpacebarUp = true;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                countScoreState = true; //check if Gomba is underneath
            }
        }
    } */
    void Update() {
        //Update and not FixedUpdate since this logic has nothing to do with the Physics Engine:
        if(!isDead){
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
            onPlayerCast.Invoke(KeyCode.Z);
            }

            if (Input.GetKeyDown("x")){
            onPlayerCast.Invoke(KeyCode.X);
            }
        }

        
        
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }
    void FixedUpdate(){

        
        if(!isDead){
            //dynamic rigidbody
            float moveHorizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (Mathf.Abs(marioBody.velocity.x) < marioMaxSpeed.Value)
                    marioBody.AddForce(movement * marioMaxSpeed.Value);
            }else{
            marioBody.velocity = new Vector2(0,marioBody.velocity.y);
            }
        

            if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
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
