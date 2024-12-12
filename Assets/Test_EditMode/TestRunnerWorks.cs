using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class TestRunnerWorksTest

{
    private int a;
    private int b;
    [Test]
    public void workSimplePasses()
    {
       a = 1;
        b = 2;

        Assert.AreEqual(3, a + b, "They are not equal");
    }
    [Test]
    public void workSimplePasses2()
    {
        a = 1;
        b = 2;

        Assert.AreNotEqual(4,a + b, "They are not equal");
    }

}
