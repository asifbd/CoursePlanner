﻿using Panta.Indexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Panta.Tests
{
    
    
    /// <summary>
    ///This is a test class for StringSplitterTest and is intended
    ///to contain all StringSplitterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringSplitterTests
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Split
        ///</summary>
        [TestMethod()]
        public void SplitTests()
        {
            string s = "! Microsoft!!.are VisualStudio222.222TestTools..@@.UnitTesting";
            Assert.AreEqual("microsoft visualstudio222 222testtools unittesting", String.Join(" ", StringSplitter.Split(s)));
        }

        [TestMethod]
        public void SeparatePrefixTests()
        {
            string prefix;
            string root;

            // Regular case
            StringSplitter.SeparatePrefix("a:b", out prefix, out root);
            Assert.AreEqual("a: b", String.Join(" ", prefix, root));

            // Multiple colons
            StringSplitter.SeparatePrefix("a::b", out prefix, out root);
            Assert.AreEqual("a: :b", String.Join(" ", prefix, root));

            // End with colon
            StringSplitter.SeparatePrefix("aaaa:", out prefix, out root);
            Assert.AreEqual(" aaaa:", String.Join(" ", prefix, root));

        }
    }
}
