using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCo : MonoBehaviour {

    //переменная для установки макс. скорости персонажа
    public float maxSpeed = 10f;
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;
    //ссылка на компонент анимаций
    private Animator anim;
    protected Rigidbody2D RB2d;
    //находится ли персонаж на земле или в прыжке?
    private bool isGrounded = false;
    //ссылка на компонент Transform объекта
    //для определения соприкосновения с землей
    public Transform groundCheck;
    //радиус определения соприкосновения с землей
    private float groundRadius = 0.2f;
    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;


    public GameObject ulikaPrefab;
    public GameObject ulika;
    private GameObject waterPrefab;
    public GameObject water;

    public int score;
    public Text ScoreText;
    // public Text LifeText;


    /// <summary>
    /// Начальная инициализация
    /// </summary>
    private void Start()
    {
        anim = GetComponent<Animator>();
        RB2d = GetComponent<Rigidbody2D>();
       ulika = GameObject.FindWithTag("ulika");
      // water = GameObject.FindWithTag("water");

    }

    /// <summary>
    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    /// </summary>
    private void FixedUpdate()
    {

        //определяем, на земле ли персонаж
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //устанавливаем соответствующую переменную в аниматоре
        anim.SetBool("Ground", isGrounded);
        //устанавливаем в аниматоре значение скорости взлета/падения
        anim.SetFloat("vSpeed", RB2d.velocity.y);
        //если персонаж в прыжке - выход из метода, чтобы не выполнялись действия, связанные с бегом
        if (!isGrounded)
            return;
        //используем Input.GetAxis для оси Х. метод возвращает значение оси в пределах от -1 до 1.
        //при стандартных настройках проекта 
        //-1 возвращается при нажатии на клавиатуре стрелки влево (или клавиши А),
        //1 возвращается при нажатии на клавиатуре стрелки вправо (или клавиши D)
        float move = Input.GetAxis("Horizontal");

        //в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
        //приэтом нам нужен модуль значения
        anim.SetFloat("Speed", Mathf.Abs(move));

        //обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
        //равную значению оси Х умноженное на значение макс. скорости
        RB2d.velocity = new Vector2(move * maxSpeed, RB2d.velocity.y);

        //если нажали клавишу для перемещения вправо, а персонаж направлен влево
        if (move > 0 && !isFacingRight)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if (move < 0 && isFacingRight)
            Flip();
    }


   // private void Awake()
   // {
   //     ScoreText.text = ":" + score.ToString();
   // }


    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingRight = !isFacingRight;
        //получаем размеры персонажа
        Vector3 theScale = transform.localScale;
        //зеркально отражаем персонажа по оси Х
        theScale.x *= -1;
        //задаем новый размер персонажа, равный старому, но зеркально отраженный
        transform.localScale = theScale;
    }

    private void Update()
    {
        //если персонаж на земле и нажат пробел...
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //устанавливаем в аниматоре переменную в false
            anim.SetBool("Ground", false);
            //прикладываем силу вверх, чтобы персонаж подпрыгнул
            RB2d.AddForce(new Vector2(0, 600));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject == water)
        //    ulika = GameObject.FindWithTag("water");
        //{

         //   score--;
         //   ScoreText.text = score.ToString();
            
        //}



        if (col.gameObject == ulika)
                ulika = GameObject.FindWithTag("ulika");
        {
           
            score++;
            ScoreText.text = score.ToString();
            Destroy(col.gameObject);
        }


        if (col.gameObject.name == "endLevel")
        {
            if (!(GameObject.Find("svitock"))) Application.LoadLevel("scene2");
        }
    }

   // void OnGUI()
  //  {
   //     GUI.Box(new Rect(0, 0, 50, 50), "Uliki: " + score);
   // }
}
