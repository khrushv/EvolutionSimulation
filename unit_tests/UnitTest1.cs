using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
namespace unit_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBallConstructor()
        {
            app.Ball actualBall = new app.Ball(2.0, 0.0, 0.0);
            Assert.AreEqual(actualBall.X, 0.0);
            Assert.AreEqual(actualBall.Y, 0.0);
            Assert.AreEqual(actualBall.Radius, 2.0);
        }
        
        [TestMethod]
        public void TestNnNextDoubleRange()
        {
            app.NN nn = new app.NN();
            Assert.AreEqual(nn.NextDoubleRange(-1f, 1f), 0.452486515045166);
        }

        [TestMethod]
        public void TestNnConstructorOneLayer()
        {
            app.NN nn = new app.NN(new int[] {1,1});
            Assert.AreEqual(nn.layers[0].weights[0,0], 0.452486515045166);
           // Assert.AreEqual(nn.layers[1].weights[0,0], 0);
        }

        [TestMethod]
        public void TestNnConstructorTwoLayerCorrect()
        {
            app.NN nn = new app.NN(new int[] { 1, 2, 2});
            float[] expectedWeights = new float[6];
            app.Layer firstLayer = new app.Layer(1, 2);
            firstLayer.weights[0, 0] = 0.452486515045166f;
            firstLayer.weights[0, 1] = 0.6346507f;
            Assert.IsTrue(nn.layers[0].Equals(firstLayer));
            app.Layer secondLayer = new app.Layer(2, 2);
            secondLayer.weights[0, 0] = 0.53604543f;
            secondLayer.weights[0, 1] = 0.1163224f;
            secondLayer.weights[1, 0] = -0.58793366f;
            secondLayer.weights[1, 1] = 0.1177696f;
            Assert.IsTrue(nn.layers[1].Equals(secondLayer));
        }

        [TestMethod]
        public void TestNnConstructorTwoLayerIncorrect()
        {
            app.NN nn = new app.NN(new int[] { 1, 2, 2 });
            float[] expectedWeights = new float[6];
            app.Layer firstLayer = new app.Layer(1, 2);
            firstLayer.weights[0, 0] = 100f;
            firstLayer.weights[0, 1] = 1f;
            Assert.IsFalse(nn.layers[0].Equals(firstLayer));

        }
        [TestMethod]
        public void TestNnFeed()
        {
            app.NN nn = new app.NN(new int[] { 1, 2, 2 });
            Assert.AreEqual(nn.FeedForward(new float[] { 1.0f })[0], -0.13057920336723328);
            Assert.AreEqual(nn.FeedForward(new float[] { 1.0f })[1], 0.12737686932086945);
        }
    }
}
