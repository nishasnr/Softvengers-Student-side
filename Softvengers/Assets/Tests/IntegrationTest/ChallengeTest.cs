using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ChallengeTest
    {
        [UnityTest]
        public IEnumerator ChallengeTestWithEnumeratorPasses()
        {
            
            yield return null;
        }
    }
}
