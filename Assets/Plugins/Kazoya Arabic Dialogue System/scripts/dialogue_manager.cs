using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Animations;

public class dialogue_manager : MonoBehaviour
{
    // scipt written by : kazoya , source : https://github.com/zaidmermam/Unity-Arabic-Dialogue-System
    // also : this scipt uses a script called : arabicfixer.cs from this video by 6wrni youtube channle : https://www.youtube.com/watch?v=zjq1r4FgLAY
    // and this script also uses a script for the text effects done by this user : https://www.youtube.com/channel/UC-n80lsjMCS3OJR8kFrATLg

    [Header("strings")]
    [TextArea(6, 10)]
    public string[] sentences; // يحتوي على جميع الجمل التي ستعرض  array 
    [Space]
    [Header("floats , ints , bools")]
    private int index; // int يعرف السكريبت على رقم الجملة التي يتوجب عرضها بداخل الأرراي 
    public float typespeed; // سرعة الكتابة التي تحدد الفرق بالثواني بين كل حرف و حرف , بالتالي كلما كان أقل كلما كانت سرعة الكتابة أسرع
    public float max_type_speed; // سرعة الكتابة القصوى التي سيتم التبديل إليها عن الضغط على زر
    public float normal_type_speed; // سرعة الكتابة العادية التي تسمح لنا بإعادة سرعة الكتابة للسرعة العادية
    public bool in_dialog; // يعرف السكريبت إذا كان النظام يعرض نصا أم لا تجنبا لحدوث تداخل بين النصوص و عدم السماح للاعب بالإنتقال للنص التالي إن كان هذا البول صحيحا
    [Space]
    [Header("components")]
    public ArabicFixerTMPRO arabicfix; // كومبونينت المسؤولة عن تصحيح الكتابة العربية شكرا لمنصة طورني لمساعدتنا بهذا الشيئ (المصدر موجود في الأعلى ) م
    public GameObject press_visual; // كائن يحتوي على صورة الزر الذي يعبر عن أن الاعب قادر على الانتقال للجملة التالية
    public AudioSource dialogue_sound; // الصوت الذي يصدر عند عرض كل حرف
    public Animator dialogue_animator; // الأنيميتور المسؤول عن أنيميشن الفتح و الإغلاق للنظام
    public cool_text wavy; // تأثير الكتابة المموج 
    public fear_text fear;// تأثير كتابة الخوف
    [Space]
    [Space]
    [Header("Dialogue emotes")]
    public Image image_to_display; // الصورة التي ستعرض صورة السخصية المتحدثة
    public string gn = "gn"; // الجملة التي تعبر عن الشخصية الأولى
    public string sn = "sn";// الجملة التي تعبر عن الشخصية الثاني التي بالتالي إذا وجد السكريبت أنها موجودةسيقوم بوضع صورة الشخصية دون أن يعرض الجملة في النظام
    public Sprite gnI; // صورة الشخصية الأولى
    public Sprite snI; // صورة الشخصية الثانية 
    /* ملاحظة إذا كنت تريد أن تضيف شخصياتك قم بإضافة المزيد من المتغيرات كالتي فوق وهي : اسم الشخصية التي
     ستكتب اسمها في الإنسبيكتور و لن يعرض الاسم , و قم بتعريف الصورة المعبرة عن اسم الشخصية و مع القليل من النسخ و اللصق أسفل السكريبت ستعرض شخصيتك عند كتابة اسمها ! */



    private void Start()
    {
        dialogue_sound = GetComponentInChildren<AudioSource>(); //نعرف صوت الكلام المعروض 
        dialogue_animator = GetComponentInParent<Animator>(); // نعرف الأنيميتور المسؤول عن فتح و إغلاق النظام
        arabicfix = GetComponentInChildren<ArabicFixerTMPRO>(); // نعرف سكريبت تصحيح الكلام العربي
        wavy = GetComponentInChildren<cool_text>(); // نعرف تأثير الكتابة المموجة
        fear = GetComponentInChildren<fear_text>(); // تأثير كتابة الخوف أو الإرتجاف
        open_dialogue(); // مناداة فنكشين التي تقوم بتشغيل الدايالوغ ((((((ملاحظة مهمة)))))) إن كنت تريد مناداة
        // النظام من مكان أخر مثل أن يصتدم الاعب بجدار مخفي فقم بمناداة هذه الفنكشين التي ستفتح الدايالوغ ! و
        // لا تنسى مسحها من الستارت فتكشين هنا
        max_type_speed = typespeed / 10; // نعرف السكريبت سرعة الكتابة القصوى التي هي ثلاثة أضعاف الكتابة العادية
        normal_type_speed = typespeed; // نعرف السكريبت سرعة الكتابة العادية
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray()) // لووب يقوم بمناداة كل حرف من كل جملة
        {
            if (sentences[index].Contains("end")) //إذا كانت الجملة تحتوي على كلمة إند فقم بإنهاء الحوار
            {
                end_dialogue();
            }
            if (sentences[index].Contains("w")) //إذا احتوت الجملة على حرف الدبليو فقم بتفعيل التأثير المموج
            {
                wavy.enabled = true;
            }
            else if (!sentences[index].Contains("w")) // إن لم تنتهي فقم بإطفاءه
            {
                wavy.enabled = false;
            }
            if (sentences[index].Contains("f")) // إذا احتوت الجملة على حرف الإف فقم بتفعيل تأثير الإرتجاف
            {
                fear.enabled = true;
            }
            else // إذا لا فقم بإطفاءه
            {
                fear.enabled = false;
            }
            if (sentences[index].Contains(gn)) // جزء مهم لمن يريد أن يضيف شخصياته الخاصة هذا هو الجزء الذي عليك نسخه و لصقه !
            {
                image_to_display.sprite = gnI;
            }
            else if (sentences[index].Contains(sn)) //  إذا احتوت الجملة على الكلمة التي عرفناها
            {
                image_to_display.sprite = snI; // فقم بعرص الصورة المخصصة لهذه الكلمة
            }

            /* بالتالي إذا كنت تريد أن تضيف شخصيتك و لتكن محمد تقوم بالتالي : 

            else if (sentences[index].Contains(mn)) 
            {
                image_to_display.sprite = mnI; 
            }
            طبعا بعد ما كنت عرفت الجملة و الصورة ! 
            */

            // هذا الجزء مسؤول على أن يمنع الكسريبت من عرض الأحرف التي عرفناها و قد قمت بشرح معنى كل حرف بجانب السطر 
            if (letter != 'g' && letter != 's' && letter != 'w' && letter != 'e' && letter != 'n' && letter != 'd' && letter != 'f') // g = geralt / s = scarlet / n = normal / w = wavy ( for wavy text effect / e and d for avoiding typing "end" / f = fear text
            {
                arabicfix.fixedText += letter; // قم بإضافة الحرف إن لم يكن من الأحرف أعلاه
                dialogue_sound.Play(); // قم بتشغيل الصوت 
                yield return new WaitForSeconds(typespeed);// انتظر مقدار سرعة الكتابة من الثواني حتى تكرر العملية
            }

        }
    }
    private void Update()
    {
        if (Input.GetButton("Fire1")) // إذا ضغط الاعب على الزر
        {
            typespeed = max_type_speed; // قم بتسريع سرعة الكتابة
        }
        else if (Input.GetButtonUp("Fire1")) // عندما يرفع يده عن الزر 
        {
            typespeed = normal_type_speed; // أعد سرعة الكتابة لسرعتها الطبيعية
        }
        if (arabicfix.fixedText.Length == sentences[index].Length - 3) // إذا كانت الجملة المعروضة تساوي الجملة المعرفة في مجموعة الجمل 
        {
            print("dialog done !"); // فسنعلم أن الجملة قد أنتهت 
            in_dialog = false; // بالتالي قم بالسماح للاعب أن ينتقل للجملة التالية
        }

        if (!in_dialog && Input.GetButtonDown("Fire1")) // إذا لم يكن الاعب عقب عرض نص و ضغط على الزر
        {
            next_sentance(); // قم بعرض الجملة التالية
            in_dialog = true; // و قم بمنع الاعب مرة أخرى من أن يتخطى الجلملة لحين إنتهائها 
        }

        if (in_dialog == true) // إذا كان الاعب عقب عرض نص أي لم ينتهي عرض الجملة
        {
            press_visual.SetActive(false); // قم بإيقاف عرض الزر المعبر عن إمكانية تخطي الجملة
        }
        else // إذا كان خارج جملة معينة أي أن عرض الجملة قد إنتهى
        {
            press_visual.SetActive(true); // قم بتفعيل الزر المعبر عن إمكانية تخطي الجملة
        }
    }


    public void next_sentance() // فنكشين الجملة التالية
    {
        if (index < sentences.Length - 1) 
        {
            in_dialog = true; // تعريف النظام أننا نعرض النص 
            index++; // عرض الجملة التالية من الدايالوغ إرراي
            arabicfix.fixedText = ""; // إفراغ محتوى النص 
            StartCoroutine(Type()); // بدأ كوروتين الكتابة
        }
        else
        {
            in_dialog = true;
            arabicfix.fixedText = "";
            press_visual.SetActive(false);
        }
    }

    public void open_dialogue() // فنكشين بدء النظام الذي يمكنك مناداته من أي سكريبت آخر لبدء عرض الجمل ( طبعا بعد تعريف سكريبت دايالوغ ماناجير) ا
    {
        in_dialog = true;
        dialogue_animator.SetBool("opening", true);
        StartCoroutine(Type());
    }
    public void end_dialogue() // الفنكشين المسؤولة عن إنهاء الدايالوغ 
    {
        dialogue_animator.SetBool("opening", false);
        StopCoroutine(Type());
    }
    public void disable_dialogue_box() //الفنكشين التي تعطل مستطيل النص هذه الفنكشين تنادى بداخل الأنيميشن 
    {
        gameObject.SetActive(false);
    }
}

