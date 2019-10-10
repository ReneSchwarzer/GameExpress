using System;
using GameExpress.Model.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.Structs
{
    [TestClass]
    public class UnitTestVector
    { 
        public void Setup()
        {
        }

        /// <summary>
        /// Teste die Längeneigenschaft anhand des pythagoreisches Zahlentripel
        /// </summary>
        [TestMethod]
        public void VectorLength()
        {
            var v1 = new Vector(3.0, 4.0);
            var v2 = new Vector(4.0, 3.0);

            Assert.IsTrue(v1.Length == 5.0, "Die Länge des Vektors ist falsch");
            Assert.IsTrue(v2.Length == 5.0, "Die Länge des Vektors ist falsch");
            Assert.IsFalse(v2.Length == 6.0, "Die Länge des Vektors ist falsch");
        }

        /// <summary>
        /// Teste die Gleichheit
        /// </summary>
        [TestMethod]
        public void Equal()
        {
            var v1 = new Vector(3.0, 4.0);
            var v2 = new Vector(3.0, 4.0);
            var v3 = new Vector(4.0, 3.0);
            var v4 = Vector.Invalid;
            var v5 = Vector.Invalid;

            Assert.IsTrue(v1 == v2, "Die Vektoren sind gleich, werden jedoch als ungleich erkannt!");
            Assert.IsFalse(v1 == v3, "Die Vektoren sind ungleich, werden jedoch als gleich erkannt!");

            Assert.IsFalse(v4 == v5, "Die Vektoren sind ungleich, werden jedoch als gleich erkannt!");
        }

        /// <summary>
        /// Teste die Ungleichheit
        /// </summary>
        [TestMethod]
        public void NotEqual()
        {
            var v1 = new Vector(3.0, 4.0);
            var v2 = new Vector(3.0, 4.0);
            var v3 = new Vector(4.0, 3.0);
            var v4 = Vector.Invalid;
            var v5 = Vector.Invalid;

            Assert.IsFalse(v1 != v2, "Die Vektoren sind gleich, werden jedoch als ungleich erkannt!");
            Assert.IsTrue(v1 != v3, "Die Vektoren sind ungleich, werden jedoch als gleich erkannt!");

            Assert.IsTrue(v4 != v5, "Die Vektoren sind ungleich, werden jedoch als gleich erkannt!");
        }

        /// <summary>
        /// Teste auf invalide Werte
        /// </summary>
        [TestMethod]
        public void IsNaN()
        {
            var v1 = new Vector(3.0, 4.0);
            var v2 = new Vector(3.0, double.NaN);
            var v3 = new Vector(double.NaN, 4.0);
            var v4 = Vector.Invalid;
            
            Assert.IsFalse(Vector.IsNaN(v1), "Der Vektor ist gültig, wird jedoch als ungültig erkannt!");
            Assert.IsTrue(Vector.IsNaN(v2), "Der Vektor ist ungültig, wird jedoch als gültig erkannt!");
            Assert.IsTrue(Vector.IsNaN(v3), "Der Vektor ist ungültig, wird jedoch als gültig erkannt!");
            Assert.IsTrue(Vector.IsNaN(v4), "Der Vektor ist ungültig, wird jedoch als gültig erkannt!");
        }

        /// <summary>
        /// Teste Plus
        /// </summary>
        [TestMethod]
        public void Plus()
        {
            var v1 = new Vector(3, 4);
            var v2 = new Vector(4.0, 3.0);
            var v3 = new Vector(7);
            var v4 = new Vector(3.0, double.NaN);
            var v5 = new Vector(double.NaN, 4.0);
            var v6 = Vector.Invalid;

            Assert.IsTrue(v1 + v2 == v3, "Das Ergebnis zweier Vektoren wurde falsch addiert");
            Assert.IsTrue(v2 + v1 == v3, "Das Ergebnis zweier Vektoren wurde falsch addiert");

            Assert.IsTrue(Vector.IsNaN(v4 + v5), "Das Ergebnis zweier Vektoren wurde falsch addiert");
        }

        /// <summary>
        /// Teste Minus
        /// </summary>
        [TestMethod]
        public void Minus()
        {
            var v1 = new Vector(3, 4);
            var v2 = new Vector(4.0, 3.0);
            var v3 = new Vector(-1.0, 1.0);
            var v4 = new Vector(1.0, -1.0);
            var v5 = new Vector(3.0, double.NaN);
            var v6 = new Vector(double.NaN, 4.0);
            var v7 = new Vector(5);
            var v8 = new Vector(5.0);
            var v9 = new Vector("<0,0>");

            Assert.IsTrue(v1 - v2 == v3, "Das Ergebnis zweier Vektoren wurde falsch subtrahiert");
            Assert.IsTrue(v2 - v1 == v4, "Das Ergebnis zweier Vektoren wurde falsch subtrahiert");

            Assert.IsTrue(Vector.IsNaN(v5 - v6), "Das Ergebnis zweier Vektoren wurde falsch subtrahiert");

            Assert.IsTrue(v7 - v8 == v9, "Das Ergebnis zweier Vektoren wurde falsch subtrahiert");
        }
    }
}