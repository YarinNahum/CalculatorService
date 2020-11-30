using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WCFCalculator_Service;
using FakeItEasy;
using Data_Layer;
using Calculator_Library;

namespace Unit_Tests
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void TestProcessAction()
        {
            //Arrange
            Stack<double> myStack = new Stack<double>();
            myStack.Push(1);
            myStack.Push(2);

            var data = A.Fake<IDataHolder<double>>();
            var calc = A.Fake<ICalculatorFunctions>();

            A.CallTo(() => data.RemoveElement()).Invokes(() => myStack.Pop()).Returns(myStack.Peek());
            A.CallTo(() => data.InsertElement(A<double>.Ignored)).Invokes(() => myStack.Push(3));

            WCFCalculatorImpl service = new WCFCalculatorImpl(data,calc);

            //Act
            service.ProcessAction("+");

            //Assert
            Assert.AreEqual(1, myStack.Count);
            Assert.AreEqual(3, myStack.Peek());

        }
        [TestMethod]
        public void TestProcessSingleRequest1()
        {
            Stack<double> myStack = new Stack<double>();
            myStack.Push(1);
            myStack.Push(2);

            var data = A.Fake<IDataHolder<double>>();
            var calc = A.Fake<ICalculatorFunctions>();

            A.CallTo(() => data.GetSize()).Returns(myStack.Count);

            A.CallTo(() => data.RemoveElement()).Invokes(() => myStack.Pop()).Returns(myStack.Peek());
            A.CallTo(() => data.InsertElement(A<double>.Ignored)).Invokes((double x) => myStack.Push(x));


            WCFCalculatorImpl service = new WCFCalculatorImpl(data, calc);

            //Act
            service.ProcessSingleRequest("*");

            //Assert
            Assert.AreEqual(1, myStack.Count);
            Assert.AreEqual(2, myStack.Peek());

        }

        [TestMethod]
        public void TestProcessSingleRequest2()
        {
            Stack<double> myStack = new Stack<double>();
            myStack.Push(1);
            myStack.Push(2);

            var data = A.Fake<IDataHolder<double>>();
            var calc = A.Fake<ICalculatorFunctions>();

            A.CallTo(() => data.GetSize()).Returns(myStack.Count);
            A.CallTo(() => data.RemoveElement()).Invokes(() => myStack.Pop()).Returns(myStack.Peek());



            WCFCalculatorImpl service = new WCFCalculatorImpl(data, calc);

            //Act
            service.ProcessSingleRequest("_");

            A.CallTo(() => data.RemoveElement()).MustHaveHappenedOnceExactly();
            //Assert
            Assert.AreEqual(1, myStack.Count);

        }

        [TestMethod]
        public void TestProcessSingleRequest3()
        {
            Stack<double> myStack = new Stack<double>();
            myStack.Push(1);
            myStack.Push(2);

            var data = A.Fake<IDataHolder<double>>();
            var calc = A.Fake<ICalculatorFunctions>();

            A.CallTo(() => data.GetSize()).Returns(myStack.Count);
            A.CallTo(() => data.InsertElement(A<double>.Ignored)).Invokes((double x) => myStack.Push(x));


            WCFCalculatorImpl service = new WCFCalculatorImpl(data, calc);

            //Act
            service.ProcessSingleRequest("123.45");

            //Assert
            Assert.AreEqual(3, myStack.Count);
            Assert.AreEqual(123.45, myStack.Peek());

        }


    }
}
