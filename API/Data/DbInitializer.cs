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
                    SectionIds = Enumerable.Range(1, 5).ToList(),
                },
                new() {
                    UserName = "yaryna",
                    SectionIds = Enumerable.Range(1, 21).ToList(),
                },
                new() {
                    UserName = "sam",
                    SectionIds = new List<int> { 1, 10, 11 },
                },
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
                },
                new() {
                    UserName = "sam",
                    LessonsInfo = new List<UserLessonInfo> {
                        new() {
                            LessonId = 3,
                            TestScore = 90,
                        },
                        new() {
                            LessonId = 4,
                            TestScore = 50,
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
                },
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
                new Lesson
                {
                    Title = "Види чисел, дроби, НСД, НСК, порівняння дробів",
                    Description = "На уроці ви дізнаєтеся які бувають види чисел (натуральні, цілі, ірраціональні...) та дробів (правильні, неправильні, десяткові, мішані). Також навчитеся знаходити НСД і НСК, перетворювати дроби з одного виду в інший та порівнювати їх.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 1,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Десяткові дроби, дії з ними, модуль",
                    Description = "На уроці ви дізнаєтеся як виконувати додавання, віднімання, множення та ділення десяткових дробів і навчитеся знаходити модуль числа.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 2,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Відношення, пропорція, відсотки, банк",
                    Description = "На уроці ви дізнаєтеся що таке відсотки та як знайти відсоток від числа. Також навчитеся розв'язувати задачі пов'язані з депозитами в банку.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 3,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Вступ в геометрію: фігури на площині, кути",
                    Description = "На уроці ви отримаєте базові знання з геометрії: які бувають фігури на площині та кути.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 4,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Степінь, лінійні рівняння",
                    Description = "На уроці ви дізнаєтеся що таке степінь числа, базові дії зі степенями. Також навчитеся розв'язувати ліінйні рівняння, що є основої для всіх інших видів рівнянь.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 5,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Формули скороченого множення, квадратні рівняння",
                    Description = "На уроці ви дізнаєтеся які є формули скороченого множення та їх застосування, а також як розв'язувати квадратні рівняння за допомогою дискримінанта або теореми Вієта.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 6,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Буквенні вирази, ОДЗ, властивості дробів",
                    Description = "На уроці ви дізнаєтеся що таке область допустимих значень виразу (ОДЗ) та деякі властивості не тільки числових, а й буквенних дробів.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 7,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Квадратний корінь, корінь n-ого степеня",
                    Description = "На уроці ви дізнаєтеся як знаходити квадратний корінь і n-ого степеня та які їхні властивості.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 8,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Трикутник. 3 ознаки рівності трик., 3 ознаки подібності трик., формула Герона",
                    Description = "На уроці ви дізнаєтеся всі базові складові трикутника, такі як середня лінія, висота, медіана, бісектриса та які властивості вони мають. Також буде вивчите по 3 ознаки рівності і подібності трикутників та формулу Герона для знаходження площі трикутника за 3-ма сторонами.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 9,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Прямокутний трикутник, теорема Піфагора, sin, cos, tg, ctg, похила",
                    Description = "На уроці ви глибше зануритеся в тему трикутників та дізнаєтесь набагато більше про один з їх видів - прямокутний трикутник. Вивчите теорему Піфагора - одну з найважливіших теорем в геометрії. А також отримаєте перше уявлення про такі тригонометричні функції, як синус, косинус, тангенс і котангенс. Також буде розглянуте поняття похилої та її проекції.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 10,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Рівнобедрений трикутник",
                    Description = "На уроці ви більше вивчите про другий вид трикутників - рівнобедрений.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 11,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Чотирикутники: паралелограм, ромб, прямокутник, квадрат, трапеція",
                    Description = "На уроці ви дізнаєтеся які бувають види 4-кутників та їхні особливості.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 12,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Периметр і площа фігур",
                    Description = "На уроці будуть формули (та пояснення звідки вони отримуються) периметрів і площ трикутників та різних видів чотирикутників.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 13,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Коло та круг",
                    Description = "На уроці будуть поясненя основні складові кола та круга, їх основні властивості та формули довжини лінії кола/площі круга.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 14,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Вписане і описане кола, теорема косинусів і синусів",
                    Description = "На уроці ви дізнаєтеся все необхідне про вписані і описані кола: де знаходиться центр, як знайти їхній радіус, яким критеріям має відповідати 4-кутник, щоб в нього можна було вписати або навколо нього описати коло..",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 15,
                    Importance = 0,
                },
                new Lesson
                {
                    Title = "Многокутники",
                    Description = "На уроці ви дізнаєтеся основні формули для правильних многокутників: як знайти внутрішній кут, вписане/описане коло.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 16,
                    Importance = 1,
                },
                new Lesson
                {
                    Title = "Складніші рівняння, теорема Безу",
                    Description = "На уроці ви будуть розглянуті способи розв'язання степеневих рівнянь та теорема Безу.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 17,
                    Importance = 2,
                },
                new Lesson
                {
                    Title = "Складніші рівняння, част 2",
                    Description = "На уроці будуть розглянуті способи розв'язання рівнянь з модулем.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 18,
                    Importance = 1,
                },
                new Lesson
                {
                    Title = "Показниковий вираз, логарифм",
                    Description = "На уроці ви дізнаєтеся що таке показниковий вираз, логарифм, чому вони пов'язані та основні дії з ними.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 19,
                    Importance = 1,
                },
                new Lesson
                {
                    Title = "Показникові рівняння",
                    Description = "На уроці ви навчитеся розв'язувати основні види показникових рівнянь.",
                    UrlTheory = "",
                    UrlPractice = "",
                    Number = 20,
                    Importance = 1,
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

        public static void InitializePreviousLessons(CourseContext context)
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

        public static void InitializeTests(CourseContext context)
        {
            if (context.Tests.Any()) return;
            if (context.Options.Any()) return;

            var lessonTests = new List<LessonTests> {
                new LessonTests
                {
                    LessonId = 1,
                    TestOptions = new List<TestOptions>
                    {
                        new TestOptions
                        {
                            Question = "1. Вкажіть натуральне число",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "4,1",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "0",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "-3",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "17",
                                    isAnswer = true,
                                },
                                new Option
                                {
                                    Text = "1,8",
                                    isAnswer = false,
                                },
                            },
                        },
                        new TestOptions
                        {
                            Question = "2. При діленні числа 28 на 6 остача дорівнює",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "0",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "1",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "2",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "4",
                                    isAnswer = true,
                                },
                                new Option
                                {
                                    Text = "6",
                                    isAnswer = false,
                                },
                            },
                        },
                        new TestOptions
                        {
                            Question = "3. Скоротіть дріб 108/18 до нескоротного",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "108/18",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "54/9",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "7",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "34/3",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "6",
                                    isAnswer = true,
                                },
                            },
                        },
                    },
                },
                new LessonTests
                {
                    LessonId = 2,
                    TestOptions = new List<TestOptions>
                    {
                        new TestOptions
                        {
                            Question = "1.  7412/1000 =",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "7412000",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "74,12",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "7,412",
                                    isAnswer = true,
                                },
                                new Option
                                {
                                    Text = "0,7412",
                                    isAnswer = false,
                                },
                            },
                        },
                        new TestOptions
                        {
                            Question = "2.  1,61 + 2,49",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "4,1",
                                    isAnswer = true,
                                },
                                new Option
                                {
                                    Text = "4",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "3,1",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "3",
                                    isAnswer = false,
                                },
                            },
                        },
                        new TestOptions
                        {
                            Question = "3.  7×1,2=",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "8,4",
                                    isAnswer = true,
                                },
                                new Option
                                {
                                    Text = "8,2",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "0,74",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "9,2",
                                    isAnswer = false,
                                },
                            },
                        },
                    },
                },
                new LessonTests
                {
                    LessonId = 3,
                    TestOptions = new List<TestOptions>
                    {
                        new TestOptions
                        {
                            Question = "1. Виразіть число 2/5 у відсотках",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "10%",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "20%",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "30%",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "40%",
                                    isAnswer = true,
                                },
                            },
                        },
                        new TestOptions
                        {
                            Question = "2. Якщо b = (33*a)/100, тоді",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "a становить 33% від b",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "b становить 33% від a",
                                    isAnswer = true,
                                },
                                new Option
                                {
                                    Text = "a становить ∼3% від b",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "b становить ∼3% від a",
                                    isAnswer = false,
                                },
                            },
                        },
                        new TestOptions
                        {
                            Question = "3. Ви поповнювали рахунок через термінал, який бере 5% комісії. У результаті комісія становила 3 грн, а решта грошей прийшла на рахунок💰 На скільки грн поповнився ваш рахунок?",
                            Options = new List<Option>
                            {
                                new Option
                                {
                                    Text = "60 грн",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "57 грн",
                                    isAnswer = true,
                                },
                                new Option
                                {
                                    Text = "15 грн",
                                    isAnswer = false,
                                },
                                new Option
                                {
                                    Text = "12 грн",
                                    isAnswer = false,
                                },
                            },
                        },
                    },
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
                new Course
                {
                    Title = "Повний курс",
                    Description = "Цей курс містить всі теми, що потрібні для повної підготовки до ЗНО/НМТ.",
                    Duration = 8,
                    PriceFull = 1850,
                    PriceMonthly = 360,
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Number = 1,
                            Title = "Місяць 1",
                        },
                        new Section
                        {
                            Number = 2,
                            Title = "Місяць 2",
                        },
                        new Section
                        {
                            Number = 3,
                            Title = "Місяць 3",
                        },
                        new Section
                        {
                            Number = 4,
                            Title = "Місяць 4",
                        },
                        new Section
                        {
                            Number = 5,
                            Title = "Місяць 5",
                        },
                        new Section
                        {
                            Number = 6,
                            Title = "Місяць 6",
                        },
                        new Section
                        {
                            Number = 7,
                            Title = "Місяць 7",
                        },
                        new Section
                        {
                            Number = 8,
                            Title = "Місяць 8",
                        },
                    },
                },
                new Course
                {
                    Title = "Скорочений курс",
                    Description = "Цей курс містить найнеобхідніші теми, що потрібні для ЗНО/НМТ.",
                    Duration = 5,
                    PriceFull = 1550,
                    PriceMonthly = 360,
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Number = 1,
                            Title = "Місяць 1",
                        },
                        new Section
                        {
                            Number = 2,
                            Title = "Місяць 2",
                        },
                        new Section
                        {
                            Number = 3,
                            Title = "Місяць 3",
                        },
                        new Section
                        {
                            Number = 4,
                            Title = "Місяць 4",
                        },
                        new Section
                        {
                            Number = 5,
                            Title = "Місяць 5",
                        },
                    },
                },
                new Course
                {
                    Title = "Алгебра",
                    Description = "Цей курс містить всі теми з алгебри, що потрібні для ЗНО/НМТ.",
                    Duration = 5,
                    PriceFull = 1250,
                    PriceMonthly = 360,
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Number = 1,
                            Title = "Місяць 1",
                        },
                        new Section
                        {
                            Number = 2,
                            Title = "Місяць 2",
                        },
                        new Section
                        {
                            Number = 3,
                            Title = "Місяць 3",
                        },
                        new Section
                        {
                            Number = 4,
                            Title = "Місяць 4",
                        },
                        new Section
                        {
                            Number = 5,
                            Title = "Місяць 5",
                        },
                    },
                },
                new Course
                {
                    Title = "Геометрія",
                    Description = "Цей курс містить всі теми з геометрії, що потрібні для ЗНО/НМТ.",
                    Duration = 3,
                    PriceFull = 950,
                    PriceMonthly = 360,
                    Sections = new List<Section>
                    {
                        new Section
                        {
                            Number = 1,
                            Title = "Місяць 1",
                        },
                        new Section
                        {
                            Number = 2,
                            Title = "Місяць 2",
                        },
                        new Section
                        {
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
                new SectionToLessons {
                    SectionId = 1,
                    LessonIds = Enumerable.Range(1, 4).ToList(),
                },
                new SectionToLessons {
                    SectionId = 2,
                    LessonIds = Enumerable.Range(5, 4).ToList(),
                },
                new SectionToLessons {
                    SectionId = 3,
                    LessonIds = Enumerable.Range(9, 2).ToList(),
                },
                new SectionToLessons {
                    SectionId = 4,
                    LessonIds = Enumerable.Range(11, 3).ToList(),
                },
                new SectionToLessons {
                    SectionId = 5,
                    LessonIds = Enumerable.Range(14, 2).ToList(),
                },
                new SectionToLessons {
                    SectionId = 6,
                    LessonIds = Enumerable.Range(16, 1).ToList(),
                },
                new SectionToLessons {
                    SectionId = 7,
                    LessonIds = Enumerable.Range(17, 2).ToList(),
                },
                new SectionToLessons {
                    SectionId = 8,
                    LessonIds = Enumerable.Range(19, 2).ToList(),
                },
                new SectionToLessons {
                    SectionId = 9,
                    LessonIds = new List<int> { 1, 2, 3 },
                },
                new SectionToLessons {
                    SectionId = 10,
                    LessonIds = new List<int> { 4, 5 },
                },
                new SectionToLessons {
                    SectionId = 11,
                    LessonIds = new List<int> { 6, 7, 8, 9 },
                },
                new SectionToLessons {
                    SectionId = 12,
                    LessonIds = new List<int> { 10, 11, 12, 13, 14 },
                },
                new SectionToLessons {
                    SectionId = 13,
                    LessonIds = new List<int> { 15, 16, 19 },
                },
                new SectionToLessons {
                    SectionId = 14,
                    LessonIds = new List<int> { 1, 2, 3 },
                },
                new SectionToLessons {
                    SectionId = 15,
                    LessonIds = new List<int> { 5, 6, 7 },
                },
                new SectionToLessons {
                    SectionId = 16,
                    LessonIds = new List<int> { 8, 17 },
                },
                new SectionToLessons {
                    SectionId = 17,
                    LessonIds = new List<int> { 18 },
                },
                new SectionToLessons {
                    SectionId = 18,
                    LessonIds = new List<int> { 19, 20 },
                },
                new SectionToLessons {
                    SectionId = 19,
                    LessonIds = new List<int> { 4, 9, 10, 11 },
                },
                new SectionToLessons {
                    SectionId = 20,
                    LessonIds = new List<int> { 12, 13 },
                },
                new SectionToLessons {
                    SectionId = 21,
                    LessonIds = new List<int> { 14, 15, 16 },
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

