using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {

        [SetUp]
        public void ResetScene()
        {

            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);

        }

        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        public void BasicTest()
        {

            bool isActive = false;

            Assert.AreEqual(false, isActive);

        }

        [Test]
        public void CatchingErrors()
        {

            GameObject gameObject = new GameObject("test");

            Assert.Throws<MissingComponentException>(
                () => gameObject.GetComponent<Rigidbody>().velocity = Vector3.one
            );

        }
    }
}
