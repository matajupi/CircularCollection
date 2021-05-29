using NUnit.Framework;
using CircularCollection;

namespace TestCollection
{
    public class CircularLinkedListTests
    {
        [Test]
        public void AddFirst_Success()
        {
            var list = new CircularLinkedList<int>();
            list.AddFirst(0);
            list.AddFirst(1);
            Assert.AreEqual(1, list[0]);
        }

        [Test]
        public void AddLast_Success()
        {
            var list = new CircularLinkedList<int>();
            list.AddFirst(0);
            list.AddFirst(1);
            list.AddLast(2);
            Assert.AreEqual(2, list[^1]);
        }

        [Test]
        public void Remove_Success()
        {
            var list = new CircularLinkedList<int>();
            list.AddLast(0);
            list.AddLast(1);
            list.AddLast(2);
            list.Remove(1);
            Assert.AreEqual(2, list[1]);
        }

        [Test]
        public void RemoveAt_Success()
        {
            var list = new CircularLinkedList<int>();
            list.AddLast(0);
            list.AddLast(1);
            list.AddLast(2);
            list.RemoveAt(1);
            Assert.AreEqual(2, list[1]);
        }
        
        [Test]
        public void Sum_IntegerList_Success()
        {
            var list = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularLinkedList);
            TestCase.Sum_IntegerCollection_Success(list);
        }

        [Test]
        public void Sum_IntegerList_Fail()
        {
            var list = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularLinkedList);
            TestCase.Sum_IntegerCollection_Fail(list);
        }

        [Test]
        public void RotateRight_IntegerList_Success()
        {
            var list = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularLinkedList);
            TestCase.Rotate_IntegerCollection_Success(list, 2);
        }

        [Test]
        public void RotateLeft_IntegerList_Success()
        {
            var list = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularLinkedList);
            TestCase.Rotate_IntegerCollection_Success(list, -2);
        }

        [Test]
        public void Foreach_IntegerList_Success()
        {
            var list = TestCase.CreateCircularCollection(TestCase.InstanceType.CircularLinkedList);
            TestCase.Foreach_IntegerCollection_Success(list);
        }
    }
}