using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaderXML;
using ReaderXML.common;
using System;

namespace ReaderXMLTests
{
    [TestClass]
    public class ReaderTests
    {
        /// <summary>
        /// We test if it will not crash when we give an empty path.
        /// </summary>
        [TestMethod]
        public void Test_1()
        {
            Reader reader = new();
            ElementXML elementXML = reader.ReadFile("");
            Console.WriteLine(elementXML.ToString(1));
        }

        [TestMethod]
        public void Test_2()
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
        public void Test_3()
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
        public void Test_4()
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
        public void Test_5()
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
    }
}