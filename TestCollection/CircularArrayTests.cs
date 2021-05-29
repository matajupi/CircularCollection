using NUnit.Framework;

namespace TestCollection
{
    public class CircularArrayTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Sum_IntegerArray_Success()
        {
            var array = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularArray);
            TestCase.Sum_IntegerCollection_Success(array);
        }

        [Test]
        public void Sum_IntegerArray_Fail()
        {
            var array = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularArray);
            TestCase.Sum_IntegerCollection_Fail(array);
        }

        [Test]
        public void RotateRight_IntegerArray_Success()
        {
            var array = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularArray);
            TestCase.Rotate_IntegerCollection_Success(array, 2);
        }

        [Test]
        public void RotateLeft_IntegerArray_Success()
        {
            var array = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularArray);
            TestCase.Rotate_IntegerCollection_Success(array, -2);
        }

        [Test]
        public void Foreach_IntegerArray_Success()
        {
            var array = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularArray);
            TestCase.Foreach_IntegerCollection_Success(array);
        }
    }
}