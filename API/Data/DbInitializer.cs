using System.Globalization;
using API.Entities;
using API.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(CourseContext context, UserManager<User> userManager)
        {
            await InitializeUsers(userManager);
            InitializeLessons(context);
            InitializeKeywords(context);
            InitializeLessonKeywords(context);
            InitializePreviousLessons(context);
            InitializeTests(context);
            InitializeCourses(context);
            InitializeSectionLessons(context);
            InitializeUserSections(context);
            InitializeUserLessons(context);
        }

        private static void InitializeUserSections(CourseContext context)
        {
            if (context.UserSections.Any()) return;

            var usersToSections = new List<UserToSections> {
                new() {
                    UserName = "bob",
                    SectionIds = Enumerable.Range(1, 3).ToList(),
                },
                new() {
                    UserName = "yaryna",
                    SectionIds = Enumerable.Range(1, 16).ToList(),
                }
            };

            foreach (var userToSections in usersToSections)
            {
                foreach (var sectionId in userToSections.SectionIds)
                {
                    var user = context.Users.FirstOrDefault(user => user.UserName == userToSections.UserName);
                    if (user != null)
                    {
                        context.UserSections.Add(
                            new UserSection
                            {
                                UserId = user.Id,
                                SectionId = sectionId,
                                isAvailable = true,
                            }
                        );
                    }
                }
            }

            context.SaveChanges();
        }

        private static void InitializeUserLessons(CourseContext context)
        {
            if (context.UserLessons.Any()) return;

            var usersToLessons = new List<UserToLessons> {
                new() {
                    UserName = "bob",
                    LessonsInfo = new List<UserLessonInfo> {
                        new() {
                            LessonId = 1,
                            TestScore = 100,
                        },
                        new() {
                            LessonId = 2,
                            TestScore = 100,
                        },
                        new() {
                            LessonId = 3,
                            TestScore = 90,
                        },
                        new() {
                            LessonId = 4,
                            TestScore = 50,
                        },
                        new() {
                            LessonId = 5,
                            TestScore = 85,
                        },
                    },
                },
                new() {
                    UserName = "yaryna",
                    LessonsInfo = new List<UserLessonInfo> {
                        new() {
                            LessonId = 1,
                            TestScore = 100,
                        },
                        new() {
                            LessonId = 2,
                            TestScore = 100,
                        },
                        new() {
                            LessonId = 3,
                            TestScore = 90,
                        },
                        new() {
                            LessonId = 4,
                            TestScore = 50,
                        },
                        new() {
                            LessonId = 5,
                            TestScore = 85,
                        },
                        new() {
                            LessonId = 9,
                            TestScore = 85,
                        },
                        new() {
                            LessonId = 10,
                            TestScore = 100,
                        },
                    },
                }
            };

            foreach (var userToLessons in usersToLessons)
            {
                foreach (var lessonInfo in userToLessons.LessonsInfo)
                {
                    var user = context.Users.FirstOrDefault(user => user.UserName == userToLessons.UserName);
                    if (user != null)
                    {
                        context.UserLessons.Add(
                            new UserLesson
                            {
                                UserId = user.Id,
                                LessonId = lessonInfo.LessonId,
                                IsTheoryCompleted = lessonInfo.IsTheoryCompleted,
                                IsPracticeCompleted = lessonInfo.IsPracticeCompleted,
                                TestScore = lessonInfo.TestScore,
                            }
                        );
                    }
                }
            }

            context.SaveChanges();
        }

        public static async Task InitializeUsers(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "bob",
                    Email = "bob@gmail.com"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");

                var admin = new User
                {
                    UserName = "yaryna",
                    Email = "plan.znoshnika@gmail.com"
                };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });
            }
        }

        public static void InitializeLessons(CourseContext context)
        {
            if (context.Lessons.Any()) return;
            var lessons = new List<Lesson>
            {
                new() {
                    Title = "Види чисел, дроби, НСД, НСК, порівняння дробів",
                    Description = "На уроці ви дізнаєтеся які бувають види чисел (натуральні, цілі, ірраціональні...) та дробів (правильні, неправильні, десяткові, мішані). Також навчитеся знаходити НСД і НСК, перетворювати дроби з одного виду в інший та порівнювати їх.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 1,
                    Importance = 0,
                },
                new() {
                    Title = "Десяткові дроби, дії з ними, модуль",
                    Description = "На уроці ви дізнаєтеся як виконувати додавання, віднімання, множення та ділення десяткових дробів і навчитеся знаходити модуль числа.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 2,
                    Importance = 0,
                },
                new() {
                    Title = "Відношення, пропорція, відсотки, банк",
                    Description = "На уроці ви дізнаєтеся що таке відсотки та як знайти відсоток від числа. Також навчитеся розв'язувати задачі пов'язані з депозитами в банку.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 3,
                    Importance = 0,
                },
                new() {
                    Title = "Вступ в геометрію: фігури на площині, кути",
                    Description = "На уроці ви отримаєте базові знання з геометрії: які бувають фігури на площині та кути.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 4,
                    Importance = 0,
                },
                new() {
                    Title = "Степінь, лінійні рівняння",
                    Description = "На уроці ви дізнаєтеся що таке степінь числа, базові дії зі степенями. Також навчитеся розв'язувати ліінйні рівняння, що є основої для всіх інших видів рівнянь.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 5,
                    Importance = 0,
                },
                new() {
                    Title = "Формули скороченого множення, квадратні рівняння",
                    Description = "На уроці ви дізнаєтеся які є формули скороченого множення та їх застосування, а також як розв'язувати квадратні рівняння за допомогою дискримінанта або теореми Вієта.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 6,
                    Importance = 0,
                },
                new() {
                    Title = "Буквенні вирази, ОДЗ, властивості дробів",
                    Description = "На уроці ви дізнаєтеся що таке область допустимих значень виразу (ОДЗ) та деякі властивості не тільки числових, а й буквенних дробів.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 7,
                    Importance = 0,
                },
                new() {
                    Title = "Квадратний корінь, корінь n-ого степеня",
                    Description = "На уроці ви дізнаєтеся як знаходити квадратний корінь і n-ого степеня та які їхні властивості.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 8,
                    Importance = 0,
                },
                new() {
                    Title = "Трикутник. 3 ознаки рівності трик., 3 ознаки подібності трик., формула Герона",
                    Description = "На уроці ви дізнаєтеся всі базові складові трикутника, такі як середня лінія, висота, медіана, бісектриса та які властивості вони мають. Також буде вивчите по 3 ознаки рівності і подібності трикутників та формулу Герона для знаходження площі трикутника за 3-ма сторонами.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 9,
                    Importance = 0,
                },
                new() {
                    Title = "Прямокутний трикутник, теорема Піфагора, sin, cos, tg, ctg, похила",
                    Description = "На уроці ви глибше зануритеся в тему трикутників та дізнаєтесь набагато більше про один з їх видів - прямокутний трикутник. Вивчите теорему Піфагора - одну з найважливіших теорем в геометрії. А також отримаєте перше уявлення про такі тригонометричні функції, як синус, косинус, тангенс і котангенс. Також буде розглянуте поняття похилої та її проекції.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 10,
                    Importance = 0,
                },
                new() {
                    Title = "Рівнобедрений трикутник",
                    Description = "На уроці ви більше вивчите про другий вид трикутників - рівнобедрений.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 11,
                    Importance = 0,
                },
                new() {
                    Title = "Чотирикутники: паралелограм, ромб, прямокутник, квадрат, трапеція",
                    Description = "На уроці ви дізнаєтеся які бувають види 4-кутників та їхні особливості.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 12,
                    Importance = 0,
                },
                new() {
                    Title = "Периметр і площа фігур",
                    Description = "На уроці будуть формули (та пояснення звідки вони отримуються) периметрів і площ трикутників та різних видів чотирикутників.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 13,
                    Importance = 0,
                },
                new() {
                    Title = "Коло та круг",
                    Description = "На уроці будуть поясненя основні складові кола та круга, їх основні властивості та формули довжини лінії кола/площі круга.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 14,
                    Importance = 0,
                },
                new() {
                    Title = "Вписане і описане кола, теорема косинусів і синусів",
                    Description = "На уроці ви дізнаєтеся все необхідне про вписані і описані кола: де знаходиться центр, як знайти їхній радіус, яким критеріям має відповідати 4-кутник, щоб в нього можна було вписати або навколо нього описати коло..",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 15,
                    Importance = 0,
                },
                new() {
                    Title = "Многокутники",
                    Description = "На уроці ви дізнаєтеся основні формули для правильних многокутників: як знайти внутрішній кут, вписане/описане коло.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 16,
                    Importance = 1,
                },
                new() {
                    Title = "Складніші рівняння, теорема Безу",
                    Description = "На уроці ви будуть розглянуті способи розв'язання степеневих рівнянь та теорема Безу.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 17,
                    Importance = 2,
                },
                new() {
                    Title = "Складніші рівняння, част 2",
                    Description = "На уроці будуть розглянуті способи розв'язання рівнянь з модулем.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 18,
                    Importance = 1,
                },
                new() {
                    Title = "Показниковий вираз, логарифм",
                    Description = "На уроці ви дізнаєтеся що таке показниковий вираз, логарифм, чому вони пов'язані та основні дії з ними.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 19,
                    Importance = 1,
                },
                new() {
                    Title = "Показникові рівняння",
                    Description = "На уроці ви навчитеся розв'язувати основні види показникових рівнянь.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 20,
                    Importance = 1,
                },
                new() {
                    Title = "Логарифмічні рівняння",
                    Description = "На уроці ви дізнаєтеся що таке логарифмічні рівняння.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 21,
                    Importance = 1,
                },
                new() {
                    Title = "Дробово-раціональні рівняння",
                    Description = "На уроці ви дізнаєтеся що таке дробово-раціональні рівняння.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 24,
                    Importance = 0,
                },
                new() {
                    Title = "Координати на площині. Рівняння фігур",
                    Description = "На уроці ви дізнаєтеся що таке координати на площині, рівняння фігур.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 25,
                    Importance = 0,
                },
                new() {
                    Title = "Графік функції гіперболи, параболи, рівняння кола",
                    Description = "На уроці ви дізнаєтеся що таке графік функції гіперболи, параболи, рівняння кола.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 27,
                    Importance = 1,
                },
                new() {
                    Title = "Графіки показникової, логарифмічної та тригонометричної функцій",
                    Description = "На уроці ви дізнаєтеся що таке графіки показникової, логарифмічної та тригонометричної функцій.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 28,
                    Importance = 2,
                },
                new() {
                    Title = "Дії з нерівностями, квадратні нерівності",
                    Description = "На уроці ви дізнаєтеся що таке дії з нерівностями, квадратні нерівності.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 29,
                    Importance = 0,
                },
                new() {
                    Title = "Метод інтервалів для нерівностей",
                    Description = "На уроці ви дізнаєтеся що таке метод інтервалів для нерівностей.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 30,
                    Importance = 2,
                },
                new() {
                    Title = "Нерівності з модулем",
                    Description = "На уроці ви дізнаєтеся що таке нерівності з модулем.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 31,
                    Importance = 1,
                },
                new() {
                    Title = "Ірраціональні рівняння",
                    Description = "На уроці ви дізнаєтеся що таке ірраціональні рівняння.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 32,
                    Importance = 1,
                },
                new() {
                    Title = "Ірраціональні нерівності",
                    Description = "На уроці ви дізнаєтеся що таке ірраціональні нерівності.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 33,
                    Importance = 2,
                },
                new() {
                    Title = "Показникові і логарифмічні нерівності",
                    Description = "На уроці ви дізнаєтеся що таке метод показникові і логарифмічні нерівності.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 36,
                    Importance = 2,
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

        public static void InitializeLessonKeywords(CourseContext context)
        {
            if (context.LessonKeywords.Any()) return;

            var lessonsToKeywords = new List<LessonToKeywords> {
                new() {
                    LessonId = 1,
                    KeywordIds = new List<int> {1, 2},
                },
                new() {
                    LessonId = 2,
                    KeywordIds = new List<int> {1, 3},
                },
                new() {
                    LessonId = 3,
                    KeywordIds = new List<int> {4, 5},
                },
                new() {
                    LessonId = 4,
                    KeywordIds = new List<int> {6},
                },
                new() {
                    LessonId = 5,
                    KeywordIds = new List<int> {7, 8, 9},
                },
                new() {
                    LessonId = 6,
                    KeywordIds = new List<int> {10, 11, 12, 13, 8},
                },
                new() {
                    LessonId = 7,
                    KeywordIds = new List<int> {14, 15, 16},
                },
                new() {
                    LessonId = 8,
                    KeywordIds = new List<int> {17},
                },
                new() {
                    LessonId = 9,
                    KeywordIds = new List<int> {18, 19, 20, 21, 22, 23, 24, 25, 26},
                },
                new() {
                    LessonId = 10,
                    KeywordIds = new List<int> {18, 27, 28, 29},
                },
                new() {
                    LessonId = 11,
                    KeywordIds = new List<int> {18},
                },
                new() {
                    LessonId = 12,
                    KeywordIds = new List<int> {30, 31, 32, 33, 34, 35},
                },
                new() {
                    LessonId = 13,
                    KeywordIds = new List<int> {36, 26},
                },
                new() {
                    LessonId = 14,
                    KeywordIds = new List<int> {37, 38, 39},
                },
                new() {
                    LessonId = 15,
                    KeywordIds = new List<int> {40, 41, 42, 36, 26},
                },
                new() {
                    LessonId = 16,
                    KeywordIds = new List<int> {43, 37, 6, 44, 40},
                },
                new() {
                    LessonId = 17,
                    KeywordIds = new List<int> {8, 11, 45},
                },
                new() {
                    LessonId = 18,
                    KeywordIds = new List<int> {8, 3, 16},
                },
                new() {
                    LessonId = 19,
                    KeywordIds = new List<int> {46, 47},
                },
                new() {
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

        public static void InitializePreviousLessons(CourseContext context)
        {
            if (context.PreviousLessons.Any()) return;

            var lessonsToPreviousLessons = new List<LessonToPreviousLessons> {
                new() {
                    LessonId = 2,
                    PreviousLessonIds = new List<int> {1},
                },
                new() {
                    LessonId = 7,
                    PreviousLessonIds = new List<int> {6},
                },
                new() {
                    LessonId = 10,
                    PreviousLessonIds = new List<int> {9},
                },
                new() {
                    LessonId = 11,
                    PreviousLessonIds = new List<int> {9},
                },
                new() {
                    LessonId = 13,
                    PreviousLessonIds = new List<int> {9, 12},
                },
                new() {
                    LessonId = 15,
                    PreviousLessonIds = new List<int> {12, 13},
                },
                new() {
                    LessonId = 17,
                    PreviousLessonIds = new List<int> {6},
                },
                new() {
                    LessonId = 18,
                    PreviousLessonIds = new List<int> {2, 7},
                },
                new() {
                    LessonId = 19,
                    PreviousLessonIds = new List<int> {5},
                },
                new() {
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

        public static void InitializeTests(CourseContext context)
        {
            if (context.Tests.Any()) return;
            if (context.Options.Any()) return;

            var lessonTests = new List<LessonTests> {
                new() {
                    LessonId = 1,
                    TestOptions = new List<TestOptions>
                    {
                        new() {
                            Question = "Вкажіть натуральне число",
                            Options = new List<Option>
                            {
                                new() { Text = "4,1", isAnswer = false },
                                new() { Text = "0", isAnswer = false },
                                new() { Text = "-3", isAnswer = false },
                                new() { Text = "17", isAnswer = true },
                                new() { Text = "1,8", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "При діленні числа 14 на 6 остача дорівнює",
                            Options = new()
                            {
                                new() { Text = "0", isAnswer = false },
                                new() { Text = "1", isAnswer = false },
                                new() { Text = "2", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Скоротіть дріб 66/24 до нескоротного",
                            Options = new()
                            {
                                new() { Text = "33/12", isAnswer = false },
                                new() { Text = "11/3", isAnswer = false },
                                new() { Text = "11/4", isAnswer = true },
                                new() { Text = "22/8", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Перетворіть мішаний дріб  2 1/2  у неправильний",
                            Options = new()
                            {
                                new() { Text = "1/2", isAnswer = false },
                                new() { Text = "3/5", isAnswer = false },
                                new() { Text = "3/2", isAnswer = false },
                                new() { Text = "5/3", isAnswer = false },
                                new() { Text = "5/2", isAnswer = true },
                            },
                        },
                        new()
                        {
                            Question = "НСД(42; 63) =",
                            Options = new()
                            {
                                new() { Text = "126", isAnswer = false },
                                new() { Text = "7", isAnswer = false },
                                new() { Text = "42", isAnswer = false },
                                new() { Text = "21", isAnswer = true },
                                new() { Text = "3", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "(4/5) * (7/4) =",
                            Options = new()
                            {
                                new() { Text = "5/7", isAnswer = false },
                                new() { Text = "35/16", isAnswer = false },
                                new() { Text = "20/28", isAnswer = false },
                                new() { Text = "7/5", isAnswer = true },
                            },
                        },
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    },
                },
                new()
                {
                    LessonId = 2,
                    TestOptions = new()
                    {
                        new()
                        {
                            Question = "7412/1000 =",
                            Options = new()
                            {
                                new() { Text = "7412000", isAnswer = false },
                                new() { Text = "74,12", isAnswer = false },
                                new() { Text = "7,412", isAnswer = true },
                                new() { Text = "0,7412", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "1,61 + 2,49",
                            Options = new()
                            {
                                new() { Text = "4,1", isAnswer = true },
                                new() { Text = "4", isAnswer = false },
                                new() { Text = "3,1", isAnswer = false },
                                new() { Text = "3", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "7×1,2=",
                            Options = new()
                            {
                                new() { Text = "8,4", isAnswer = true },
                                new() { Text = "8,2", isAnswer = false },
                                new() { Text = "0,74", isAnswer = false },
                                new() { Text = "9,2", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "21:9=",
                            Options = new()
                            {
                                new() { Text = "189", isAnswer = false },
                                new() { Text = "2,(3)", isAnswer = true },
                                new() { Text = "3,(1)", isAnswer = false },
                                new() { Text = "3", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Переведіть періодичний дріб 1,(6) у неправильний",
                            Options = new()
                            {
                                new() { Text = "5/3", isAnswer = true },
                                new() { Text = "2/5", isAnswer = false },
                                new() { Text = "1/6", isAnswer = false },
                                new() { Text = "2/3", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Обчислити 3 + (20 - 10 × 0,2) : 6 × 2",
                            Options = new()
                            {
                                new() { Text = "6", isAnswer = false },
                                new() { Text = "4,5", isAnswer = false },
                                new() { Text = "9", isAnswer = true },
                                new() { Text = "3,(6)", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "(-0,3) × (-5)",
                            Options = new()
                            {
                                new() { Text = "1,5", isAnswer = true },
                                new() { Text = "-15", isAnswer = false },
                                new() { Text = "-1,5", isAnswer = false },
                                new() { Text = "15", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Розкрити дужки -c (b - a)",
                            Options = new()
                            {
                                new() { Text = "- cb - ca", isAnswer = false },
                                new() { Text = "ac - bc", isAnswer = true },
                                new() { Text = "bc + ac", isAnswer = false },
                                new() { Text = "bc - ab", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Обчисліть вираз |7/14 - 0,6| + 0,2",
                            Options = new()
                            {
                                new() { Text = "0", isAnswer = false },
                                new() { Text = "0,1", isAnswer = false },
                                new() { Text = "0,2", isAnswer = false },
                                new() { Text = "0,3", isAnswer = true },
                            },
                        },
                        new()
                        {
                            Question = "Округліть число 1234,567 до десятків",
                            Options = new()
                            {
                                new() { Text = "1235", isAnswer = false },
                                new() { Text = "1234,57", isAnswer = false },
                                new() { Text = "1230", isAnswer = true },
                                new() { Text = "1234,6", isAnswer = false },
                            },
                        },
                    },
                },
                new()
                {
                    LessonId = 3,
                    TestOptions = new()
                    {
                        new()
                        {
                            Question = "Виразіть число 2/5 у відсотках",
                            Options = new()
                            {
                                new() { Text = "10%", isAnswer = false },
                                new() { Text = "20%", isAnswer = false },
                                new() { Text = "30%", isAnswer = false },
                                new() { Text = "40%", isAnswer = true },
                            },
                        },
                        new()
                        {
                            Question = "Обчисліть 25% від 140",
                            Options = new()
                            {
                                new() { Text = "30", isAnswer = false },
                                new() { Text = "35", isAnswer = true },
                                new() { Text = "70", isAnswer = false },
                                new() { Text = "75", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Ви поповнювали рахунок через термінал, який бере 5% комісії. У результаті комісія становила 3 грн, а решта грошей прийшла на рахунок💰 На скільки грн поповнився ваш рахунок?",
                            Options = new()
                            {
                                new() { Text = "60 грн", isAnswer = false },
                                new() { Text = "57 грн", isAnswer = true },
                                new() { Text = "15 грн", isAnswer = false },
                                new() { Text = "12 грн", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Ви поклали 3000 грн на депозит під 7% річних і щороку отримуєте однакову суму на карту. Який прибуток ви отримаєте через 5 років?",
                            Options = new()
                            {
                                new() { Text = "3210 грн", isAnswer = false },
                                new() { Text = "4050 грн", isAnswer = false },
                                new() { Text = "210 грн", isAnswer = false },
                                new() { Text = "1050 грн", isAnswer = true },
                            },
                        },
                        new()
                        {
                            Question = "Товар зі знижкою 15% коштує 425 грн. Яка ціна без знижки? (відповідь округліть до десятків)",
                            Options = new()
                            {
                                new() { Text = "490 грн", isAnswer = false },
                                new() { Text = "500 грн", isAnswer = true },
                                new() { Text = "360 грн", isAnswer = false },
                                new() { Text = "450 грн", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Відстань між школами дорівнює 6 км. Скільки це см на карті, якщо її масштаб 1:30 000?",
                            Options = new()
                            {
                                new() { Text = "20 см", isAnswer = true },
                                new() { Text = "18 см", isAnswer = false },
                                new() { Text = "2 см", isAnswer = false },
                                new() { Text = "1,8 см", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Обчисліть 65% від 80",
                            Options = new()
                            {
                                new() { Text = "52", isAnswer = true },
                                new() { Text = "65", isAnswer = false },
                                new() { Text = "48", isAnswer = false },
                                new() { Text = "50", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "Число 42 складає 70% від числа a. Визначте число a",
                            Options = new()
                            {
                                new() { Text = "65", isAnswer = false },
                                new() { Text = "32", isAnswer = false },
                                new() { Text = "81", isAnswer = false },
                                new() { Text = "60", isAnswer = true },
                                new() { Text = "29", isAnswer = false },
                            },
                        },
                        new()
                        {
                            Question = "В роздріб одна шоколадка коштує 30 грн. Але на оптову закупівлю (від 40 штук) діє знижка 25%. Скільки коштуватиме партія з 50-ти таких шоколадок?",
                            Options = new()
                            {
                                new() { Text = "1750 грн", isAnswer = false },
                                new() { Text = "1200 грн", isAnswer = false },
                                new() { Text = "900 грн", isAnswer = false },
                                new() { Text = "1350 грн", isAnswer = false },
                                new() { Text = "1125 грн", isAnswer = true },
                            },
                        },
                        new()
                        {
                            Question = "Працівник отримав підвищення. У результаті його зарплата зросла на 10% і тепер становить 16 500 грн/міс. Яку зарплату він отримував до цього підвищення?",
                            Options = new()
                            {
                                new() { Text = "15 000 грн/міс", isAnswer = true },
                                new() { Text = "14 500 грн/міс", isAnswer = false },
                                new() { Text = "13 750 грн/міс", isAnswer = false },
                                new() { Text = "14 850 грн/міс", isAnswer = false },
                                new() { Text = "16 000 грн/міс", isAnswer = false },
                            },
                        },
                    },
                },
                new()
                {
                    LessonId = 4,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Яка фігура має лише одну пару паралельних сторін?",
                            Options = new()
                            {
                                new() { Text = "Квадрат", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = true },
                                new() { Text = "Прямокутник", isAnswer = false },
                                new() { Text = "Ромб", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Який із цих кутів називається прямим?",
                            Options = new()
                            {
                                new() { Text = "45 градусів", isAnswer = false },
                                new() { Text = "90 градусів", isAnswer = true },
                                new() { Text = "120 градусів", isAnswer = false },
                                new() { Text = "180 градусів", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки градусів має тупий кут?",
                            Options = new()
                            {
                                new() { Text = "Менше 90 градусів", isAnswer = false },
                                new() { Text = "Більше 90, але менше 180 градусів", isAnswer = true },
                                new() { Text = "Точно 180 градусів", isAnswer = false },
                                new() { Text = "Рівно 90 градусів", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка фігура має всі сторони рівні?",
                            Options = new()
                            {
                                new() { Text = "Трапеція", isAnswer = false },
                                new() { Text = "Квадрат", isAnswer = true },
                                new() { Text = "Прямокутник", isAnswer = false },
                                new() { Text = "Паралелограм", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має правильний п'ятикутник?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = false },
                                new() { Text = "5", isAnswer = true },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 5,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2^3?",
                            Options = new()
                            {
                                new() { Text = "6", isAnswer = false },
                                new() { Text = "8", isAnswer = true },
                                new() { Text = "9", isAnswer = false },
                                new() { Text = "12", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яке з цих рівнянь є лінійним?",
                            Options = new()
                            {
                                new() { Text = "x^2 + 3x + 2 = 0", isAnswer = false },
                                new() { Text = "3x - 5 = 10", isAnswer = true },
                                new() { Text = "2x^2 + 5x = 7", isAnswer = false },
                                new() { Text = "4x + 7x^2 = 12", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яке значення степеня має 5^0?",
                            Options = new()
                            {
                                new() { Text = "0", isAnswer = false },
                                new() { Text = "1", isAnswer = true },
                                new() { Text = "-1", isAnswer = false },
                                new() { Text = "5", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яке рівняння є прикладом лінійного рівняння з двома змінними?",
                            Options = new()
                            {
                                new() { Text = "3x + 4y = 12", isAnswer = true },
                                new() { Text = "x^2 + y^2 = 25", isAnswer = false },
                                new() { Text = "xy = 10", isAnswer = false },
                                new() { Text = "x^3 + y = 9", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Чому дорівнює 10^2?",
                            Options = new()
                            {
                                new() { Text = "10", isAnswer = false },
                                new() { Text = "100", isAnswer = true },
                                new() { Text = "20", isAnswer = false },
                                new() { Text = "200", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 6,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Яка формула є формулою квадрату суми?",
                            Options = new()
                            {
                                new() { Text = "(a + b)^2 = a^2 + 2ab + b^2", isAnswer = true },
                                new() { Text = "(a - b)^2 = a^2 - 2ab + b^2", isAnswer = false },
                                new() { Text = "a^2 - b^2 = (a - b)(a + b)", isAnswer = false },
                                new() { Text = "(a + b)^2 = a^2 - 2ab + b^2", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається дискримінант у квадратному рівнянні?",
                            Options = new()
                            {
                                new() { Text = "D = b^2 - 4ac", isAnswer = true },
                                new() { Text = "D = a^2 - b^2", isAnswer = false },
                                new() { Text = "D = b^2 + 4ac", isAnswer = false },
                                new() { Text = "D = a^2 + b^2", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 - 4 = 0?",
                            Options = new()
                            {
                                new() { Text = "x = ±2", isAnswer = true },
                                new() { Text = "x = 4", isAnswer = false },
                                new() { Text = "x = 0", isAnswer = false },
                                new() { Text = "x = ±4", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка з формул є формулою різниці квадратів?",
                            Options = new()
                            {
                                new() { Text = "a^2 - b^2 = (a - b)(a + b)", isAnswer = true },
                                new() { Text = "(a + b)^2 = a^2 + 2ab + b^2", isAnswer = false },
                                new() { Text = "(a - b)^2 = a^2 - 2ab + b^2", isAnswer = false },
                                new() { Text = "a^2 + b^2 = (a + b)(a - b)", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки коренів може мати квадратне рівняння, якщо дискримінант менший за нуль?",
                            Options = new()
                            {
                                new() { Text = "0", isAnswer = true },
                                new() { Text = "1", isAnswer = false },
                                new() { Text = "2", isAnswer = false },
                                new() { Text = "Безкінечна кількість", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 7,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Що таке область допустимих значень виразу (ОДЗ)?",
                            Options = new()
                            {
                                new() { Text = "Значення, при яких дріб дорівнює нулю", isAnswer = false },
                                new() { Text = "Значення змінної, при яких вираз має сенс", isAnswer = true },
                                new() { Text = "Всі можливі значення змінної", isAnswer = false },
                                new() { Text = "Значення, при яких вираз дорівнює одиниці", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яке значення не може бути в знаменнику дробу?",
                            Options = new()
                            {
                                new() { Text = "Нуль", isAnswer = true },
                                new() { Text = "Одиниця", isAnswer = false },
                                new() { Text = "Дробове число", isAnswer = false },
                                new() { Text = "Негативне число", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як можна спростити дріб 3x / 3?",
                            Options = new()
                            {
                                new() { Text = "x", isAnswer = true },
                                new() { Text = "3x", isAnswer = false },
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "1/3x", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка область допустимих значень для виразу 1/(x-2)?",
                            Options = new()
                            {
                                new() { Text = "x ≠ 2", isAnswer = true },
                                new() { Text = "x = 2", isAnswer = false },
                                new() { Text = "x > 2", isAnswer = false },
                                new() { Text = "x < 2", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як спростити вираз (a * b) / a?",
                            Options = new()
                            {
                                new() { Text = "b", isAnswer = true },
                                new() { Text = "a * b", isAnswer = false },
                                new() { Text = "a", isAnswer = false },
                                new() { Text = "1/a", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 8,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює √25?",
                            Options = new()
                            {
                                new() { Text = "5", isAnswer = true },
                                new() { Text = "10", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                                new() { Text = "2,5", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Чому дорівнює ³√8?",
                            Options = new()
                            {
                                new() { Text = "2", isAnswer = true },
                                new() { Text = "4", isAnswer = false },
                                new() { Text = "8", isAnswer = false },
                                new() { Text = "3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яке з тверджень правильне про корінь n-ого степеня?",
                            Options = new()
                            {
                                new() { Text = "√x завжди є цілим числом", isAnswer = false },
                                new() { Text = "³√x може бути від’ємним", isAnswer = true },
                                new() { Text = "√x завжди більше x", isAnswer = false },
                                new() { Text = "³√x завжди дорівнює 1", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як спростити √(a^2)?",
                            Options = new()
                            {
                                new() { Text = "a", isAnswer = true },
                                new() { Text = "a^2", isAnswer = false },
                                new() { Text = "√a", isAnswer = false },
                                new() { Text = "2a", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яке з цих значень є правильним для ⁴√16?",
                            Options = new()
                            {
                                new() { Text = "2", isAnswer = true },
                                new() { Text = "4", isAnswer = false },
                                new() { Text = "8", isAnswer = false },
                                new() { Text = "16", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 9,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Яка формула Герона для знаходження площі трикутника?",
                            Options = new()
                            {
                                new() { Text = "S = √(p(p-a)(p-b)(p-c))", isAnswer = true },
                                new() { Text = "S = ab/2", isAnswer = false },
                                new() { Text = "S = a^2 + b^2", isAnswer = false },
                                new() { Text = "S = abc/2", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка з ознак є ознакою рівності трикутників?",
                            Options = new()
                            {
                                new() { Text = "Дві сторони і кут між ними", isAnswer = true },
                                new() { Text = "Дві сторони і медіана", isAnswer = false },
                                new() { Text = "Три кути", isAnswer = false },
                                new() { Text = "Три висоти", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка з ознак є ознакою подібності трикутників?",
                            Options = new()
                            {
                                new() { Text = "Два кути і сторона між ними", isAnswer = true },
                                new() { Text = "Три сторони однакові", isAnswer = false },
                                new() { Text = "Одна сторона і два кути", isAnswer = false },
                                new() { Text = "Три висоти", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що таке середня лінія трикутника?",
                            Options = new()
                            {
                                new() { Text = "Відрізок, що з'єднує середини двох сторін", isAnswer = true },
                                new() { Text = "Відрізок, що з'єднує вершину трикутника з протилежною стороною", isAnswer = false },
                                new() { Text = "Лінія, що ділить трикутник навпіл", isAnswer = false },
                                new() { Text = "Відрізок між двома кутами трикутника", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки ознак рівності трикутників існує?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = true },
                                new() { Text = "2", isAnswer = false },
                                new() { Text = "4", isAnswer = false },
                                new() { Text = "5", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 10,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Як звучить теорема Піфагора?",
                            Options = new()
                            {
                                new() { Text = "a^2 + b^2 = c^2", isAnswer = true },
                                new() { Text = "a^2 + b = c", isAnswer = false },
                                new() { Text = "a + b = c^2", isAnswer = false },
                                new() { Text = "a^2 - b^2 = c", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що таке синус кута в прямокутному трикутнику?",
                            Options = new()
                            {
                                new() { Text = "Відношення протилежного катета до гіпотенузи", isAnswer = true },
                                new() { Text = "Відношення прилеглого катета до гіпотенузи", isAnswer = false },
                                new() { Text = "Відношення катета до кута", isAnswer = false },
                                new() { Text = "Квадрат кута", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Чому дорівнює cos(90°)?",
                            Options = new()
                            {
                                new() { Text = "0", isAnswer = true },
                                new() { Text = "1", isAnswer = false },
                                new() { Text = "-1", isAnswer = false },
                                new() { Text = "0,5", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як знайти тангенс кута в прямокутному трикутнику?",
                            Options = new()
                            {
                                new() { Text = "tg = протилежний катет / прилеглий катет", isAnswer = true },
                                new() { Text = "tg = прилеглий катет / протилежний катет", isAnswer = false },
                                new() { Text = "tg = гіпотенуза / катет", isAnswer = false },
                                new() { Text = "tg = кут / катет", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що таке похила в геометрії?",
                            Options = new()
                            {
                                new() { Text = "Відрізок, проведений з точки до прямої під кутом", isAnswer = true },
                                new() { Text = "Гіпотенуза в прямокутному трикутнику", isAnswer = false },
                                new() { Text = "Лінія, що ділить трикутник навпіл", isAnswer = false },
                                new() { Text = "Сума кутів трикутника", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 11,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Які сторони рівнобедреного трикутника рівні між собою?",
                            Options = new()
                            {
                                new() { Text = "Дві бічні сторони", isAnswer = true },
                                new() { Text = "Усі сторони", isAnswer = false },
                                new() { Text = "Тільки основа", isAnswer = false },
                                new() { Text = "Усі кути", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Який кут в рівнобедреному трикутнику завжди дорівнює?",
                            Options = new()
                            {
                                new() { Text = "Кут при основі", isAnswer = true },
                                new() { Text = "Кут між бічними сторонами", isAnswer = false },
                                new() { Text = "Кут при вершині", isAnswer = false },
                                new() { Text = "Прямий кут", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається лінія, що проходить через вершину рівнобедреного трикутника і перпендикулярна основі?",
                            Options = new()
                            {
                                new() { Text = "Висота", isAnswer = true },
                                new() { Text = "Медіана", isAnswer = false },
                                new() { Text = "Бісектриса", isAnswer = false },
                                new() { Text = "Середня лінія", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки однакових сторін має рівнобедрений трикутник?",
                            Options = new()
                            {
                                new() { Text = "2", isAnswer = true },
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "1", isAnswer = false },
                                new() { Text = "Жодної", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Чи може рівнобедрений трикутник бути прямокутним?",
                            Options = new()
                            {
                                new() { Text = "Так", isAnswer = true },
                                new() { Text = "Ні", isAnswer = false },
                                new() { Text = "Лише в окремих випадках", isAnswer = false },
                                new() { Text = "Не може", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 12,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Скільки сторін рівні в паралелограмі?",
                            Options = new()
                            {
                                new() { Text = "Протилежні сторони", isAnswer = true },
                                new() { Text = "Усі сторони", isAnswer = false },
                                new() { Text = "Лише дві сторони", isAnswer = false },
                                new() { Text = "Тільки одна пара сторін", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка з фігур є ромбом?",
                            Options = new()
                            {
                                new() { Text = "Чотирикутник з рівними сторонами", isAnswer = true },
                                new() { Text = "Чотирикутник з усіма прямими кутами", isAnswer = false },
                                new() { Text = "Фігура з лише двома парами рівних сторін", isAnswer = false },
                                new() { Text = "Чотирикутник з рівними діагоналями", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка властивість квадрату є правильною?",
                            Options = new()
                            {
                                new() { Text = "Всі сторони рівні і всі кути прямі", isAnswer = true },
                                new() { Text = "Всі сторони рівні, але кути не обов'язково прямі", isAnswer = false },
                                new() { Text = "Має лише одну пару рівних сторін", isAnswer = false },
                                new() { Text = "Має рівні кути, але різні сторони", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Який чотирикутник має лише одну пару паралельних сторін?",
                            Options = new()
                            {
                                new() { Text = "Трапеція", isAnswer = true },
                                new() { Text = "Паралелограм", isAnswer = false },
                                new() { Text = "Ромб", isAnswer = false },
                                new() { Text = "Квадрат", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Який чотирикутник має рівні діагоналі, але не є квадратом?",
                            Options = new()
                            {
                                new() { Text = "Прямокутник", isAnswer = true },
                                new() { Text = "Ромб", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                                new() { Text = "Паралелограм", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 13,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Як знайти периметр трикутника?",
                            Options = new()
                            {
                                new() { Text = "Сума всіх сторін", isAnswer = true },
                                new() { Text = "Добуток всіх сторін", isAnswer = false },
                                new() { Text = "Сума двох сторін", isAnswer = false },
                                new() { Text = "Різниця двох сторін", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a^2", isAnswer = false },
                                new() { Text = "S = ab / 2", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Чому дорівнює периметр квадрата зі стороною 5 см?",
                            Options = new()
                            {
                                new() { Text = "20 см", isAnswer = true },
                                new() { Text = "25 см", isAnswer = false },
                                new() { Text = "10 см", isAnswer = false },
                                new() { Text = "15 см", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для площі трикутника?",
                            Options = new()
                            {
                                new() { Text = "S = 1/2 * a * h", isAnswer = true },
                                new() { Text = "S = a^2 + b^2", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = false },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Чому дорівнює площа кола з радіусом 7 см?",
                            Options = new()
                            {
                                new() { Text = "154 см²", isAnswer = true },
                                new() { Text = "49 см²", isAnswer = false },
                                new() { Text = "44 см²", isAnswer = false },
                                new() { Text = "100 см²", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 14,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 15,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 16,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 17,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 18,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 19,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 20,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 21,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 22,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 23,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 24,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 25,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 26,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 27,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 28,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 29,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 30,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
                new()
                {
                    LessonId = 31,
                    TestOptions = new()
                    {
                        new() {
                            Question = "Чому дорівнює 2 + 2?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Скільки сторін має квадрат?",
                            Options = new()
                            {
                                new() { Text = "3", isAnswer = false },
                                new() { Text = "4", isAnswer = true },
                                new() { Text = "5", isAnswer = false },
                                new() { Text = "6", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для знаходження площі прямокутника?",
                            Options = new()
                            {
                                new() { Text = "S = a + b", isAnswer = false },
                                new() { Text = "S = a * b", isAnswer = true },
                                new() { Text = "S = 2a + 2b", isAnswer = false },
                                new() { Text = "S = a²", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Що є коренем рівняння x^2 = 9?",
                            Options = new()
                            {
                                new() { Text = "x = 9", isAnswer = false },
                                new() { Text = "x = ±3", isAnswer = true },
                                new() { Text = "x = 3", isAnswer = false },
                                new() { Text = "x = -3", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Як називається трикутник з трьома рівними сторонами?",
                            Options = new()
                            {
                                new() { Text = "Рівносторонній", isAnswer = true },
                                new() { Text = "Рівнобедрений", isAnswer = false },
                                new() { Text = "Прямокутний", isAnswer = false },
                                new() { Text = "Трапеція", isAnswer = false },
                            },
                        },
                        new() {
                            Question = "Яка формула для обчислення периметра квадрата?",
                            Options = new()
                            {
                                new() { Text = "P = 4a", isAnswer = true },
                                new() { Text = "P = 2a + 2b", isAnswer = false },
                                new() { Text = "P = a^2", isAnswer = false },
                                new() { Text = "P = a + b", isAnswer = false },
                            },
                        },
                    }
                },
            };

            foreach (var lessonTest in lessonTests)
            {
                foreach (var testOption in lessonTest.TestOptions)
                {
                    var test = new Test
                    {
                        Question = testOption.Question,
                        Type = testOption.Type,
                        ImgUrl = testOption.ImgUrl,
                        LessonId = lessonTest.LessonId,
                    };

                    context.Tests.Add(test);
                    context.SaveChanges();

                    foreach (var option in testOption.Options)
                    {
                        option.TestId = context.Tests.Max(t => t.Id);
                        context.Options.Add(option);
                    }
                }
            }

            context.SaveChanges();
        }

        public static void InitializeCourses(CourseContext context)
        {
            if (context.Courses.Any()) return;
            if (context.Sections.Any()) return;

            var coursesSections = new List<Course>
            {
                new() {
                    Title = "Повний курс",
                    Description = "Цей курс містить всі теми, що потрібні для повної підготовки до ЗНО/НМТ.",
                    Duration = 6,
                    PriceFull = 2950,
                    PriceMonthly = 750,
                    Sections = new List<Section>
                    {
                        new() {
                            Number = 1,
                            Title = "Місяць 1",
                        },
                        new() {
                            Number = 2,
                            Title = "Місяць 2",
                        },
                        new() {
                            Number = 3,
                            Title = "Місяць 3",
                        },
                        new() {
                            Number = 4,
                            Title = "Місяць 4",
                        },
                        new() {
                            Number = 5,
                            Title = "Місяць 5",
                        },
                        new() {
                            Number = 6,
                            Title = "Місяць 6",
                        },
                    },
                },
                new() {
                    Title = "Скорочений курс",
                    Description = "Цей курс містить найнеобхідніші теми, що потрібні для ЗНО/НМТ.",
                    Duration = 4,
                    PriceFull = 1950,
                    PriceMonthly = 750,
                    Sections = new List<Section>
                    {
                        new() {
                            Number = 1,
                            Title = "Місяць 1",
                        },
                        new() {
                            Number = 2,
                            Title = "Місяць 2",
                        },
                        new() {
                            Number = 3,
                            Title = "Місяць 3",
                        },
                        new() {
                            Number = 4,
                            Title = "Місяць 4",
                        },
                    },
                },
                new() {
                    Title = "Алгебра",
                    Description = "Цей курс містить всі теми з алгебри, що потрібні для ЗНО/НМТ.",
                    Duration = 3,
                    PriceFull = 1650,
                    PriceMonthly = 750,
                    Sections = new List<Section>
                    {
                        new() {
                            Number = 1,
                            Title = "Місяць 1",
                        },
                        new() {
                            Number = 2,
                            Title = "Місяць 2",
                        },
                        new() {
                            Number = 3,
                            Title = "Місяць 3",
                        },
                    },
                },
                new() {
                    Title = "Геометрія",
                    Description = "Цей курс містить всі теми з геометрії, що потрібні для ЗНО/НМТ.",
                    Duration = 3,
                    PriceFull = 1450,
                    PriceMonthly = 750,
                    Sections = new List<Section>
                    {
                        new() {
                            Number = 1,
                            Title = "Місяць 1",
                        },
                        new() {
                            Number = 2,
                            Title = "Місяць 2",
                        },
                        new() {
                            Number = 3,
                            Title = "Місяць 3",
                        },
                    },
                },
            };

            foreach (var courseSections in coursesSections)
            {
                var course = new Course
                {
                    Title = courseSections.Title,
                    Description = courseSections.Description,
                    Duration = courseSections.Duration,
                    PriceFull = courseSections.PriceFull,
                    PriceMonthly = courseSections.PriceMonthly,
                };

                context.Courses.Add(course);
                context.SaveChanges();

                var courseId = context.Courses.Max(c => c.Id);

                foreach (var section in courseSections.Sections)
                {
                    section.CourseId = courseId;
                    context.Sections.Add(section);
                }
            }

            context.SaveChanges();
        }

        public static void InitializeSectionLessons(CourseContext context)
        {
            if (context.SectionLessons.Any()) return;

            var sectionsToLessons = new List<SectionToLessons> {
                // повний курс
                new() {
                    SectionId = 1,
                    LessonIds = Enumerable.Range(1, 6).ToList(),
                },
                new() {
                    SectionId = 2,
                    LessonIds = Enumerable.Range(7, 5).ToList(),
                },
                new() {
                    SectionId = 3,
                    LessonIds = Enumerable.Range(12, 5).ToList(),
                },
                new() {
                    SectionId = 4,
                    LessonIds = Enumerable.Range(17, 5).ToList(),
                },
                new() {
                    SectionId = 5,
                    LessonIds = Enumerable.Range(22, 5).ToList(),
                },
                new() {
                    SectionId = 6,
                    LessonIds = Enumerable.Range(27, 5).ToList(),
                },

                // скорочений курс
                new() {
                    SectionId = 7,
                    LessonIds = new List<int> { 1, 2, 3, 4, 5 },
                },
                new() {
                    SectionId = 8,
                    LessonIds = new List<int> { 6, 7, 8, 9, 10 },
                },
                new() {
                    SectionId = 9,
                    LessonIds = new List<int> { 11, 12, 13, 14, 15 },
                },
                new() {
                    SectionId = 10,
                    LessonIds = new List<int> { 16, 19, 23, 24, 26 },
                },

                // алгебра
                new() {
                    SectionId = 11,
                    LessonIds = new List<int> { 1, 2, 3, 5, 6, 7 },
                },
                new() {
                    SectionId = 12,
                    LessonIds = new List<int> { 8, 17, 18, 19, 20, 21, 22 },
                },
                new() {
                    SectionId = 13,
                    LessonIds = new List<int> { 26, 27, 28, 29, 30, 31 },
                },

                // геометрія
                new() {
                    SectionId = 14,
                    LessonIds = new List<int> { 4, 9, 10, 11 },
                },
                new() {
                    SectionId = 15,
                    LessonIds = new List<int> { 12, 13, 14, 15 },
                },
                new() {
                    SectionId = 16,
                    LessonIds = new List<int> { 16, 23, 24, 25 },
                },
            };

            foreach (var sectionToLessons in sectionsToLessons)
            {
                foreach (var lessonId in sectionToLessons.LessonIds)
                {
                    context.SectionLessons.Add(
                        new SectionLesson
                        {
                            SectionId = sectionToLessons.SectionId,
                            LessonId = lessonId,
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

    class SectionToLessons
    {
        public int SectionId { get; set; }
        public List<int> LessonIds { get; set; }
    }

    class UserToSections
    {
        public string UserName { get; set; }
        public List<int> SectionIds { get; set; }
    }

    class UserToLessons
    {
        public string UserName { get; set; }
        public List<UserLessonInfo> LessonsInfo { get; set; }
    }

    class UserLessonInfo
    {
        public int LessonId { get; set; }
        public bool IsTheoryCompleted { get; set; } = false;
        public bool IsPracticeCompleted { get; set; } = false;
        public float? TestScore { get; set; }
    }

    class LessonToPreviousLessons
    {
        public int LessonId { get; set; }
        public List<int> PreviousLessonIds { get; set; }
    }

    class LessonTests
    {
        public int LessonId { get; set; }
        public List<TestOptions> TestOptions { get; set; }
    }

    class TestOptions
    {
        public string Question { get; set; }
        public string Type { get; set; } = "radio";
        public string ImgUrl { get; set; } = "";
        public List<Option> Options { get; set; }
    }
}

