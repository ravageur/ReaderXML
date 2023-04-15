using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderXML;
using ReaderXML.Common;
using System;
using System.Collections.Generic;

namespace ReaderXMLTests
{
    [TestClass]
    public class ReaderTests
    {
        /// <summary>
        /// We test if it will not crash when we give an empty path.
        /// </summary>
        [TestMethod]
        public void Test_reader()
        {
            Reader reader = new();
            ElementXML elementXML = reader.ReadFile("");
            Console.WriteLine(elementXML.ToString(1));
        }

        [TestMethod]
        public void Test_file1()
        {
            Reader reader = new();
            ElementXML elementXML = reader.ReadFile("../../../files_tests/file_1.xml");



            TestGeneric(elementXML, "xml", "", new string[] { "note" });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "note", "", new string[] { "to", "from", "heading", "body" });

            TestGeneric(elementXML.ElementsXML[0], "to", "Tove", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "from", "Jani", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "heading", "Reminder", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "body", "Don\'t forget me this weekend!", Array.Empty<string>());
        }

        [TestMethod]
        public void Test_file2()
        {
            Reader reader = new();
            ElementXML elementXML = reader.ReadFile("../../../files_tests/file_2.xml");



            TestGeneric(elementXML, "xml", "", new string[] { "world" });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "world", "", new string[] { "country" });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "country", "", new string[] { "city", "city" });

            TestGeneric(elementXML.ElementsXML[0], "city", "Kyoto", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "city", "Tokyo", Array.Empty<string>());
        }

        [TestMethod]
        public void Test_file3()
        {
            Reader reader = new();
            ElementXML elementXML = reader.ReadFile("../../../files_tests/file_3.xml");


            TestGeneric(elementXML, "xml", "", new string[] { "school" });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "school", "", new string[] { "student", "student" });

            TestGeneric(elementXML.ElementsXML[0], "student", "", new string[] { "name", "age"});

            TestGeneric(elementXML.ElementsXML[0].ElementsXML[0], "name", "John", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[0].ElementsXML[1], "age", "18", Array.Empty<string>());


            TestGeneric(elementXML.ElementsXML[1], "student", "", new string[] { "name", "age" });

            TestGeneric(elementXML.ElementsXML[1].ElementsXML[0], "name", "Leyla", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1].ElementsXML[1], "age", "21", Array.Empty<string>());
        }

        [TestMethod]
        public void Test_file4()
        {
            Reader reader = new();
            ElementXML elementXML = reader.ReadFile("../../../files_tests/file_4.xml");



            TestGeneric(elementXML, "xml", "", new string[] { "note" });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "note", "", new string[] { "to", "from", "heading", "body" });

            TestGeneric(elementXML.ElementsXML[0], "to", "Tove", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "from", "Jani", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "heading", "Reminder", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "body", "Don\'t forget me this weekend!", Array.Empty<string>());
        }

        [TestMethod]
        public void Test_file5()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_5.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file6()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_6.xml");



            TestGeneric(elementXML, "xml", "", new string[] 
            { 
                "math" 
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[] 
            { 
                "number", 
                "number", 
                "number", 
                "number", 
                "number", 
                "number", 
                "number", 
                "number" 
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file7()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_7.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file8()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_8.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file9()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_9.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file10()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_10.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file11()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_11.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file12()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_12.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file13()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_13.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file14()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_14.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file15()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_15.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file16()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_16.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }

        [TestMethod]
        public void Test_file17()
        {
            ElementXML elementXML = new Reader().ReadFile("../../../files_tests/file_17.xml");



            TestGeneric(elementXML, "xml", "", new string[]
            {
                "math"
            });

            elementXML = elementXML.ElementsXML[0];
            TestGeneric(elementXML, "math", "", new string[]
            {
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number",
                "number"
            });



            TestGeneric(elementXML.ElementsXML[0], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[1], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[2], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[3], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[4], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[5], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[6], "number", "", Array.Empty<string>());
            TestGeneric(elementXML.ElementsXML[7], "number", "", Array.Empty<string>());

            List<string> attributesToVerify = new()
            {
                "1",
                " 2",
                "3 ",
                " 4 ",
                "5",
                " 6",
                "7 ",
                " 8 "
            };

            for (int i = 0; i < 8; i++)
            {
                TestAttributes(elementXML.ElementsXML[i], new()
                {
                    new()
                    {
                        "nb",
                        attributesToVerify[i]
                    }
                });
            }
        }




        private static void TestGeneric(ElementXML elementXML, string nameElementExpected, string valueExpected, string[] namesExpected)
        {
            Assert.IsTrue(elementXML.Name.Equals(nameElementExpected), elementXML.Name + " is not equal to " + nameElementExpected);
            Assert.IsTrue(elementXML.Value.Equals(valueExpected), elementXML.Value + " is not equal to " + valueExpected);

            Assert.IsTrue(namesExpected.Length == elementXML.ElementsXML.Count, "There are " + namesExpected.Length + " names provided but it is expected to be " + elementXML.ElementsXML.Count + " names.");

            for (int i = 0; i < elementXML.ElementsXML.Count; i++)
            {
                Assert.IsTrue(elementXML.ElementsXML[i].Name.Equals(namesExpected[i]),
                    elementXML.ElementsXML[i].Name + " is not equal to " + namesExpected[i]);
            }
        }

        private static void TestAttributes(ElementXML elementXml, List<List<string>> attributesToTest)
        {
            Assert.IsTrue(elementXml.AttributesXML.Count == attributesToTest.Count);

            for (int i = 0; i < elementXml.AttributesXML.Count; i++)
            {
                Assert.IsTrue(elementXml.AttributesXML[i].Name.Equals(attributesToTest[i][0]));
                Assert.IsTrue(elementXml.AttributesXML[i].Value.Equals(attributesToTest[i][1]));
            }
        }
    }
}