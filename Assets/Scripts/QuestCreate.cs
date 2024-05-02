using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCreate : MonoBehaviour
{
    private QuestManager questManager;
    private List<Quiz> quizzes;

    // Start is called before the first frame update
    void Start()
    {
        questManager = GetComponent<QuestManager>();
        quizzes = new List<Quiz>();
        CreateQuizzes();
        CreateQuests();

    }

    private List<Quiz> QuizListGroupByLevel(int levelQuest)
    {
        List<Quiz> quizListGroupByLevel = new List<Quiz>();
        for (int i = 0; i < quizzes.Count; i++)
        {
            if (quizzes[i].levelQuiz == levelQuest)
            {
                quizListGroupByLevel.Add(quizzes[i]);
            }
        }
        return quizListGroupByLevel;
    }

    private void CreateQuizzes()
    {
        quizzes.Add(new Quiz(question: "In the realm of coding described by Lilith the Enchantress, variables are compared to enchanted sigils that hold the essence of a character's prowess. Why is this analogy apt? How does it relate to variables in programming?",
            answer1: "Variables store data like sigils hold essence.",
            answer2: "Variables empower characters like sigils do.",
            answer3: "Variables encapsulate traits like sigils.",
            answer4: "All of the above.",
            correctAnswer: 3,
            levelQuiz: 1));
        quizzes.Add(new Quiz(question: "What is the purpose of the if statement in programming, and how is it used with the health variable in a game scenario?",
            answer1: "\"if\" statements trigger actions based on conditions, like checking health.",
            answer2: "In games, \"if\" statements set health thresholds by evaluating conditions.",
            answer3: "\"if\" statements in games halt actions when conditions like zero health are met.",
            answer4: "None of the above.",
            correctAnswer: 1,
            levelQuiz: 1));

        quizzes.Add(new Quiz(question: "What distinguishes the \"while\" loop from the \"for\" loop in the context of the provided description?",
            answer1: "\"While\" loops iterate until false; \"for\" loops iterate over elements.",
            answer2: "\"While\" loop performs actions until false, \"for\" loop iterates over elements.",
            answer3: "\"While\" loop iterates until condition false; \"for\" loop acts until false.",
            answer4: "\"While\" loop acts until false; \"for\" loop ends based on condition.",
            correctAnswer: 1,
            levelQuiz: 2));

        quizzes.Add(new Quiz(question: "In the provided scripts for the \"while\" and \"for\" loops, what is the main purpose of each loop in the context of battling enemies?",
            answer1: "\"While\" loop fights single enemy, \"for\" loop battles multiple.",
            answer2: "\"While\" loop engages until enemy defeated, \"for\" loop battles each enemy.",
            answer3: "\"While\" loop engages based on condition, \"for\" loop iterates over enemies.",
            answer4: " \"While\" loop iterates over enemies, \"for\" loop fights single enemy.",
            correctAnswer: 3,
            levelQuiz: 2));

        quizzes.Add(new Quiz(question: "In the enchanted realm described by the narrator, what role do functions play in the mystical craft of coding?",
            answer1: "Functions serve as weapons to defeat enemies.",
            answer2: "Functions are magical rituals that encapsulate reusable sequences of code.",
            answer3: "Functions are complex rituals performed in coding.",
            answer4: "Functions are keys to unlocking secrets in coding.",
            correctAnswer: 2,
            levelQuiz: 3));

        quizzes.Add(new Quiz(question: "What is the main purpose of defining a function, as demonstrated in the provided script?",
            answer1: "To execute a series of commands simultaneously.",
            answer2: "To encapsulate code for future reuse.",
            answer3: "To cast spells on enemies.",
            answer4: "To perform complex rituals.",
            correctAnswer: 2,
            levelQuiz: 3));
    }
    private void CreateQuests()
    {
        questManager.AddQuest(new Quest(title: "Unleashing the Magic of Programming",
            description: "Welcome, Champion!\r\n\r\nGreetings, brave warrior, and welcome to the realm of coding! I am Lilith the Enchantress, here to guide you on your journey to mastering the arcane arts of programming. In this mystical realm, the power of magic flows through the lines of code, empowering you to craft spells with the ferocity of your blade and the cunning of your intellect.\r\n\r\nBefore we embark on our quest, let me introduce you to the enigmatic concept of variables. In our world, variables are akin to enchanted sigils that contain the essence of your character's prowess. Picture \"health\" as the life force coursing through your veins, and \"mana\" as the reservoir of mystical energy fueling your every strike.\r\n\r\nNow, let us join forces to cast our first incantation. With the language of code, we can harness the elements to empower your character in battle. Witness:\r\n\r\n<color=#FF0000>health = 100 </color>  # Set the initial health value\r\n\r<color=#FF0000>if health < 50:\r\n    mend_wounds()  </color>\r\n\r\nIn this spellbinding script, we fortify your character with an initial health value of 100. Then, with a keen eye for battle, we assess if your character's health has dwindled below 50. If so, we invoke the \"mend_wounds()\" function to restore vitality and bolster your strength for the challenges ahead.\r\n\r\nRemember, Champion, with each line of code, you shape the destiny of our realm. As you venture forth, you will encounter more potent enchantments, including loops to hone your combat skills and debugging spells to sharpen your wit when it falters.\r\n\r\nNow, take up your keyboard, and let the magic surge through your fingertips like the strike of a mighty warrior. Together, we shall unlock the secrets of programming and triumph over the trials that await us in the abyss of code!",
            levelQuest: 1,
            pages: 4,
            instructions: "Find Lilith the Enchantress",
            quizzes: QuizListGroupByLevel(levelQuest: 1)));
        List<Quiz> quizzes = new List<Quiz>();
        quizzes = QuizListGroupByLevel(levelQuest: 1);
        Debug.Log(quizzes[0].correctAnswer);
        Debug.Log(quizzes[1].correctAnswer);

        questManager.AddQuest(new Quest(title: "The Path of Persistence",
            description: "Ah, greetings, stalwart Champion! Your prowess with the blade is matched only by your dedication to mastering the arcane arts of coding. Now, let us sharpen our skills further as we tread the Path of Persistence.\r\n\r\nIn the realm of coding, persistence is the cornerstone of victory, allowing us to persevere through challenges until our goals are achieved. Today, we shall uncover two formidable techniques: the relentless \"while\" loop and the resolute \"for\" loop, each a stalwart ally in our journey.\r\n\r\nFirst, let us unsheathe the \"while\" loop, a spell of unwavering determination. With this incantation, we can execute a series of actions as long as a certain condition holds true. Behold its invocation:\r\n\r\n<color=#FF0000>enemy_health = 100\r\r\nwhile enemy_health > 0:\r\n    swing_sword()\r\n    enemy_health -= sword_damage</color>\r\n\r\nIn this mighty script, we continue to wield our blade against our foe until their health is depleted. With each swing of our sword, the enemy's health diminishes until victory is ours.\r\n\r\nBut fear not, for the \"for\" loop stands ready to bolster our efforts with structured repetition. With this enchantment, we can traverse through a sequence of adversaries, striking them down one by one. Witness its incantation:\r\n\r\n<color=#FF0000>enemies = [\"Goblin\", \"Orc\", \"Skeleton\", \"Dragon\"]\r\n\r\nfor enemy in enemies:\r\n    engage_in_combat(enemy)</color>\r\n\r\nIn this valiant script, we march forth to battle against a horde of enemies, each one met with the fury of our blade. With each iteration of the loop, a new foe is vanquished until none remain standing.\r\n\r\nBut remember, Champion, with great power comes great responsibility. Exercise caution when wielding these looping spells, for misuse can lead to unintended consequences. Always ensure that your conditions are just and your actions are true.\r\n\r\nNow, take up your blade and your keyboard, and let the echoes of your courage resound through the realm of code. Together, we shall conquer the challenges that lie ahead and emerge victorious in our quest for mastery.\r\n\r\nOnward, brave Champion, to glory and triumph!",
            levelQuest: 2,
            pages: 5,
            instructions: "Find the Yellow Dress Demon",
            quizzes: QuizListGroupByLevel(levelQuest: 2)));

        questManager.AddQuest(new Quest(title: "The Arcane Art of Functions",
            description: "Welcome, Champion!\r\n\r\nNow that you've begun to grasp the fundamentals of coding, it's time to delve deeper into the mysteries of the craft. In this quest, we shall unlock the secrets of functions, potent spells that will empower you to wield the magic of programming with even greater finesse.\r\n\r\nIn our enchanted realm, functions serve as magical rituals, allowing us to encapsulate sequences of code into reusable incantations. Whether it be casting spells, wielding weapons, or performing complex rituals, functions are the key to unleashing the full extent of your coding prowess.\r\n\r\nLet us begin our journey by crafting our first function. Behold the incantation:\r\n\r\n<color=#FF0000>def attack_enemy():\r\n    swing_sword()\r\n    cast_spell()</color>\r\n\r\nIn this mystical script, we have defined a function named \"attack_enemy.\" Within its arcane boundaries lie the commands to swing your sword and unleash a spell upon your foe. With this function at your disposal, you can unleash devastating attacks with a single invocation.\r\n\r\nBut wait, there's more to functions than meets the eye! They can accept inputs, known as parameters, and return values, imbuing them with even greater versatility. With parameters, you can tailor your spells to specific needs, while return values allow your functions to yield mystical artifacts of their own.\r\n\r\nNow, let us wield our newfound knowledge to overcome the challenges that lie ahead. Practice crafting functions of your own, and soon you will wield the power of programming with the skill of a true master.\r\n\r\nOnward, brave Champion, to new heights of coding mastery and adventure!",
            levelQuest: 3,
            pages: 4,
            instructions: "Find Pyralis, the Mistress of Code",
            quizzes: QuizListGroupByLevel(levelQuest: 3)));
    }
}
