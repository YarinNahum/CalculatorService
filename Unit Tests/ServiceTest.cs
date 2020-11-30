using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WCFCalculator_Service;
using FakeItEasy;
using Data_Layer;
using Calculator_Library;
using ServiceContracts;
using System.ServiceModel;

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

            WCFCalculatorImpl service = new WCFCalculatorImpl(data, calc);

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
            var calc = A.Fake<CalculatorFunctionsImpl>(options => options.CallsBaseMethods());

            A.CallTo(() => data.GetSize()).ReturnsLazily(()=>myStack.Count);

            A.CallTo(() => data.RemoveElement()).ReturnsLazily(()=>myStack.Pop());
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
            //Arrange
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

        [TestMethod]
        public void TestProcessRequest1()
        {
            //Arrange
            Stack<double> myStack = new Stack<double>();

            var data = A.Fake<IDataHolder<double>>();
            var calc = A.Fake<CalculatorFunctionsImpl>(option => option.CallsBaseMethods());
            var callback = A.Fake<ICallback>();

            A.CallTo(() => data.GetSize()).ReturnsLazily(()=>myStack.Count);
            A.CallTo(() => data.InsertElement(A<double>.Ignored)).Invokes((double x) => myStack.Push(x));

            A.CallTo(() => data.RemoveElement()).ReturnsLazily(()=>myStack.Pop());
            string[] request = { "3", "4", "+", "2", "*"};

            IWCFCalculator service = new WCFCalculatorImpl(data, calc);



            //Act
            service.ProcessRequest(new List<string>(request));

            //Assert
            Assert.AreEqual(1, myStack.Count);
            Assert.AreEqual(14, myStack.Peek());

        }
        [TestMethod]
        public void TestProcessRequest2()
        {
            //Arrange
            Stack<double> myStack = new Stack<double>();
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(3);


            var data = A.Fake<IDataHolder<double>>();
            var calc = A.Fake<CalculatorFunctionsImpl>(option => option.CallsBaseMethods());
            var callback = A.Fake<ICallback>();

            A.CallTo(() => data.GetData()).Returns(myStack);
            A.CallTo(() => data.GetSize()).ReturnsLazily(() => myStack.Count);
            A.CallTo(() => data.InsertElement(A<double>.Ignored)).Invokes((double x) => myStack.Push(x));
            A.CallTo(() => data.SetData(A<Stack<double>>.Ignored)).Invokes((Stack<double> s) => myStack = new Stack<double>(s));
            A.CallTo(() => data.RemoveElement()).ReturnsLazily(() => myStack.Pop());
            
            string[] request = {"+", "*" , "+"};

            IWCFCalculator service = new WCFCalculatorImpl(data, calc);



            //Act
            service.ProcessRequest(new List<string>(request));

            //Assert
            Assert.AreEqual(3, myStack.Count);
            Assert.AreEqual(3, myStack.Peek());

        }

    }
}
