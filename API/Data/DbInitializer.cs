using API.Entities;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CourseContext context)
        {
            InitializeLessons(context);
            InitializeKeywords(context);
            InitializeLessonKeywords(context);
            InitializePreviousLessons(context);
        }
        public static void InitializeLessons(CourseContext context)
        {
            if (context.Lessons.Any()) return;
            var lessons = new List<Lesson>
            {
                new Lesson
                {
                    Title = "Види чисел, дроби, НСД, НСК, порівняння дробів",
                    Description = "На уроці ви дізнаєтеся які бувають види чисел (натуральні, цілі, ірраціональні...) та дробів (правильні, неправильні, десяткові, мішані). Також навчитеся знаходити НСД і НСК, перетворювати дроби з одного виду в інший та порівнювати їх.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 1,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Десяткові дроби, дії з ними, модуль",
                    Description = "На уроці ви дізнаєтеся як виконувати додавання, віднімання, множення та ділення десяткових дробів і навчитеся знаходити модуль числа.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 2,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Відношення, пропорція, відсотки, банк",
                    Description = "На уроці ви дізнаєтеся що таке відсотки та як знайти відсоток від числа. Також навчитеся розв'язувати задачі пов'язані з депозитами в банку.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 3,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Вступ в геометрію: фігури на площині, кути",
                    Description = "На уроці ви отримаєте базові знання з геометрії: які бувають фігури на площині та кути.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 4,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Степінь, лінійні рівняння",
                    Description = "На уроці ви дізнаєтеся що таке степінь числа, базові дії зі степенями. Також навчитеся розв'язувати ліінйні рівняння, що є основої для всіх інших видів рівнянь.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 5,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Формули скороченого множення, квадратні рівняння",
                    Description = "На уроці ви дізнаєтеся які є формули скороченого множення та їх застосування, а також як розв'язувати квадратні рівняння за допомогою дискримінанта або теореми Вієта.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 6,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Буквенні вирази, ОДЗ, властивості дробів",
                    Description = "На уроці ви дізнаєтеся що таке область допустимих значень виразу (ОДЗ) та деякі властивості не тільки числових, а й буквенних дробів.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 7,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Квадратний корінь, корінь n-ого степеня",
                    Description = "На уроці ви дізнаєтеся як знаходити квадратний корінь і n-ого степеня та які їхні властивості.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 8,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Трикутник. 3 ознаки рівності трик., 3 ознаки подібності трик., формула Герона",
                    Description = "На уроці ви дізнаєтеся всі базові складові трикутника, такі як середня лінія, висота, медіана, бісектриса та які властивості вони мають. Також буде вивчите по 3 ознаки рівності і подібності трикутників та формулу Герона для знаходження площі трикутника за 3-ма сторонами.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 9,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Прямокутний трикутник, теорема Піфагора, sin, cos, tg, ctg, похила",
                    Description = "На уроці ви глибше зануритеся в тему трикутників та дізнаєтесь набагато більше про один з їх видів - прямокутний трикутник. Вивчите теорему Піфагора - одну з найважливіших теорем в геометрії. А також отримаєте перше уявлення про такі тригонометричні функції, як синус, косинус, тангенс і котангенс. Також буде розглянуте поняття похилої та її проекції.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 10,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Рівнобедрений трикутник",
                    Description = "На уроці ви більше вивчите про другий вид трикутників - рівнобедрений.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 11,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Чотирикутники: паралелограм, ромб, прямокутник, квадрат, трапеція",
                    Description = "На уроці ви дізнаєтеся які бувають види 4-кутників та їхні особливості.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 12,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Периметр і площа фігур",
                    Description = "На уроці будуть формули (та пояснення звідки вони отримуються) периметрів і площ трикутників та різних видів чотирикутників.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 13,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Коло та круг",
                    Description = "На уроці будуть поясненя основні складові кола та круга, їх основні властивості та формули довжини лінії кола/площі круга.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 14,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Вписане і описане кола, теорема косинусів і синусів",
                    Description = "На уроці ви дізнаєтеся все необхідне про вписані і описані кола: де знаходиться центр, як знайти їхній радіус, яким критеріям має відповідати 4-кутник, щоб в нього можна було вписати або навколо нього описати коло..",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 15,
                    Importance = 0,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Многокутники",
                    Description = "На уроці ви дізнаєтеся основні формули для правильних многокутників: як знайти внутрішній кут, вписане/описане коло.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 16,
                    Importance = 1,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Складніші рівняння, теорема Безу",
                    Description = "На уроці ви будуть розглянуті способи розв'язання степеневих рівнянь та теорема Безу.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 17,
                    Importance = 2,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Складніші рівняння, част 2",
                    Description = "На уроці будуть розглянуті способи розв'язання рівнянь з модулем.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 18,
                    Importance = 1,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Показниковий вираз, логарифм",
                    Description = "На уроці ви дізнаєтеся що таке показниковий вираз, логарифм, чому вони пов'язані та основні дії з ними.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 19,
                    Importance = 1,
                    isCompleted = false,
                },
                new Lesson
                {
                    Title = "Показникові рівняння",
                    Description = "На уроці ви навчитеся розв'язувати основні види показникових рівнянь.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 20,
                    Importance = 1,
                    isCompleted = false,
                },
            };

            foreach (var lesson in lessons)
            {
                context.Lessons.Add(lesson);
            }

            context.SaveChanges();
        }

        public static void InitializeKeywords(CourseContext context)
        {
            if (context.Keywords.Any()) return;
            var words = new List<string> { "дроби", "НСД/НСК", "модуль", "відсотки", "пропорції", "кут", "степінь", "рівняння", "лінійні рівняння",
                "формули скороченого множення", "квадратне рівняння", "дискримінант", "теорема Вієта", "ОДЗ (область допустимих значень)",
                "інтервал/проміжок", "система/об'єднання", "корені", "трикутник", "середня лінія", "медіана", "висота", "бісектриса", "3 ознаки рівності △",
                "3 ознаки подібності △", "формула Герона", "площа", "теорема Піфагора", "тригонометричні функції (sin, cos, tg, ctg)", "похила і проекція",
                "чотирикутник", "паралелограм", "ромб", "прямокутник", "квадрат", "трапеція", "периметр", "коло", "круг", "вписаний/центральний кут",
                "вписане/описане коло", "теорема косинусів", "теорема синусів", "многокутник", "зовнішній кут", "біквадратні рівняння", "показниковий вираз",
                "логарифм", "показникові рівняння", "логарифмічні рівняння",
                };

            var keywords = new List<Keyword>();

            foreach (var word in words)
            {
                keywords.Add(
                    new Keyword
                    {
                        Word = word,
                    }
                );
            }

            foreach (var keyword in keywords)
            {
                context.Keywords.Add(keyword);
            }

            context.SaveChanges();
        }

        public static void InitializeLessonKeywords (CourseContext context)
        {
            if (context.LessonKeywords.Any()) return;

            var lessonsToKeywords = new List<LessonToKeywords> {
                new LessonToKeywords
                {
                    LessonId = 1,
                    KeywordIds = new List<int> {1, 2},
                },
                new LessonToKeywords
                {
                    LessonId = 2,
                    KeywordIds = new List<int> {1, 3},
                },
                new LessonToKeywords
                {
                    LessonId = 3,
                    KeywordIds = new List<int> {4, 5},
                },
                new LessonToKeywords
                {
                    LessonId = 4,
                    KeywordIds = new List<int> {6},
                },
                new LessonToKeywords
                {
                    LessonId = 5,
                    KeywordIds = new List<int> {7, 8, 9},
                },
                new LessonToKeywords
                {
                    LessonId = 6,
                    KeywordIds = new List<int> {10, 11, 12, 13, 8},
                },
                new LessonToKeywords
                {
                    LessonId = 7,
                    KeywordIds = new List<int> {14, 15, 16},
                },
                new LessonToKeywords
                {
                    LessonId = 8,
                    KeywordIds = new List<int> {17},
                },
                new LessonToKeywords
                {
                    LessonId = 9,
                    KeywordIds = new List<int> {18, 19, 20, 21, 22, 23, 24, 25, 26},
                },
                new LessonToKeywords
                {
                    LessonId = 10,
                    KeywordIds = new List<int> {18, 27, 28, 29},
                },
                new LessonToKeywords
                {
                    LessonId = 11,
                    KeywordIds = new List<int> {18},
                },
                new LessonToKeywords
                {
                    LessonId = 12,
                    KeywordIds = new List<int> {30, 31, 32, 33, 34, 35},
                },
                new LessonToKeywords
                {
                    LessonId = 13,
                    KeywordIds = new List<int> {36, 26},
                },
                new LessonToKeywords
                {
                    LessonId = 14,
                    KeywordIds = new List<int> {37, 38, 39},
                },
                new LessonToKeywords
                {
                    LessonId = 15,
                    KeywordIds = new List<int> {40, 41, 42, 36, 26},
                },
                new LessonToKeywords
                {
                    LessonId = 16,
                    KeywordIds = new List<int> {43, 37, 6, 44, 40},
                },
                new LessonToKeywords
                {
                    LessonId = 17,
                    KeywordIds = new List<int> {8, 11, 45},
                },
                new LessonToKeywords
                {
                    LessonId = 18,
                    KeywordIds = new List<int> {8, 3, 16},
                },
                new LessonToKeywords
                {
                    LessonId = 19,
                    KeywordIds = new List<int> {46, 47},
                },
                new LessonToKeywords
                {
                    LessonId = 20,
                    KeywordIds = new List<int> {8, 46, 48},
                },
            };

            foreach (var lessonToKeywords in lessonsToKeywords)
            {
                foreach (var keywordId in lessonToKeywords.KeywordIds)
                {
                    context.LessonKeywords.Add(
                        new LessonKeyword
                        {
                            LessonId = lessonToKeywords.LessonId,
                            KeywordId = keywordId,
                        }
                    );
                }
            }

            context.SaveChanges();
        }

        public static void InitializePreviousLessons (CourseContext context)
        {
            if (context.PreviousLessons.Any()) return;

            var lessonsToPreviousLessons = new List<LessonToPreviousLessons> {
                new LessonToPreviousLessons
                {
                    LessonId = 2,
                    PreviousLessonIds = new List<int> {1},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 7,
                    PreviousLessonIds = new List<int> {6},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 10,
                    PreviousLessonIds = new List<int> {9},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 11,
                    PreviousLessonIds = new List<int> {9},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 13,
                    PreviousLessonIds = new List<int> {9, 12},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 15,
                    PreviousLessonIds = new List<int> {12, 13},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 17,
                    PreviousLessonIds = new List<int> {6},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 18,
                    PreviousLessonIds = new List<int> {2, 7},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 19,
                    PreviousLessonIds = new List<int> {5},
                },
                new LessonToPreviousLessons
                {
                    LessonId = 20,
                    PreviousLessonIds = new List<int> {19},
                },
            };

            foreach (var lessonToPreviousLessons in lessonsToPreviousLessons)
            {
                foreach (var previousLessonId in lessonToPreviousLessons.PreviousLessonIds)
                {
                    context.PreviousLessons.Add(
                        new LessonPreviousLesson
                        {
                            LessonId = lessonToPreviousLessons.LessonId,
                            PreviousLessonId = previousLessonId,
                        }
                    );
                }
            }

            context.SaveChanges();
        }
    }

    // helper classes to make seeding data into join tables easier
    class LessonToKeywords
    {
        public int LessonId { get; set; }
        public List<int> KeywordIds { get; set; }

    }

    class LessonToPreviousLessons
    {
        public int LessonId { get; set; }
        public List<int> PreviousLessonIds { get; set; }

    }
}

